using System;
namespace Messages
{
    [Serializable]
    public class UpdateChat : Message
    {
        public string Message { get; set; }
        public int PlayerID { get; set; }
    }
}
