using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGamesNetworkingLibrary.Packets.Server
{
    public class ServerLoginPacket : ServerPacketBase
    {

        /// <summary>
        /// Client_None packet type: Functionality for this packet is none.
        /// </summary>
        public static readonly ServerLoginPacket None = new ServerLoginPacket("None");
        public static readonly ServerLoginPacket ConnectionApprove = new ServerLoginPacket("ConnectionApprove");

        public ServerLoginPacket(string key) : base(key)
        {

        }

    }
}
