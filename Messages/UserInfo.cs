using System;
namespace Messages
{
    [Serializable]
    public class UserInfo : Message
    {
        public string UserName { get; set; }
    }
}
