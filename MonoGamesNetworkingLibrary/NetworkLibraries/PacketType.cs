using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGamesNetworkingLibrary.NetworkLibraries
{
    public enum PacketType
    {
        Login,
        NewPlayer,
        AllPlayers,
        RequestAllPlayers,
        RecieveAllPlayers
    }
    public class PacketTypes
    {
        

        /// <summary>
        /// Write a specified byte to an outgoing message.
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="packet"></param>
        public static void writeToMessage(NetOutgoingMessage msg, PacketType packet)
        {
            msg.Write((byte)packet);
        }

        /// <summary>
        /// Read the type of packet from an incoming message.
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static PacketType readPacketType(NetIncomingMessage msg)
        {
            return (PacketType)msg.ReadByte();
        }
    }
}
