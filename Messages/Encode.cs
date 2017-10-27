using System;
using System.Text;
using System.IO;
using System.Net;

namespace Messages
{
    public class Encoder
    {
        private MemoryStream _mstream;

        public Encoder()
        {
            _mstream = new MemoryStream();
        }

        public void Add(int value)
        {
            //convert to short
            short shortVal = (short) value;
            byte[] bytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(shortVal));
            _mstream.Write(bytes, 0, bytes.Length);
        }

        public void Add(string value)
        {
            byte[] bytes = Encoding.BigEndianUnicode.GetBytes(value);
            Add((short)bytes.Length);
            _mstream.Write(bytes, 0, bytes.Length);
        }

        public byte[] getBytes()
        {
            return _mstream.ToArray();
        }
    }

    public class Decoder
    {
        private MemoryStream _mstream;

        public Decoder(byte[] bytes)
        {
            _mstream = new MemoryStream(bytes);
        }

        //actually reads a short - can change if we need more
        public int readInt()
        {
            byte[] bytes = new byte[2];
            int bytesRead = _mstream.Read(bytes, 0, bytes.Length);
            if (bytesRead != bytes.Length)
                throw new ApplicationException("Cannot decode an integer from message");

            return IPAddress.NetworkToHostOrder(BitConverter.ToInt16(bytes, 0));
        }

        public int readByte()
        {
            return _mstream.ReadByte();
        }

        public string readString()
        {
            string result = String.Empty;
            int length = readInt();
            if (length > 0)
            {
                byte[] bytes = new byte[length];
                int bytesRead = _mstream.Read(bytes, 0, bytes.Length);
                if (bytesRead != length)
                    throw new ApplicationException("Cannot decode a string from message");

                result = Encoding.BigEndianUnicode.GetString(bytes, 0, bytes.Length);
            }
            return result;
        }
    }
}
