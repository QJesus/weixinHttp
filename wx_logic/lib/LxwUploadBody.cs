using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

#if WeChat
namespace WeChat.Lib
#else
namespace HttpSocket
#endif
{
    public class LxwUploadBody
    {
        public LxwUploadBody(Encoding encoding, string filename, Dictionary<string, string> KEYS = null)
        {
            Encoding = encoding;
            //uM55vqcAjmXSlVHa
            Boundary = "----WebKitFormBoundaryLXW" + DateTime.Now.ToString("MMddHHmmssfff");

            CreateBody(filename, KEYS);
        }

        /// <summary>
        /// 生成body
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="KEYS"></param>
        private void CreateBody(string filename, Dictionary<string, string> KEYS)
        {
            foreach (var o in KEYS.Keys)
            {
                AddDisposition(o, KEYS[o]);
            }

            var name = Path.GetFileName(filename);
            //插入body
            AddString("--" + Boundary);
            AddString(LINE);
            AddString("Content-Disposition: form-data; name=\"filename\"; filename=\"" + name + "\"");
            AddString(LINE);
            AddString("Content-Type: " + MimeMapping.GetMimeMapping(Path.GetExtension(filename)));
            AddString(LINE);
            AddString(LINE);
            foreach (var o in File.ReadAllBytes(filename))
            {
                body.Add(o);
            }
            AddString(LINE);
            AddString("--" + Boundary + "--");
            //如果这里差一个，就发送不出去
            AddString(LINE);
        }

        void AddDisposition(string key, string value)
        {
            AddString("--" + Boundary);
            AddString(LINE);
            AddString("Content-Disposition: form-data; name=\"" + key + "\"");
            AddString(LINE);
            AddString(LINE);
            AddString(value);
            AddString(LINE);
        }

        string LINE = "\r\n";

        void AddString(string data)
        {
            foreach (var o in Encoding.GetBytes(data))
            {
                body.Add(o);
            }
        }

        List<byte> body = new List<byte>();
        public byte[] Body => body.ToArray();

        public Encoding Encoding { get; set; }

        public string Boundary { get; set; }
    }
}
