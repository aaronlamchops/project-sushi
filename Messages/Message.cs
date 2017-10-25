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

    public class Message{
        static Message() { }

        int MsgId;

        int ConvId;

        public byte[] Encode()
        {
            return;
        }

        public void Decode()
        {

        }

    }
}
