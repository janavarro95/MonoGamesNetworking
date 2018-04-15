using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGamesNetworkingLibrary.Packets.Server
{
    /// <summary>
    /// Packet to send to the server to understand how to parse the data.
    /// </summary>
    public class ServerPacketTypes
    {

        /// <summary>
        /// Denotes that the packet sent is a none type and holds a none functionality.
        /// </summary>
        public static readonly ServerPacketTypes None_None = new ServerPacketTypes("None", Server.ServerNonePacket.None.Key);

        public static readonly ServerPacketTypes Login_None = new ServerPacketTypes("Login", Server.ServerLoginPacket.None.Key);
        public static readonly ServerPacketTypes Login_ConnectionApprove = new ServerPacketTypes("Login", Server.ServerLoginPacket.ConnectionApprove.Key);

        public static readonly ServerPacketTypes Recieve_AllPlayers = new ServerPacketTypes("Recieve", Server.ServerRecievePacket.AllPlayers.Key);


        /// <summary>
        /// Changes the value passed in to a string;
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.packetType + "_" + this.functionality;
        }


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="Value"></param>
        protected ServerPacketTypes(string PacketType, string functionality)
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

        public static ServerPacketTypes readFromMessage(NetIncomingMessage msg)
        {
            string type = msg.ReadString();
            string function = msg.ReadString();
            ServerPacketTypes packet = new ServerPacketTypes(type, function);
            return packet;

        }

    }
}
