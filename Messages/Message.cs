using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages
{
    //Need messages for:
    /*
     * Create Game
     * Join Game
     * Start Game
     * Exit Game
     * Send User Info
     * Select Card - TCP?
     * Update Game State - TCP?
     * Update Chat
     * Pass Cards - TCP?
     * Start New Round - TCP?
     * Heartbeat
     */
     // Also need decorator
     // Tweak however works

    //Base-concrete message class
    public class Message{

        public virtual int MsgId { get; set; }
        public virtual int ConvId { get; set; }


        public virtual byte[] Encode()
        {
            return null;
        }

        public virtual void Decode(byte[] message)
        {

        }

    }
}