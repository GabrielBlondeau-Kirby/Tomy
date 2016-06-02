using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Threading;
using UnityEngine.UI;
using networkBase;
using UnityEditor;

public class NetworkManager : MonoBehaviour {

	// Use this for initialization
	
	private static int _serverPort = 4444;


	private bool isServerInitialized = false;

	public RawImage imgscreen;

	void Awake()
	{
		Debug.Log("Awake");
		SetupServer();
	}

	// Create a server and listen on a port
	public void SetupServer()
	{
		if (!isServerInitialized)
		{
			Debug.Log("Setting up Server");
			HostTopology Topology = new HostTopology(network.getConnectionConfig(), 5); // récupération de la topologie réseau
			NetworkServer.Configure(Topology); // Applique la configuration réseau (3 channel)

			// Map une fonction a chaque Id de message reçu
			NetworkServer.RegisterHandler(MsgType.Connect,OnConnected);
			NetworkServer.RegisterHandler(CamUpdate.MsgId, ReceivedUpdateCam);
			NetworkServer.RegisterHandler(NetMessage.MsgId, ReceivedNetMessage);

			NetworkServer.Listen(_serverPort); // écoute sur le port 4444 sur toute les interface réseau disponible
			Debug.Log(NetworkServer.numChannels);
			isServerInitialized = true;
		}
		else
		{
			Debug.Log("Killing server");
			NetworkServer.Shutdown();
			NetworkServer.Reset();
			isServerInitialized = false;
		}
	}

	private void ReceivedNetMessage(NetworkMessage netmsg)
	{
		Debug.Log("Received netmsg");

	}

	public void sendMessage1()
	{
		Debug.Log("sending message 1");
		NetMessage nm = new NetMessage();
		nm.number = 5;
		nm.str = "toggle_anim;kitkat;Logo";
		NetworkServer.SendToAll(NetMessage.MsgId, nm);
	}

	public void sendMessage2()
	{
		Debug.Log("sending message 1");
		NetMessage nm = new NetMessage();
		nm.number = 5;
		nm.str = "toggle_anim;kitkat;RTradeMark";
		NetworkServer.SendToAll(NetMessage.MsgId, nm);
	}
	
	public void OnConnected(NetworkMessage netMsg)
	{
		Debug.Log("Connection occured");
	}

	public void ReceivedUpdateCam(NetworkMessage netMsg)
	{
		//reception et lecture d'un message CamUpdate
		CamUpdate cu = netMsg.ReadMessage<CamUpdate>();
		Debug.Log("Received msg " + cu.ImageBytes.ToString());
		
		Texture2D t2d = new Texture2D(640, 480); // création d'une nouvelle texture 2d
		t2d.LoadImage(cu.ImageBytes); // envoie l'image reçu depuis CamUpdate dans la texture2D
		t2d.Apply();

		//Application de la texture a l'image sur l'écran
		imgscreen.texture = t2d;
	}
}