using System;
using System.Collections.Generic;
using SharedObjects;

namespace Messages
{
    [Serializable]
    public class PassCard : Message
    {
        public List<CardTypes> Hand { get; set; }
    }
}
