﻿using System;
using SharedObjects;

namespace Messages
{
    [Serializable]
    public class JoinGame : Message
    {
        public int GameID { get; set; }
        public Player Player { get; set; }
    }
}
