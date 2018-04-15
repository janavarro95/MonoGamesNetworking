using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGamesNetworkingLibrary.Packets.Server
{
    /// <summary>
    /// Used to hold keys to determine 
    /// </summary>
    public class ServerNonePacket : ServerPacketBase
    {

        /// <summary>
        /// Client_None packet type: Functionality for this packet is none.
        /// </summary>
        public static readonly ServerNonePacket None = new ServerNonePacket("None");

        public ServerNonePacket(string key) : base(key)
        {

        }

    }
}
