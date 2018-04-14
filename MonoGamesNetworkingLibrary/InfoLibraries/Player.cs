using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGamesNetworkingLibrary.InfoLibraries
{
    public class Player
    {
        public string Name { get; set; }
        public NetConnection connection { get; set; }
        public float xPosition { get; set; }
        public float yPosition { get; set; }

        public Player()
        {

        }

        public Player(string name, float xPos, float yPos)
        {
            this.Name = name;
            this.xPosition = xPos;
            this.yPosition = yPos;
        }

        /// <summary>
        /// Writes all of this following information to a outgoing message. The message MUST be read in this order.
        /// Only writes some simple variables to a message. THIS ORDER MUST BE THE SAME AS writeToMessage.
        /// </summary>
        /// <param name="msg"></param>
        public void writeLoginInfo(NetOutgoingMessage msg)
        {
            msg.Write(Name);
            msg.Write(xPosition);
            msg.Write(yPosition);
        }

        /// <summary>
        /// Writes the player to a outgoing net message.
        /// </summary>
        /// <param name="msg"></param>
        public void writeToMessage(NetOutgoingMessage msg)
        {
            msg.WriteAllProperties(this);
            return;
        }

        /// <summary>
        /// Takes a net incoming message that was sent from the client using the writeLoginInfo parameters and reads it all in order to create a new player.
        /// 
        /// Reads the player info from variables passed in, not the entire character.
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static Player createFromIncomingNetMessageSimple(NetIncomingMessage msg)
        {
            Player player = new Player()
            {
                connection = msg.SenderConnection,
                Name = msg.ReadString(),
                xPosition = msg.ReadFloat(),
                yPosition = msg.ReadFloat(),
            };
            return player;
        }

        /// <summary>
        /// Takes a net message and using reflection to get the player from the message.
        /// Reads in a whole character that was sent via a message.
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static Player createFromIncomingNetMessageVerbose(NetIncomingMessage msg)
        {
            Player player = new Player();
            msg.ReadAllProperties(player);
            return player;
        }


    }
}
