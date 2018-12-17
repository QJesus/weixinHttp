using HttpSocket;
using System.IO;

namespace wx_logic
{
    public partial class WXLogic
    {
        void _11_SENDFILE(string userId, string filename)
        {
            ShowMessage(System.Reflection.MethodBase.GetCurrentMethod().Name, null);

            var MediaId = _17_WEBWXUPLOADMEDIA(filename);

            var type = MimeMapping.GetMimeMappingByFile(filename).ToLower();
            //if(type.StartsWith("audio/"))
            //{

            //}
            if (type.StartsWith("image/"))
            {
                //·¢ËÍÍ¼Æ¬
                _15_WEBWXSENDEMOTICON(userId, LoginUser.UserName, MediaId, 47);
            }
            else
            {
                var Message = "<appmsg appid='" + WEB["APPID"] + "' sdkver=''><title>{filename}</title><des></des><action></action><type>6</type><content></content><url></url><lowurl></lowurl><appattach><totallen>{filelength}</totallen><attachid>{attachid}</attachid><fileext>{filetype}</fileext></appattach><extinfo></extinfo></appmsg>";
                Message = Message.Replace("{attachid}", MediaId);
                Message = Message.Replace("{filename}", Path.GetFileName(filename));
                Message = Message.Replace("{filelength}", "" + File.ReadAllBytes(filename).Length);
                Message = Message.Replace("{filetype}", Path.GetExtension(filename).TrimStart(new char[] { '.' }));

                _16_WEBWXSENDAPPMSG(userId, LoginUser.UserName, Message, 6);
            }
        }
    }
}