using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;


namespace demo_win_httpsocket
{
    public class kyfw12306
    {
        public static object[] TrainsDetails(string date = "2019-10-01")
        {
            var random = new Random();
            RootObject[] values;
            while ((values = GetLeftTickets(date)) == null)
            // while ((values = Test()) == null)
            {
                Thread.Sleep(TimeSpan.FromSeconds(random.Next(1, 5)));
            }


            var tour_flag = "";
            var trains = values
                // .Where(v => new[] { 'G', 'D' }.Contains(v.queryLeftNewDTO.station_train_code[0])) // 只看高铁或动车
                .Select(train =>
                {
                    var t = train.queryLeftNewDTO;
                    var t1 = t.start_time.Split(':').Select(int.Parse).ToArray();
                    var t2 = t.lishi.Split(':').Select(int.Parse).ToArray();
                    return new
                    {
                        日期 = date,
                        车次 = t.station_train_code,
                        出发站 = t.from_station_name,
                        到达站 = t.to_station_name,
                        出发时间 = t.start_time,
                        到达时间 = t.arrive_time,
                        历时 = t.lishi + "\t" + new[] { "当", "一", "二", "三", "四", "五", "六", "七", "八", "九", "十", }[(t1[0] + t2[0] + (t1[1] + t2[1]) / 60) / 24] + "日到达",
                        商务座特等座 = (!string.IsNullOrEmpty(t.swz_num) && t.swz_num != "--" && t.swz_num != "0" && t.swz_num != "无")
                            ? cs(t.swz_num, "SWZ_", t.train_no, t.yp_ex, "91", t.controlled_train_flag, train)
                            : !string.IsNullOrEmpty(t.tz_num) && t.tz_num != "--" && t.tz_num != "0" && t.tz_num != "无"
                                ? cs(t.tz_num, "TZ_", t.train_no, t.yp_ex, "P1", t.controlled_train_flag, train)
                                : !string.IsNullOrEmpty(t.swz_num) && t.swz_num == "无"
                                    ? cs(t.swz_num, "SWZ_", t.train_no, t.yp_ex, "91", t.controlled_train_flag, train)
                                : cs(t.tz_num, "TZ_", t.train_no, t.yp_ex, "P1", t.controlled_train_flag, train),
                        一等座 = cs(t.zy_num, "ZY_", t.train_no, t.yp_ex, "M1", t.controlled_train_flag, train),
                        二等座 = cs(t.ze_num, "ZE_", t.train_no, t.yp_ex, "O1", t.controlled_train_flag, train),
                        高级软卧 = cs(t.gr_num, "GR_", t.train_no, t.yp_ex, "61", t.controlled_train_flag, train),
                        软卧一等卧 = cs(t.rw_num, "RW_", t.train_no, t.yp_ex, "41", t.controlled_train_flag, train),
                        动卧 = cs(t.srrb_num, "SRRB_", t.train_no, t.yp_ex, "F1", t.controlled_train_flag, train),
                        硬卧二等卧 = cs(t.yw_num, "YW_", t.train_no, t.yp_ex, "31", t.controlled_train_flag, train),
                        软座 = cs(t.rz_num, "RZ_", t.train_no, t.yp_ex, "21", t.controlled_train_flag, train),
                        硬座 = cs(t.yz_num, "YZ_", t.train_no, t.yp_ex, "11", t.controlled_train_flag, train),
                        无座 = cs(t.wz_num, "WZ_", t.train_no, t.yp_ex, "W1", t.controlled_train_flag, train),
                        其它 = cs(t.qt_num, "QT_", t.train_no, t.yp_ex, "", t.controlled_train_flag, train),

                        备注 = t.canWebBuy == "Y" ? "预约" + (t.exchange_train_flag == "1" ? "(兑)" : "") : train.buttonTextInfo,
                    };
                })
                .ToArray();
            return trains;

            string cs(string c6, string c2, string cX, string c3, string cU, string cR, RootObject cT)
            {
                var cN = cT.queryLeftNewDTO;
                var cY = b0(date + " " + cN.start_time);
                if (string.IsNullOrEmpty(cT.secretStr) || "null" == cT.secretStr)
                {
                    cY = false;
                }
                if ("1" != cN.houbu_train_flag)
                {
                    cY = false;
                }
                if (tour_flag == "fc" || tour_flag == "gc")
                {
                    cY = false;
                }
                if ("无" == c6 && "WZ_" != c2 && "QT_" != c2 && cY)
                {
                    c6 = "候补";
                }
                return c6;
            }

            bool b0(string cQ)
            {
                try
                {
                    var cM = DateTime.Now.AddHours(3);
                    var cO = cM.Ticks;
                    var cP = DateTime.Parse(cQ).Ticks;
                    if (cP <= cO)
                    {
                        return false;
                    }
                }
                catch { }
                return true;
            }
        }

