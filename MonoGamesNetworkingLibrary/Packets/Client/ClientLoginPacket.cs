using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGamesNetworkingLibrary.Packets.Client
{
    /// <summary>
    /// Used to hold keys to determine 
    /// </summary>
    public class ClientLoginPacket : ClientPacketBase
    {

        /// <summary>
        /// Client_None packet type: Functionality for this packet is none.
        /// </summary>
        public static readonly ClientLoginPacket None = new ClientLoginPacket("None");

        public ClientLoginPacket(string key) : base(key)
        {

        }

    }
}
