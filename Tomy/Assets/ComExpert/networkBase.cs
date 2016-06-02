using UnityEngine.Networking;

namespace networkBase
{
	/**
	 * Ce fichier doit être identique pour les 2 programmes
	 * 
	 */
	public class CamUpdate : MessageBase
    {

		public static short MsgId = 10002;

		public byte[] ImageBytes;

	}

	public class NetMessage : MessageBase
	{

		public static short MsgId = 10003;

		public string str;
		public int number = 0;

	}

	public class network
	{
		private static ConnectionConfig  cc;
		public static ConnectionConfig getConnectionConfig()
		{
			if (cc != null)
				return cc;

			cc = new ConnectionConfig();
			cc.AddChannel(QosType.Reliable);
			cc.AddChannel(QosType.Unreliable);
			cc.AddChannel(QosType.ReliableFragmented);

			return cc;
		}
	}

}
