using System;
using System.Text;

namespace HttpSocket
{

    public class LxwRequestHeader
    {
        public LxwRequestHeader(Encoding encoding)
        {
            Encoding = encoding;
        }

        public Uri Uri { get; set; }
        public byte[] HeaderByte => !string.IsNullOrEmpty(Header) ? Encoding.GetBytes(Header) : null;
        public string Header { get; set; }
        public bool SSL { get; set; }
        public Encoding Encoding { get; private set; }
    }
}
