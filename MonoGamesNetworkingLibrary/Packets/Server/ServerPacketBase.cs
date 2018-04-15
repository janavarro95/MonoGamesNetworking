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
    public class ServerPacketBase
    {

        /// <summary>
        /// Client_None packet type: Functionality for this packet is none.
        /// </summary>
        public static readonly ServerPacketBase None = new ServerPacketBase("None");

        /// <summary>
        /// Changes the value passed in to a string;
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Key;
        }


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="Value"></param>
        protected ServerPacketBase(string Key)
        {
            this.Key = Key;

        }

        /// <summary>
        /// The value of my enum.
        /// </summary>
        public string Key { get; private set; }

    }
}