        private static RootObject[] GetLeftTickets(string date)
        {
            // 参考链接 https://blog.csdn.net/xiahn1a/article/details/42584507
            var url = $@"https://kyfw.12306.cn/otn/leftTicket/queryA?leftTicketDTO.train_date={date}&leftTicketDTO.from_station=BJP&leftTicketDTO.to_station=CDW&purpose_codes=ADULT";
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.CookieContainer = new CookieContainer();
            request.ProtocolVersion = HttpVersion.Version11;
            request.Accept = @"*/*";
            request.UserAgent = @"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36";
            request.Referer = @"https://kyfw.12306.cn/otn/leftTicket/init";
            request.ContentType = @"application/json;charset=UTF-8";
            request.Method = "GET";
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    string res = reader.ReadToEnd();
                    try
                    {
                        var obj = JObject.Parse(res);
                        var cO = obj["data"]["result"].ToObject<string[]>();
                        var cQ = obj["data"]["map"];
                        var values = cp(cO, cQ);
                        return values.Select(v => v.ToObject<RootObject>()).ToArray();
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }

        private static JObject[] cp(string[] cO, JToken cQ)
        {
            var cN = new JObject[cO.Length];
            for (var cM = 0; cM < cO.Length; cM++)
            {
                var cR = new JObject();
                var cL = cO[cM].Split('|');
                cR["secretStr"] = cL[0];
                cR["buttonTextInfo"] = cL[1];
                var cP = new JObject();
                cP["train_no"] = cL[2];
                cP["station_train_code"] = cL[3];
                cP["start_station_telecode"] = cL[4];
                cP["end_station_telecode"] = cL[5];
                cP["from_station_telecode"] = cL[6];
                cP["to_station_telecode"] = cL[7];
                cP["start_time"] = cL[8];
                cP["arrive_time"] = cL[9];
                cP["lishi"] = cL[10];
                cP["canWebBuy"] = cL[11];
                cP["yp_info"] = cL[12];
                cP["start_train_date"] = cL[13];
                cP["train_seat_feature"] = cL[14];
                cP["location_code"] = cL[15];
                cP["from_station_no"] = cL[16];
                cP["to_station_no"] = cL[17];
                cP["is_support_card"] = cL[18];
                cP["controlled_train_flag"] = cL[19];
                cP["gg_num"] = cL[20] ?? "--";
                cP["gr_num"] = cL[21] ?? "--";
                cP["qt_num"] = cL[22] ?? "--";
                cP["rw_num"] = cL[23] ?? "--";
                cP["rz_num"] = cL[24] ?? "--";
                cP["tz_num"] = cL[25] ?? "--";
                cP["wz_num"] = cL[26] ?? "--";
                cP["yb_num"] = cL[27] ?? "--";
                cP["yw_num"] = cL[28] ?? "--";
                cP["yz_num"] = cL[29] ?? "--";
                cP["ze_num"] = cL[30] ?? "--";
                cP["zy_num"] = cL[31] ?? "--";
                cP["swz_num"] = cL[32] ?? "--";
                cP["srrb_num"] = cL[33] ?? "--";
                cP["yp_ex"] = cL[34];
                cP["seat_types"] = cL[35];
                cP["exchange_train_flag"] = cL[36];
                cP["houbu_train_flag"] = cL[37];
                if (cL.Length > 38)
                {
                    cP["houbu_seat_limit"] = cL[38];

                }
                cP["from_station_name"] = cQ[cL[6]];
                cP["to_station_name"] = cQ[cL[7]];
                cR["queryLeftNewDTO"] = cP;
                cN[cM] = cR;
            }
            return cN;
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受  
        }

        public class RootObject
        {
            public string secretStr { get; set; }
            public string buttonTextInfo { get; set; }
            public QueryLeftNewDTO queryLeftNewDTO { get; set; }
        }

        public class QueryLeftNewDTO
        {
            public string train_no { get; set; }
            public string station_train_code { get; set; }
            public string start_station_telecode { get; set; }
            public string end_station_telecode { get; set; }
            public string from_station_telecode { get; set; }
            public string to_station_telecode { get; set; }
            public string start_time { get; set; }
            public string arrive_time { get; set; }
            public string lishi { get; set; }
            public string canWebBuy { get; set; }
            public string yp_info { get; set; }
            public string start_train_date { get; set; }
            public string train_seat_feature { get; set; }
            public string location_code { get; set; }
            public string from_station_no { get; set; }
            public string to_station_no { get; set; }
            public string is_support_card { get; set; }
            public string controlled_train_flag { get; set; }

            public string swz_num { get; set; } // 商务座
            public string zy_num { get; set; }  // 一等座
            public string ze_num { get; set; }  // 二等座
            public string gr_num { get; set; }  // 高级软卧
            public string rw_num { get; set; }  // 软卧一等卧
            public string srrb_num { get; set; }// 动卧
            public string yw_num { get; set; }  // 硬卧二等卧
            public string rz_num { get; set; }  // 软卧
            public string yz_num { get; set; }  // 硬座
            public string wz_num { get; set; }  // 无座
            public string qt_num { get; set; }  // 其它


            public string gg_num { get; set; }
            public string tz_num { get; set; }
            public string yb_num { get; set; }
            public string yp_ex { get; set; }
            public string seat_types { get; set; }
            public string exchange_train_flag { get; set; }
            public string houbu_train_flag { get; set; }
            public string houbu_seat_limit { get; set; }
            public string from_station_name { get; set; }
            public string to_station_name { get; set; }
        }
    }
}