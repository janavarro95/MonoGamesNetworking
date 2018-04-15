using Lidgren.Network;
using MonoGamesNetworkingLibrary.Packets.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MonoGamesNetworkingLibrary.Packets.Packet;

namespace MonoGamesNetworkingLibrary.Packets
{
    public class PacketInfo
    {
        public SenderPackets sender { get; set; }

        public Packets.ClientPacketTypes clientPacket { get; set; } //A packet sent by the client.
        public Packets.Server.ServerPacketTypes serverPacket { get; set; } //A packet sent by the server.


        public PacketInfo()
        {

        }

        public override string ToString()
        {
            return "Sender: " + this.sender + " Client: " + this.clientPacket + " Server: " + this.serverPacket; 
        }

        public PacketInfo(SenderPackets Sender, Packets.ClientPacketTypes ClientPacket, Packets.Server.ServerPacketTypes ServerPacket)
        {
            this.sender = Sender;
            this.clientPacket = ClientPacket;
            this.serverPacket = ServerPacket;
        }

        /// <summary>
        /// Gets the info stored in the packet sent.
        /// </summary>
        /// <returns></returns>
        public string getPacketType()
        {
            //If the sender is a server, get the value from the client packet so i know to parse the client functionality.
            if (this.sender.Value == SenderPackets.Server.Value)
            {
                return clientPacket.packetType;
            }

            //If the sender is a Client, get the value from the server packet so i know to parse the server functionality.
            if (this.sender.Value == SenderPackets.Client.Value)
            {
                return serverPacket.packetType;
            }
            return "";
        }

        /// <summary>
        /// Gets the info stored in the packet sent.
        /// </summary>
        /// <returns></returns>
        public string getPacketFunction()
        {
            //If the sender is a server, get the value from the client packet so i know to parse the client functionality.
            if (this.sender == SenderPackets.Server)
            {
                return clientPacket.functionality;
            }

            //If the sender is a Client, get the value from the server packet so i know to parse the server functionality.
            if (this.sender == SenderPackets.Client)
            {
                return serverPacket.functionality;
            }
            return "";
        }

        /// <summary>
        /// Write this to a net outgoing message
        /// </summary>
        /// <param name="msg"></param>
        public void writeToMessage(NetOutgoingMessage msg)
        {
            sender.writeToMessage(msg);
            clientPacket.writeToMessage(msg);
            serverPacket.writeToMessage(msg);

            //msg.WriteAllProperties(this);
        }

        /// <summary>
        /// Read this from an outgoing message.
        /// </summary>
        /// <param name="msg"></param>
        public static PacketInfo readFromMessage(NetIncomingMessage msg)
        {
            PacketInfo info = new PacketInfo();
            info.sender = SenderPackets.readFromMessage(msg);
            info.clientPacket = ClientPacketTypes.readFromMessage(msg);
            info.serverPacket = ServerPacketTypes.readFromMessage(msg);
            return info;
        }

    }
}
