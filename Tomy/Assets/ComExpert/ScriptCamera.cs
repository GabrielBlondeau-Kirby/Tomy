using UnityEngine;
using networkBase;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Threading;
using Vuforia;

public class ScriptCamera : MonoBehaviour
{
	//private const string ServerIp = "192.168.0.58";
	private const string ServerIp = "10.145.128.96";
	private const int ServerPort = 4444;

	private const string PrefixTarget = "ObjectTarget_";
	private const string PrefixAugmentation = "anim_";

	private const string Command_ToggleAnim = "toggle_anim";

	public Text debug_normal; // ecriture vert à l'écran
	public Text debug_network; // ecriture rouge à l'écran
	private NetworkClient netClient;
	private int tick = 0;
	// Use this for initialization
	void Start ()
	{
		// camera vuforia
		CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);

		//recuperation de la topologie réseau
		HostTopology Topology = new HostTopology(network.getConnectionConfig(), 5);
		dbg_network("Client");

		//initialisation et configuration du client réseau
		netClient = new NetworkClient();
		netClient.Configure(Topology);

		// on map les methodes qui seront appelé pour chaque id des messages reçu
		netClient.RegisterHandler(MsgType.Connect, OnConnected);
		netClient.RegisterHandler(CamUpdate.MsgId, ReceivedUpdateCam);
		netClient.RegisterHandler(NetMessage.MsgId, ReceivedMessage);

