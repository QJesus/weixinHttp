﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// 20160111 lxw beijing
/// 主要作用socket模拟http请求
/// 用c# httpwebrequest httpclient webclient 总是感觉操作不是很爽快，总有什么卡住
/// 这个类可以直接发送request 头 内容
/// 
/// 注意:
/// body的长度非常重要，上传文件最后的\r\n不要搞错了。
/// POST GET /路径问题
/// 编码问题
/// OD OA OD OA问题
/// 
/// </summary>
#if WeChat
namespace WeChat.Lib
#else
namespace HttpSocket
#endif
{
    public class LxwHttpSocket
    {
        /// <summary>
        /// 存储cookies
        /// </summary>
        public Dictionary<string, LxwCookie> LST_COOKIE = new Dictionary<string, LxwCookie>();

        /// <summary>
        /// 设置默认编码
        /// </summary>
        /// <param name="Encoding"></param>
        public LxwHttpSocket(Encoding Encoding = null, int TimeOut = 3)
        {
            this.Encoding = Encoding ?? Encoding.UTF8;
            WriteTimeOut = TimeOut;
            ReadTimeOut = 30;
        }

        /// <summary>
        /// 存储一些全局信息
        /// </summary>
        Dictionary<string, string> DI_KEYS = new Dictionary<string, string>();
        public void Add(string Key, string Value)
        {
            DI_KEYS[Key] = Value;
        }

        public string this[string key]
        {
            get
            {
                if (DI_KEYS.ContainsKey(key))
                {
                    return DI_KEYS[key];
                }

                foreach (var cookies in LST_COOKIE.Values)
                {
                    if (cookies.Key == key)
                    {
                        return cookies.Value;
                    }
                }

                return "";
            }
        }

        /// <summary>
        /// 编码
        /// </summary>
        public Encoding Encoding { get; set; }

        /// <summary>
        /// request 里面第一条必须是method
        /// </summary>
        /// <param name="request"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public LxwResponse SendRequest(string request, string body = null, Dictionary<string, string> KEYS = null, bool write = false)
        {
            var sendBody = FormatBody(body, KEYS);
            var sendHeader = FormatHeader(request, sendBody, KEYS);

            Console.WriteLine(sendHeader.Uri.ToString());
            TcpClient client = new TcpClient(sendHeader.Uri.Host, sendHeader.Uri.Port);

            try
            {
                if (!client.Connected)
                {
                    throw new Exception("client.Connected is false");
                }

                return sendHeader.SSL ? SendSSLRequest(client, sendBody, sendHeader, write) : SendNoSSLRequest(client, sendBody, sendHeader, write);
            }
            finally
            {
                client.Close();
            }
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="request"></param>
        /// <param name="filename"></param>
        /// <param name="KEYS"></param>
        /// <returns></returns>
        public LxwResponse SendUpload(string request, string filename, Dictionary<string, string> KEYS = null)
        {
            var body = new LxwUploadBody(Encoding, filename, KEYS);
            //var sendBody = FormatBody(body, KEYS);
            var sendHeader = FormatHeader(request, body.Body, KEYS, body.Boundary);

            Console.WriteLine(sendHeader.Uri.ToString());
            TcpClient client = new TcpClient(sendHeader.Uri.Host, sendHeader.Uri.Port);

            try
            {
                if (!client.Connected)
                {
                    throw new Exception("client.Connected is false");
                }

                return sendHeader.SSL ? SendSSLRequest(client, body.Body, sendHeader, true) : SendNoSSLRequest(client, body.Body, sendHeader, true);
            }
            finally
            {
                client.Close();
            }
        }

        /// <summary>
        /// SSL 请求
        /// </summary>
        /// <param name="client"></param>
        /// <param name="sendBody"></param>
        /// <param name="sendHeader"></param>
        /// <returns></returns>
        LxwResponse SendSSLRequest(TcpClient client, byte[] sendBody, LxwRequestHeader sendHeader, bool write = false)
        {
            SslStream sslStream = new SslStream(client.GetStream(), true,
                new RemoteCertificateValidationCallback((sender, certificate, chain, sslPolicyErrors) => sslPolicyErrors == SslPolicyErrors.None), null);

            sslStream.ReadTimeout = ReadTimeOut * 1000;
            sslStream.WriteTimeout = WriteTimeOut * 1000;

            X509Store store = new X509Store(StoreName.My);

            sslStream.AuthenticateAsClient(
                sendHeader.Uri.Host,
                store.Certificates,
                System.Security.Authentication.SslProtocols.Default,
                false);

            if (!sslStream.IsAuthenticated)
            {
                throw new Exception("sslStream.IsAuthenticated is false");
            }

            if (write)
            {
                var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_ssl.data");
                FileStream fs = new FileStream(file, FileMode.CreateNew);
                fs.Write(sendHeader.HeaderByte, 0, sendHeader.HeaderByte.Length);
                fs.Write(LINE, 0, LINE.Length);
                fs.Write(LINE, 0, LINE.Length);
                if (sendBody != null)
                {
                    fs.Write(sendBody, 0, sendBody.Length);
                }

                fs.Write(LINE, 0, LINE.Length);
                fs.Close();
            }
            {
                sslStream.Write(sendHeader.HeaderByte);
                sslStream.Write(LINE);
                sslStream.Write(LINE);
                if (sendBody != null)
                {
                    sslStream.Write(sendBody);
                }

                sslStream.Write(LINE);
            }

            sslStream.Flush();

            return ReadResponse(sslStream);
        }

        /// <summary>
        /// 普通请求
        /// </summary>
        /// <param name="client"></param>
        /// <param name="sendBody"></param>
        /// <param name="sendHeader"></param>
        /// <returns></returns>
        LxwResponse SendNoSSLRequest(TcpClient client, byte[] sendBody, LxwRequestHeader sendHeader, bool write = false)
        {
            NetworkStream stream = client.GetStream();

            stream.ReadTimeout = WriteTimeOut * 1000;
            stream.WriteTimeout = WriteTimeOut * 1000;


            if (write)
            {
                var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_nossl.data");
                FileStream fs = new FileStream(file, FileMode.CreateNew);
                fs.Write(sendHeader.HeaderByte, 0, sendHeader.HeaderByte.Length);
                fs.Write(LINE, 0, LINE.Length);
                fs.Write(LINE, 0, LINE.Length);
                if (sendBody != null)
                {
                    fs.Write(sendBody, 0, sendBody.Length);
                }

                fs.Write(LINE, 0, LINE.Length);
                fs.Close();
            }

            stream.Write(sendHeader.HeaderByte, 0, sendHeader.HeaderByte.Length);
            stream.Write(LINE, 0, LINE.Length);
            stream.Write(LINE, 0, LINE.Length);

            if (sendBody != null)
            {
                stream.Write(sendBody, 0, sendBody.Length);
            }

            stream.Write(LINE, 0, LINE.Length);


            stream.Flush();

            return ReadResponse(stream);
        }

        class TaskArguments
        {
            public TaskArguments(CancellationTokenSource cancelSource, Stream sm)
            {
                CancelSource = cancelSource;
                Stream = sm;
            }
            public CancellationTokenSource CancelSource { get; private set; }
            public Stream Stream { get; private set; }
        }

        void CookieMethod(string cookie)
        {
            //Set-Cookie: wxsid=; Domain=wx.qq.com; Path=/; Expires=Thu, 01-Jan-1970 00:00:30 GMT
            LxwCookie cook = new LxwCookie();
            foreach (var o in cookie.Split(';'))
            {
                var arr = o.Split('=');
                if (arr.Length != 2)
                {
                    continue;
                }

                if (arr[0].ToLower().Contains("domain"))
                {
                    cook.Domain = arr[1].Trim();
                    continue;
                }


                if (arr[0].ToLower().Contains("path"))
                {
                    cook.Path = arr[1].Trim();
                    continue;
                }


                if (arr[0].ToLower().Contains("expires"))
                {
                    cook.Expires = arr[1].Trim();
                    continue;
                }

                if (arr[0].Trim() != "")
                {
                    cook.Key = arr[0].Trim();
                    cook.Value = arr[1].Trim();
                    continue;
                }
            }

            if (!string.IsNullOrEmpty(cook.Key))
            {
                LST_COOKIE[cook.Key] = cook;
            }
        }

        private LxwResponse ReadResponse(Stream sm)
        {
            CancellationTokenSource cancelSource = new CancellationTokenSource();
            Task<string> myTask = Task.Factory.StartNew<string>(
                new Func<object, string>(ReadHeaderProcess),
                new TaskArguments(cancelSource, sm),
                cancelSource.Token);

            LxwResponse response = null;
            if (myTask.Wait(ReadTimeOut * 1000)) //尝试3秒时间读取协议头
            {
                //头部信息
                string header = myTask.Result;

                if (!string.IsNullOrEmpty(header))
                {
                    //代表继续接收信息，这部分信息先不考虑处理
                    if (header.StartsWith("HTTP/1.1 100"))
                    {
                        return ReadResponse(sm);
                    }

                    //处理头文件信息
                    var responseHeader = new LxwResponseHeader(header, CookieMethod);

                    while (true)
                    {
                        if (responseHeader.ContentLength > 0)
                        {
                            var buff = new byte[responseHeader.ContentLength];
                            int inread = sm.Read(buff, 0, buff.Length);
                            while (inread < buff.Length)
                            {
                                inread += sm.Read(buff, inread, buff.Length - inread);
                            }
                            response = new LxwResponse(buff, responseHeader);
                            break;
                        }

                        if (responseHeader.TransferEncoding)
                        {
                            var buff = ChunkedReadResponse(sm);
                            response = new LxwResponse(buff, responseHeader);
                            break;
                        }

                        {
                            //如果走到这里，就比较特殊了，可以不考虑
                            var buff = SpecialReadResponse(sm);
                            response = new LxwResponse(buff, responseHeader);
                            break;
                        }
                    }
                }
            }
            else
            {
                cancelSource.Cancel(); //超时的话，别忘记取消任务哦
            }
            return response;
        }

        /// <summary>
        /// 这里不是通用的，根据自己的情况调整。
        /// </summary>
        /// <param name="sm"></param>
        /// <returns></returns>
        byte[] SpecialReadResponse(Stream sm)
        {
            ArrayList array = new ArrayList();
            StringBuilder bulider = new StringBuilder();
            int length = 0;
            DateTime now = DateTime.Now;
            while (true)
            {
                byte[] buff = new byte[1024 * 10];
                int len = sm.Read(buff, 0, buff.Length);
                if (len > 0)
                {
                    length += len;
                    byte[] reads = new byte[len];
                    Array.Copy(buff, 0, reads, 0, len);
                    array.Add(reads);
                    bulider.Append(Encoding.GetString(reads));
                }
                else
                {
                    break;
                }

                //string temp = bulider.ToString();
                //if (temp.ToUpper().Contains("</HTML>"))
                //{
                //    break;
                //}
                if (DateTime.Now.Subtract(now).TotalSeconds >= 30)
                {
                    break;//超时30秒则跳出
                }
            }
            byte[] bytes = new byte[length];
            int index = 0;
            for (int i = 0; i < array.Count; i++)
            {
                byte[] temp = (byte[])array[i];
                Array.Copy(temp, 0, bytes,
                    index, temp.Length);
                index += temp.Length;
            }
            return bytes;
        }

        /// <summary>
        /// 读取Response 头部信息
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        string ReadHeaderProcess(object args)
        {
            TaskArguments argument = args as TaskArguments;
            StringBuilder bulider = new StringBuilder();
            if (argument != null)
            {
                Stream sm = argument.Stream;
                while (!argument.CancelSource.IsCancellationRequested)
                {
                    try
                    {
                        int read = sm.ReadByte();
                        if (read != -1)
                        {
                            byte b = (byte)read;
                            bulider.Append((char)b);
                            string temp = bulider.ToString();
                            //Http协议头尾
                            if (temp.EndsWith("\r\n\r\n"))
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                        break;
                    }
                }
            }
            return bulider.ToString();
        }

        byte[] LINE
        {
            get
            {
                return Encoding.GetBytes("\r\n");
            }
        }

        byte[] ChunkedReadResponse(Stream sm)
        {
            ArraySegmentList<byte> arraySegmentList = new ArraySegmentList<byte>();
            int chunked = GetChunked(sm);
            while (chunked > 0)
            {
                byte[] buff = new byte[chunked];
                try
                {
                    int inread = sm.Read(buff, 0, buff.Length);
                    while (inread < buff.Length)
                    {
                        inread += sm.Read(buff, inread, buff.Length - inread);
                    }
                    arraySegmentList.Add(new ArraySegment<byte>(buff));
                    if (sm.ReadByte() != -1)//读取段末尾的\r\n
                    {
                        sm.ReadByte();
                    }
                }
                catch (Exception)
                {
                    break;
                }
                chunked = GetChunked(sm);
            }
            return arraySegmentList.ToArray();
        }

        int GetChunked(Stream sm)
        {
            int chunked = 0;
            StringBuilder bulider = new StringBuilder();
            while (true)
            {
                try
                {
                    int read = sm.ReadByte();
                    if (read != -1)
                    {
                        byte b = (byte)read;
                        bulider.Append((char)b);
                        string temp = bulider.ToString();
                        if (temp.EndsWith("\r\n"))
                        {
                            chunked = Convert.ToInt32(temp.Trim(), 16);
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    break;
                }
            }
            return chunked;
        }

        LxwRequestHeader FormatHeader(string request, byte[] body = null, Dictionary<string, string> KEYS = null, string boundary = null)
        {
            if (request == null)
            {
                throw new ArgumentNullException("byte[] FormatHeader(string request), request is null");
            }

            request = FormatKeys(request, KEYS);

            LxwRequestHeader req = new LxwRequestHeader(Encoding);
            var bodys = Regex.Split(request, "\r\n");
            var list = new List<string>();
            var start = 0;
            var end = bodys.Length - 1;
            {
                for (; start < bodys.Length; start++)
                {
                    if (bodys[start] != "")
                    {
                        break;
                    }
                }
                for (; end > start; end--)
                {
                    if (bodys[end] != "")
                    {
                        break;
                    }
                }

                for (; start <= end; start++)
                {
                    if (bodys[start].StartsWith("Content-Length", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    if (bodys[start].StartsWith("Cookie:", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    //Content-Type: multipart/form-data; boundary=----WebKitFormBoundaryuM55vqcAjmXSlVHa
                    if (bodys[start].ToLower().Contains("multipart/form-data"))
                    {
                        if (!string.IsNullOrEmpty(boundary))
                        {
                            bodys[start] = "Content-Type: multipart/form-data; boundary=" + boundary;
                        }
                        list.Add(bodys[start]);
                        continue;
                    }

                    {
                        //这里还可以扩充
                        var arr = bodys[start].Split(' ');
                        if (arr[0] == "GET" ||
                            arr[0] == "POST" ||
                            arr[0] == "OPTIONS")
                        {
                            req.SSL = arr[1].StartsWith("https://", StringComparison.OrdinalIgnoreCase);

                            req.Uri = new Uri(arr[1]);
                            //if (string.IsNullOrEmpty(boundary))
                            arr[1] = req.Uri.PathAndQuery;

                            bodys[start] = string.Join(" ", arr);
                        }
                    }

                    list.Add(bodys[start]);
                }

                if (body != null)
                {
                    list.Add("Content-Length: " + body.Length);
                }

                //if (string.IsNullOrEmpty(boundary))
                if (LST_COOKIE.Count > 0)
                {
                    list.Add("Cookie: " + CreateCookies());
                }



                //输出信息
                Debug.WriteLine(string.Join("\r\n", list.ToArray()));

                req.Header = string.Join("\r\n", list.ToArray());
                //req.HeaderByte
            }

            //返回
            return req;
        }

        /// <summary>
        /// 生成cookies
        /// </summary>
        /// <returns></returns>
        string CreateCookies()
        {
            List<string> lst = new List<string>();
            foreach (var o in LST_COOKIE)
            {
                if (o.Key != "")
                {
                    lst.Add(o.Value.ToString());
                }
            }
            return string.Join(";", lst.ToArray());
        }

        byte[] FormatBody(string body = null, Dictionary<string, string> KEYS = null)
        {
            if (body == null)
            {
                return null;
            }

            body = FormatKeys(body, KEYS);

            var bodys = Regex.Split(body, "\r\n");
            var list = new List<string>();
            var start = 0;
            var end = bodys.Length - 1;
            {
                for (; start < bodys.Length; start++)
                {
                    if (bodys[start] != "")
                    {
                        break;
                    }
                }
                for (; end > start; end--)
                {
                    if (bodys[end] != "")
                    {
                        break;
                    }
                }

                for (; start <= end; start++)
                {
                    list.Add(bodys[start]);
                }
            }

            //返回
            return Encoding.GetBytes(string.Join("\r\n", list.ToArray()));
        }

        /// <summary>
        /// 格式化信息
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public string FormatKeys(string body, Dictionary<string, string> KEYS = null)
        {
            if (KEYS != null)
            {
                foreach (string key in KEYS.Keys)
                {
                    body = body.Replace("{" + key.ToUpper() + "}", KEYS[key]);
                }
            }

            foreach (string key in DI_KEYS.Keys)
            {
                body = body.Replace("{" + key.ToUpper() + "}", DI_KEYS[key]);
            }

            foreach (var cookies in LST_COOKIE.Values)
            {
                if (cookies.Key != "")
                {
                    body = body.Replace("{" + cookies.Key.ToUpper() + "}", cookies.Value);
                }
            }

            //生成时间
            body = body.Replace("{TIME}", DateTimeToStamp(DateTime.Now) + "");

            return body;
        }

        int DateTimeToStamp(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }
        /// <summary>
        /// 设置超时时间
        /// </summary>
        public int WriteTimeOut { get; set; }

        /// <summary>
        /// 默认30秒
        /// </summary>
        public int ReadTimeOut { get; set; }
    }



}