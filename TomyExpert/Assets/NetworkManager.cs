using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Threading;
using UnityEngine.UI;
using networkBase;

public class NetworkManager : MonoBehaviour {

	// Use this for initialization
	
	private static int _serverPort = 4444;


	private bool isServerInitialized = false;
	private bool clientConnected = false;
	private int currentConnection = -1;

	public RawImage imgscreen;
	public GameObject PanelAppelEntrant;
	public AudioSource audioSource;


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
			NetworkServer.RegisterHandler(MsgType.Connect, OnConnected);
			NetworkServer.RegisterHandler(MsgType.Disconnect, OnDisconnected);
			NetworkServer.RegisterHandler(CamUpdate.MsgId, ReceivedUpdateCam);
			NetworkServer.RegisterHandler(NetMessage.MsgId, ReceivedNetMessage);

			NetworkServer.Listen(_serverPort); // écoute sur le port 4444 sur toute les interface réseau disponible
			//Debug.Log(NetworkServer.numChannels);
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
		//nm.str = "toggle_anim;kitkat;Logo";
		nm.str = "test";
		NetworkServer.SendToAll(NetMessage.MsgId, nm);
		AllowConnexion();
	}

	public void sendMessage2()
	{
		RefuseConnexion();
	}

	public void sendMessageSurimpression(string model, string element) {
		Debug.Log("sending message " + model + " " + element);
		NetMessage nm = new NetMessage();
		nm.number = 5;
		nm.str = "surimpression;"+ model +";" + element;
		NetworkServer.SendToAll(NetMessage.MsgId, nm);
	}
	
	public void sendMessageChangeArrowProperty(string model, string property, string axisOperation) {
		
		NetMessage nm = new NetMessage();
		nm.number = 5;
		nm.str = "arrow;"+ model +";" + property + ";" + axisOperation;
		Debug.Log(nm.str);
		NetworkServer.SendToAll(NetMessage.MsgId, nm);
	}
	
	public void OnConnected(NetworkMessage netMsg)
	{
		Debug.Log("Connection occured");
		//Refuse automatiquement les connexions supplémentaires
		if (clientConnected) { 
			RefuseConnexion(netMsg.conn.connectionId);
			
		//	netMsg.conn.Disconnect(); // Le client appel Disconnected aussi => peut crée une erreur.
		}
		else
		{
			
			PanelAppelEntrant.SetActive(true);
			audioSource.Play();
			clientConnected = true;
			currentConnection = netMsg.conn.connectionId;
		}
	}
	public void OnDisconnected(NetworkMessage netMsg)
	{
		Debug.Log("Someone Disconnected");
		if (netMsg.conn.connectionId == currentConnection)
		{
			Debug.Log("Current Client disconnected");
			imgscreen.texture = null;
			currentConnection = -1;
			clientConnected = false;

		}
	}

	public void AllowConnexion()
	{
		Debug.Log("sending message allow Connexion true");
		NetMessage nm = new NetMessage();
		nm.number = 0;
		nm.str = "allowConnexion;true";
		NetworkServer.SendToAll(NetMessage.MsgId, nm);
	}

	public void RefuseConnexion()
	{
		Debug.Log("sending message allow Connexion false");
		NetMessage nm = new NetMessage();
		nm.number = 0;
		nm.str = "allowConnexion;false";
		NetworkServer.SendToAll(NetMessage.MsgId, nm);
	}

	public void RefuseConnexion(int connId)
	{
		Debug.Log("sending message allow Connexion false");
		NetMessage nm = new NetMessage();
		nm.number = 1;
		nm.str = "allowConnexion;false";
		NetworkServer.SendToClient(connId,NetMessage.MsgId, nm);
	}

	public void CloseConnexion()
	{
		Debug.Log("sending close Connexion ");
		NetMessage nm = new NetMessage();
		nm.number = 1;
		nm.str = "closeConnexion;true";
		NetworkServer.SendToAll(NetMessage.MsgId, nm);
	}

	public void ReceivedUpdateCam(NetworkMessage netMsg)
	{
		//reception et lecture d'un message CamUpdate
		CamUpdate cu = netMsg.ReadMessage<CamUpdate>();
	//	Debug.Log("Received msg " + cu.ImageBytes.ToString());
		
		Texture2D t2d = new Texture2D(640, 480); // création d'une nouvelle texture 2d
		t2d.LoadImage(cu.ImageBytes); // envoie l'image reçu depuis CamUpdate dans la texture2D
		t2d.Apply();

		//Application de la texture a l'image sur l'écran
		imgscreen.texture = t2d;
	}
}