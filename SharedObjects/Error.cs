using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace SharedObjects
{
    [DataContract]
    public class Error
    {
        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public DateTime TimeStamp { get; private set; } = DateTime.Now;

    }
}
