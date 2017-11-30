using System;
namespace Messages
{
    [Serializable]
    public class RequestPID : Message
    {
        public int PlayerID { get; set; }
    }
}
