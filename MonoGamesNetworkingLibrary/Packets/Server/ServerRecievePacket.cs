using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGamesNetworkingLibrary.Packets.Server
{
    /// <summary>
    /// Packets sent to the client to tell the client how to receive this data.
    /// </summary>
    class ServerRecievePacket :ServerPacketBase
    {
        /// <summary>
        /// Client_None packet type: Functionality for this packet is none.
        /// </summary>
        public static readonly ServerRecievePacket None = new ServerRecievePacket("None");
        public static readonly ServerRecievePacket AllPlayers = new ServerRecievePacket("AllPlayers");

        public ServerRecievePacket(string key) : base(key)
        {

        }

    }
}
