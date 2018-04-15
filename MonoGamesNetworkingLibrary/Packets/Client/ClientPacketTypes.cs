using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGamesNetworkingLibrary.Packets
{
    /// <summary>
    /// Packet to send to the client to understand how to parse the data.
    /// </summary>
    public class ClientPacketTypes
    {
        public static readonly string Type_None = "None";
        public static readonly string Type_Login = "Login";
        public static readonly string Type_AllPlayers = "AllPlayers";
        public static readonly string Type_Request = "Request";


        /// <summary>
        /// Denotes that the packet sent is a none type and holds a none functionality.
        /// </summary>
        public static readonly ClientPacketTypes None_None = new ClientPacketTypes(Type_None,Client.ClientNonePacket.None.Key);

        public static readonly ClientPacketTypes Login_None = new ClientPacketTypes(Type_Login, Client.ClientLoginPacket.None.Key);

        public static readonly ClientPacketTypes AllPlayers_None = new ClientPacketTypes(Type_AllPlayers, Client.ClientAllPlayersPacket.None.Key);

        public static readonly ClientPacketTypes Request_None = new ClientPacketTypes(Type_Request, Client.ClientRequestPacket.None.Key);
        public static readonly ClientPacketTypes Request_AllPlayers = new ClientPacketTypes(Type_Request, Client.ClientRequestPacket.AllPlayers.Key);


        /// <summary>
        /// Changes the value passed in to a string;
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.packetType+"_"+this.functionality;
        }


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="Value"></param>
        protected ClientPacketTypes(string PacketType,string functionality)
        {
            this.packetType = PacketType;
            this.functionality = functionality;

        }

        /// <summary>
        /// The value of my enum.
        /// </summary>
        public string packetType { get; private set; }

        /// <summary>
        /// The string value of the functionality to happen.
        /// </summary>
        public string functionality { get; private set; }

        public void writeToMessage(NetOutgoingMessage msg)
        {
            msg.Write(this.packetType);
            msg.Write(this.functionality);
        }

        public static ClientPacketTypes readFromMessage(NetIncomingMessage msg)
        {
            string type = msg.ReadString();
            string function = msg.ReadString();
            ClientPacketTypes packet = new ClientPacketTypes(type, function);
            return packet;

        }

    }
}