		//Connection du client
		netClient.Connect(ServerIp, ServerPort);
		dbg_network(netClient.isConnected?"connected":"not connected");
	}

	private void OnConnected(NetworkMessage netmsg)
	{
		dbg_network("connected");
	}

	private void ReceivedMessage(NetworkMessage netmsg)
	{
		dbg_network("received message");

		string message = netmsg.ReadMessage<NetMessage>().str;
		string[] split = message.Split(';');
		if (split[0].Equals(Command_ToggleAnim))
		{
			GameObject go = GameObject.Find(PrefixTarget + split[1]).transform.Find(PrefixAugmentation + split[2]).gameObject;
			go.SetActive(!go.activeInHierarchy);
			dbg_network("activating ");
		}
	}

	private void ReceivedUpdateCam(NetworkMessage netmsg)
	{
		dbg_network("received cam update Should not happen");
	}


	private void dbg_network(string text)
	{
		if (debug_network != null)
			if (debug_network.IsActive())
				debug_network.text= text;
	}
	private void dbg_normal(string text)
	{
		if (debug_normal != null)
			if (debug_normal.IsActive())
				debug_normal.text= text;
	}

	//
	//                         .-=-.          .--. <3
	//             __        .'     '.    <3 /  " )
	//     _     .'  '.     /   .-.   \     /  .-'\  
	//    ( \   / .-.  \   /   /   \   \   /  /    ^ 
	//     \ `-` /   \  `-'   /     \   `-`  /	 	  <3  
	//      `-.-`     '.____.'       `.____.'
	//
	//ascii.co.uk

	void Update ()
	{
		tick++;
		//Tick pour eviter envoyer l'image de la caméra toute les X updates (la c'est 15)
		if (tick > 15)
		{
			tick = 0;
			// récupération de la taille de l'écran divisé par 8
			int width = Screen.width/8,
				height = Screen.height/8;
			// crée une texture qui fait 5/8 de la taille de l'écran
			Texture2D tex = new Texture2D(width*5,height*5);

			tex.ReadPixels(new Rect(width+width/2,height+height/2, width*5,height*5),0,0);
			tex.Apply();

			//Point permet de changer la définition d'une Texture2D.
			Point(tex, Screen.width / 4, Screen.height / 4);

			
			CamUpdate cu = new CamUpdate();
			cu.ImageBytes = tex.EncodeToJPG();
			dbg_normal(cu.ImageBytes.Length + "");
			netClient.SendByChannel(CamUpdate.MsgId, cu, 2);
			/**/
		}
	}



	/**
	 * 
	 * 
	 * 
	 * 
	 * 
	 * 
	 * 
	 * 
	 * 
	 * Librairie pour resize un Texture2D
	 * Pas besoin d'aller plus bas dans la classe
	 * 
	 * 
	 * 
	 * 
	 * 
	 * 
	 * 
	 * 
	 * 
	 * 
	 */


	public class ThreadData
	{
		public int start;
		public int end;
		public ThreadData(int s, int e)
		{
			start = s;
			end = e;
		}
	}

	private static Color[] texColors;
	private static Color[] newColors;
	private static int w;
	private static float ratioX;
	private static float ratioY;
	private static int w2;
	private static int finishCount;
	private static Mutex mutex;

	public static void Point(Texture2D tex, int newWidth, int newHeight)
	{
		ThreadedScale(tex, newWidth, newHeight, false);
	}

	public static void Bilinear(Texture2D tex, int newWidth, int newHeight)
	{
		ThreadedScale(tex, newWidth, newHeight, true);
	}

	private static void ThreadedScale(Texture2D tex, int newWidth, int newHeight, bool useBilinear)
	{
		texColors = tex.GetPixels();
		newColors = new Color[newWidth * newHeight];
		if (useBilinear)
		{
			ratioX = 1.0f / ((float)newWidth / (tex.width - 1));
			ratioY = 1.0f / ((float)newHeight / (tex.height - 1));
		}
		else
		{
			ratioX = ((float)tex.width) / newWidth;
			ratioY = ((float)tex.height) / newHeight;
		}
		w = tex.width;
		w2 = newWidth;
		var cores = Mathf.Min(SystemInfo.processorCount, newHeight);
		var slice = newHeight / cores;

		finishCount = 0;
		if (mutex == null)
		{
			mutex = new Mutex(false);
		}
		if (cores > 1)
		{
			int i = 0;
			ThreadData threadData;
			for (i = 0; i < cores - 1; i++)
			{
				threadData = new ThreadData(slice * i, slice * (i + 1));
				ParameterizedThreadStart ts = useBilinear ? new ParameterizedThreadStart(BilinearScale) : new ParameterizedThreadStart(PointScale);
				Thread thread = new Thread(ts);
				thread.Start(threadData);
			}
			threadData = new ThreadData(slice * i, newHeight);
			if (useBilinear)
			{
				BilinearScale(threadData);
			}
			else
			{
				PointScale(threadData);
			}
			while (finishCount < cores)
			{
				Thread.Sleep(1);
			}
		}
		else
		{
			ThreadData threadData = new ThreadData(0, newHeight);
			if (useBilinear)
			{
				BilinearScale(threadData);
			}
			else
			{
				PointScale(threadData);
			}
		}

		tex.Resize(newWidth, newHeight);
		tex.SetPixels(newColors);
		tex.Apply();
	}

	public static void BilinearScale(System.Object obj)
	{
		ThreadData threadData = (ThreadData)obj;
		for (var y = threadData.start; y < threadData.end; y++)
		{
			int yFloor = (int)Mathf.Floor(y * ratioY);
			var y1 = yFloor * w;
			var y2 = (yFloor + 1) * w;
			var yw = y * w2;

			for (var x = 0; x < w2; x++)
			{
				int xFloor = (int)Mathf.Floor(x * ratioX);
				var xLerp = x * ratioX - xFloor;
				newColors[yw + x] = ColorLerpUnclamped(ColorLerpUnclamped(texColors[y1 + xFloor], texColors[y1 + xFloor + 1], xLerp),
													   ColorLerpUnclamped(texColors[y2 + xFloor], texColors[y2 + xFloor + 1], xLerp),
													   y * ratioY - yFloor);
			}
		}

		mutex.WaitOne();
		finishCount++;
		mutex.ReleaseMutex();
	}

	public static void PointScale(System.Object obj)
	{
		ThreadData threadData = (ThreadData)obj;
		for (var y = threadData.start; y < threadData.end; y++)
		{
			var thisY = (int)(ratioY * y) * w;
			var yw = y * w2;
			for (var x = 0; x < w2; x++)
			{
				newColors[yw + x] = texColors[(int)(thisY + ratioX * x)];
			}
		}

		mutex.WaitOne();
		finishCount++;
		mutex.ReleaseMutex();
	}

	private static Color ColorLerpUnclamped(Color c1, Color c2, float value)
	{
		return new Color(c1.r + (c2.r - c1.r) * value,
						  c1.g + (c2.g - c1.g) * value,
						  c1.b + (c2.b - c1.b) * value,
						  c1.a + (c2.a - c1.a) * value);
	}
}
