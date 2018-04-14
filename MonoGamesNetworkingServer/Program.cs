using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGamesNetworkingLibrary.NetworkLibraries;
using MonoGamesNetworkingLibrary.InfoLibraries;

namespace MonoGamesNetworkingLibrary.Server
{
    /// <summary>
    /// Runs the server program.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {

            var server = new Server();
            server.Run();
        }


    }
}
