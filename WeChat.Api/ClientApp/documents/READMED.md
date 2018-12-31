# 微信网页版接口

## 1. jslogin

``` http
GET https://login.wx.qq.com/jslogin?appid=wx782c26e4c19acffb&redirect_uri=https%3A%2F%2Fwx.qq.com%2Fcgi-bin%2Fmmwebwx-bin%2Fwebwxnewloginpage&fun=new&lang=zh_CN&_=1546211298739 HTTP/1.1
Host: login.wx.qq.com
Connection: keep-alive
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36
Accept: */*
Referer: https://wx.qq.com/?lang=zh_CN
Accept-Encoding: gzip, deflate, br
Accept-Language: zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7
Cookie: mm_lang=zh_CN

HTTP/1.1 200 OK
Connection: keep-alive
Content-Type: text/javascript
Content-Type: text/html; charset=gbk
Cache-Control: no-cache, must-revalidate
Content-Length: 64

window.QRLogin.code = 200; window.QRLogin.uuid = "gbyKnbsmqQ==";
```

## 2. qrcode

``` http
GET https://login.weixin.qq.com/qrcode/gbyKnbsmqQ== HTTP/1.1
Host: login.weixin.qq.com
Connection: keep-alive
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36
Accept: image/webp,image/apng,image/*,*/*;q=0.8
Referer: https://wx.qq.com/?lang=zh_CN
Accept-Encoding: gzip, deflate, br
Accept-Language: zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7

HTTP/1.1 200 OK
Connection: keep-alive
Content-Type: image/jpeg
Content-Length: 38654

data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQABAAD......
```

## 3. login

``` http
GET https://login.wx.qq.com/cgi-bin/mmwebwx-bin/login?loginicon=true&uuid=gbyKnbsmqQ==&tip=1&r=-23073595&_=1546211298740 HTTP/1.1
Host: login.wx.qq.com
Connection: keep-alive
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36
Accept: */*
Referer: https://wx.qq.com/?lang=zh_CN
Accept-Encoding: gzip, deflate, br
Accept-Language: zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7
Cookie: mm_lang=zh_CN

HTTP/1.1 200 OK
Connection: keep-alive
Content-Type: text/javascript
Content-Length: 16

window.code=408;
####################################################################################################################################
GET https://login.wx.qq.com/cgi-bin/mmwebwx-bin/login?loginicon=true&uuid=gbyKnbsmqQ==&tip=0&r=-23226313&_=1546211298746 HTTP/1.1
Host: login.wx.qq.com
Connection: keep-alive
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36
Accept: */*
Referer: https://wx.qq.com/?lang=zh_CN
Accept-Encoding: gzip, deflate, br
Accept-Language: zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7
Cookie: mm_lang=zh_CN

HTTP/1.1 200 OK
Connection: keep-alive
Content-Type: text/javascript
Content-Length: 5711

window.code=201;window.userAvatar = 'data:img/jpg;base64,/9j/4AAQSkZJRgABAQEASABIAAD......';
####################################################################################################################################
GET https://login.wx.qq.com/cgi-bin/mmwebwx-bin/login?loginicon=true&uuid=gbyKnbsmqQ==&tip=0&r=-23291087&_=1546211298749 HTTP/1.1
Host: login.wx.qq.com
Connection: keep-alive
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36
Accept: */*
Referer: https://wx.qq.com/?lang=zh_CN
Accept-Encoding: gzip, deflate, br
Accept-Language: zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7
Cookie: mm_lang=zh_CN

HTTP/1.1 200 OK
Connection: keep-alive
Content-Type: text/javascript
Content-Length: 183

window.code=200;window.redirect_uri="https://wx.qq.com/cgi-bin/mmwebwx-bin/webwxnewloginpage?ticket=AaXdHpmbWGYo_ZOk7ylAnnCY@qrticket_0&uuid=gbyKnbsmqQ==&lang=zh_CN&scan=1546211468";
```

## 4. webwxnewloginpage

``` http
GET https://wx.qq.com/cgi-bin/mmwebwx-bin/webwxnewloginpage?ticket=AaXdHpmbWGYo_ZOk7ylAnnCY@qrticket_0&uuid=gbyKnbsmqQ==&lang=zh_CN&scan=1546211468&fun=new&version=v2&lang=zh_CN HTTP/1.1
Host: wx.qq.com
Connection: keep-alive
Accept: application/json, text/plain, */*
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36
Referer: https://wx.qq.com/?lang=zh_CN
Accept-Encoding: gzip, deflate, br
Accept-Language: zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7
Cookie: mm_lang=zh_CN; MM_WX_NOTIFY_STATE=1; MM_WX_SOUND_STATE=1

HTTP/1.1 200 OK
Connection: keep-alive
Content-Type: text/plain;charset=utf-8
Set-Cookie: wxuin=176666155; Domain=wx.qq.com; Path=/; Expires=Mon, 31-Dec-2018 11:12:21 GMT; Secure
Set-Cookie: wxsid=5YpLHmVgWwH5LxN1; Domain=wx.qq.com; Path=/; Expires=Mon, 31-Dec-2018 11:12:21 GMT; Secure
Set-Cookie: wxloadtime=1546211541; Domain=wx.qq.com; Path=/; Expires=Mon, 31-Dec-2018 11:12:21 GMT; Secure
Set-Cookie: mm_lang=zh_CN; Domain=wx.qq.com; Path=/; Expires=Mon, 31-Dec-2018 11:12:21 GMT; Secure
Set-Cookie: webwx_data_ticket=gSehWtnDWTMh38gmz0IjaFv0; Domain=.qq.com; Path=/; Expires=Mon, 31-Dec-2018 11:12:21 GMT; Secure
Set-Cookie: webwxuvid=fcc96ffbc5c1f0c49dba1a84cc9bceee1b7ea37d1100e803e3b4ff6f6ddecca5d210211a325524605501164faa3a5a61; Domain=wx.qq.com; Path=/; Expires=Wed, 27-Dec-2028 23:12:21 GMT; Secure
Set-Cookie: webwx_auth_ticket=CIsBENjn/8ICGoABHRBIWkIstAR4f6vOhj3FDD/Yge5pisSrRuwZeblgB/4ltMu/WAwqySrzD4zsO7ADs9JpDrMyS+rxsLEgoVGaJbhb5iMyGRjTEOdP+ohY9S+6aZh8ADk/RdxE3SrHB5cNDdDw0t9ZAUx2fa35DiM8pGFJQa+5P7PqennD1Lsl3P8=; Domain=wx.qq.com; Path=/; Expires=Wed, 27-Dec-2028 23:12:21 GMT; Secure
Strict-Transport-Security: max-age=31536000 ; includeSubDomains
Content-Encoding: gzip
Content-Length: 224

<error><ret>0</ret><message></message><skey>@crypt_63d18dea_a7de6934c67e2f89ac399fa35b689a50</skey><wxsid>5YpLHmVgWwH5LxN1</wxsid><wxuin>176666155</wxuin><pass_ticket>kbmy5Jt9nj%2BJzBaJZYNpUBQyBwRTejeXq1sM%2FYH7iOI%3D</pass_ticket><isgrayscale>1</isgrayscale></error>
```

## 5. webwxinit

``` http
POST https://wx.qq.com/cgi-bin/mmwebwx-bin/webwxinit?r=-23072231&lang=zh_CN&pass_ticket=kbmy5Jt9nj%252BJzBaJZYNpUBQyBwRTejeXq1sM%252FYH7iOI%253D HTTP/1.1
Host: wx.qq.com
Connection: keep-alive
Content-Length: 148
Accept: application/json, text/plain, */*
Origin: https://wx.qq.com
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36
Content-Type: application/json;charset=UTF-8
Referer: https://wx.qq.com/?lang=zh_CN
Accept-Encoding: gzip, deflate, br
Accept-Language: zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7
Cookie: mm_lang=zh_CN; MM_WX_NOTIFY_STATE=1; MM_WX_SOUND_STATE=1; wxuin=176666155; wxsid=5YpLHmVgWwH5LxN1; wxloadtime=1546211541; webwx_data_ticket=gSehWtnDWTMh38gmz0IjaFv0; webwxuvid=fcc96ffbc5c1f0c49dba1a84cc9bceee1b7ea37d1100e803e3b4ff6f6ddecca5d210211a325524605501164faa3a5a61; webwx_auth_ticket=CIsBENjn/8ICGoABHRBIWkIstAR4f6vOhj3FDD/Yge5pisSrRuwZeblgB/4ltMu/WAwqySrzD4zsO7ADs9JpDrMyS+rxsLEgoVGaJbhb5iMyGRjTEOdP+ohY9S+6aZh8ADk/RdxE3SrHB5cNDdDw0t9ZAUx2fa35DiM8pGFJQa+5P7PqennD1Lsl3P8=; login_frequency=1; last_wxuin=176666155

{"BaseRequest":{"Uin":"176666155","Sid":"5YpLHmVgWwH5LxN1","Skey":"@crypt_63d18dea_a7de6934c67e2f89ac399fa35b689a50","DeviceID":"e940492848928413"}}

HTTP/1.1 200 OK
Connection: keep-alive
Content-Type: text/plain
Strict-Transport-Security: max-age=31536000 ; includeSubDomains
Content-Encoding: gzip
Content-Length: 5252

{
    "BaseResponse": {"Ret": 0,"ErrMsg": ""},
    "Count": 9,
    "ContactList": [{
        "Uin": 0,
        "UserName": "@a53c1fbfce2d6ece47a39329ca2e95071e05cddbd94f0ec3973ffe39a06ccbe6",
        "NickName": "æ¨çˆ½",
        "HeadImgUrl": "/cgi-bin/mmwebwx-bin/webwxgeticon?seq=696335334&username=@a53c1fbfce2d6ece47a39329ca2e95071e05cddbd94f0ec3973ffe39a06ccbe6&skey=@crypt_63d18dea_a7de6934c67e2f89ac399fa35b689a50",
        "ContactFlag": 34819,
        "MemberCount": 0,
        "MemberList": [],
        "RemarkName": "äº²çˆ±çš„å¤§å®è´",
        "HideInputBarFlag": 0,
        "Sex": 0,
        "Signature": "",
        "VerifyFlag": 0,
        "OwnerUin": 0,
        "PYInitial": "YS",
        "PYQuanPin": "yangshuang",
        "RemarkPYInitial": "",
        "RemarkPYQuanPin": "",
        "StarFriend": 0,
        "AppAccountFlag": 0,
        "Statues": 0,
        "AttrStatus": 135461,
        "Province": "",
        "City": "",
        "Alias": "",
        "SnsFlag": 1,
        "UniFriend": 0,
        "DisplayName": "",
        "ChatRoomId": 0,
        "KeyWord": "",
        "EncryChatRoomId": "",
        "IsOwner": 0
    }, {
        "Uin": 0,
        "UserName": "@@35c78783e0906eb43b070ef4425a965cf06933f67cc45a436d89d7e31267348e",
        "NickName": "ç›¸äº²ç›¸çˆ±çš„ä¸€å®¶äºº",
        "HeadImgUrl": "/cgi-bin/mmwebwx-bin/webwxgetheadimg?seq=0&username=@@35c78783e0906eb43b070ef4425a965cf06933f67cc45a436d89d7e31267348e&skey=@crypt_63d18dea_a7de6934c67e2f89ac399fa35b689a50",
        "ContactFlag": 0,
        "MemberCount": 14,
        "MemberList": [{
            "Uin": 0,
            "UserName": "@a53c1fbfce2d6ece47a39329ca2e95071e05cddbd94f0ec3973ffe39a06ccbe6",
            "NickName": "",
            "AttrStatus": 0,
            "PYInitial": "",
            "PYQuanPin": "",
            "RemarkPYInitial": "",
            "RemarkPYQuanPin": "",
            "MemberStatus": 0,
            "DisplayName": "",
            "KeyWord": ""
        }],
        "RemarkName": "",
        "HideInputBarFlag": 0,
        "Sex": 0,
        "Signature": "",
        "VerifyFlag": 0,
        "OwnerUin": 0,
        "PYInitial": "",
        "PYQuanPin": "",
        "RemarkPYInitial": "",
        "RemarkPYQuanPin": "",
        "StarFriend": 0,
        "AppAccountFlag": 0,
        "Statues": 0,
        "AttrStatus": 0,
        "Province": "",
        "City": "",
        "Alias": "",
        "SnsFlag": 0,
        "UniFriend": 0,
        "DisplayName": "",
        "ChatRoomId": 0,
        "KeyWord": "",
        "EncryChatRoomId": "",
        "IsOwner": 0
    }],
    "SyncKey": {"Count": 4,"List": [{"Key": 1,"Val": 698325105}, {"Key": 2,"Val": 698325109}, {"Key": 3,"Val": 698325051}, {"Key": 1000,"Val": 1546210682}]},
    "User": {
        "Uin": 176666155,
        "UserName": "@f0c04abe389590b02d91684d99584439",
        "NickName": "ç§¦ä¹ æ·³",
        "HeadImgUrl": "/cgi-bin/mmwebwx-bin/webwxgeticon?seq=1264610125&username=@f0c04abe389590b02d91684d99584439&skey=@crypt_63d18dea_a7de6934c67e2f89ac399fa35b689a50",
        "RemarkName": "",
        "PYInitial": "",
        "PYQuanPin": "",
        "RemarkPYInitial": "",
        "RemarkPYQuanPin": "",
        "HideInputBarFlag": 0,
        "StarFriend": 0,
        "Sex": 1,
        "Signature": "èµšé’±å…»å®¶",
        "AppAccountFlag": 0,
        "VerifyFlag": 0,
        "ContactFlag": 0,
        "WebWxPluginSwitch": 0,
        "HeadImgFlag": 1,
        "SnsFlag": 17
    },
    "ChatSet": "filehelper,weixin,@d77f22c1444fcbd7092fa2464899391a3cc0d6a9ec79b0770ce91be01a66a09b,@68704825b7c47cc72277145d4e376719,@a53c1fbfce2d6ece47a39329ca2e95071e05cddbd94f0ec3973ffe39a06ccbe6,filehelper,@@0d9ad8ab75097485e5f22cda0d84c837b086361207759fafbbf16316bbd22663,@7d9c6fe675ea3df684a19a3c1c7c44d519bd791985d9e3b221c3c4dea45f3330,@@35c78783e0906eb43b070ef4425a965cf06933f67cc45a436d89d7e31267348e,",
    "SKey": "@crypt_63d18dea_a7de6934c67e2f89ac399fa35b689a50",
    "ClientVersion": 385876258,
    "SystemTime": 1546211541,
    "GrayScale": 1,
    "InviteStartCount": 40,
    "MPSubscribeMsgCount": 1,
    "MPSubscribeMsgList": [{
        "UserName": "@7251035d5a234d3de44ce76437596d65",
        "MPArticleCount": 5,
        "MPArticleList": [{
            "Title": "ä½¿ç”¨PerfViewç›‘æµ‹.NETç¨‹åºæ€§èƒ½ï¼ˆä¸‰ï¼‰ï¼šåˆ†ç»„",
            "Digest": "çŽ°åœ¨æ¥çœ‹çœ‹Perfviewä¸­çš„åˆ†ç»„æ“ä½œï¼ˆGroupingï¼‰ã€‚åˆ†ç»„åŠŸèƒ½éƒ½æ—¨å°†è®°å½•åˆ°çš„å„ç§å‡½æ•°è°ƒç”¨å †æ ˆä»¥æŒ‡å®šçš„è§„åˆ™è¿›è¡Œåˆ†ç»„ï¼Œå¸®åŠ©ä½ ç»„ç»‡å’Œæ‰¾åˆ°æ›´å…³å¿ƒçš„æ•°æ®ã€‚",
            "Cover": "http://mmbiz.qpic.cn/mmbiz_jpg/gak2lhVxV6Lpv858kH5UED4gtHkH1aByyAwJRxILlkfbGy68AlaXtpwvKzkEBf2J4h1rmxT1I4ibqYXafqUJicMQ/300?wxtype=jpeg&wxfrom=0",
            "Url": "http://mp.weixin.qq.com/s?__biz=MzAwNTMxMzg1MA==&mid=2654073443&idx=4&sn=f515a48c231bbce1a87349f44d3047bb&chksm=80dbd636b7ac5f207843f9e462a74f32f5daae066f38011c733e9f48fdd9798af1424c32dccc&scene=0#rd"
        }],
        "Time": 1546210870,
        "NickName": "dotNETè·¨å¹³å°"
    }],
    "ClickReportInterval": 600000
}
```

## 6. webwxstatusnotify

```http
POST /cgi-bin/mmwebwx-bin/webwxstatusnotify?lang=zh_CN&pass_ticket=kbmy5Jt9nj%252BJzBaJZYNpUBQyBwRTejeXq1sM%252FYH7iOI%253D HTTP/1.1
Host: wx.qq.com
Connection: keep-alive
Content-Length: 283
Accept: application/json, text/plain, */*
Origin: https://wx.qq.com
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36
Content-Type: application/json;charset=UTF-8
Referer: https://wx.qq.com/?lang=zh_CN
Accept-Encoding: gzip, deflate, br
Accept-Language: zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7
Cookie: mm_lang=zh_CN; MM_WX_NOTIFY_STATE=1; MM_WX_SOUND_STATE=1; wxuin=176666155; wxsid=5YpLHmVgWwH5LxN1; wxloadtime=1546211541; webwx_data_ticket=gSehWtnDWTMh38gmz0IjaFv0; webwxuvid=fcc96ffbc5c1f0c49dba1a84cc9bceee1b7ea37d1100e803e3b4ff6f6ddecca5d210211a325524605501164faa3a5a61; webwx_auth_ticket=CIsBENjn/8ICGoABHRBIWkIstAR4f6vOhj3FDD/Yge5pisSrRuwZeblgB/4ltMu/WAwqySrzD4zsO7ADs9JpDrMyS+rxsLEgoVGaJbhb5iMyGRjTEOdP+ohY9S+6aZh8ADk/RdxE3SrHB5cNDdDw0t9ZAUx2fa35DiM8pGFJQa+5P7PqennD1Lsl3P8=; login_frequency=1; last_wxuin=176666155

{"BaseRequest":{"Uin":176666155,"Sid":"5YpLHmVgWwH5LxN1","Skey":"@crypt_63d18dea_a7de6934c67e2f89ac399fa35b689a50","DeviceID":"e435716924360955"},"Code":3,"FromUserName":"@f0c04abe389590b02d91684d99584439","ToUserName":"@f0c04abe389590b02d91684d99584439","ClientMsgId":1546211539889}

HTTP/1.1 200 OK
Connection: keep-alive
Content-Type: text/plain
Strict-Transport-Security: max-age=31536000 ; includeSubDomains
Content-Length: 80

{"BaseResponse": {"Ret": 0,"ErrMsg": ""},"MsgID": "8032721323945545465"}
```

## 7. webwxgeticon

``` http
GET https://wx.qq.com/cgi-bin/mmwebwx-bin/webwxgeticon?seq=696335334&username=@a53c1fbfce2d6ece47a39329ca2e95071e05cddbd94f0ec3973ffe39a06ccbe6&skey=@crypt_63d18dea_a7de6934c67e2f89ac399fa35b689a50 HTTP/1.1
Host: wx.qq.com
Connection: keep-alive
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36
Accept: image/webp,image/apng,image/*,*/*;q=0.8
Referer: https://wx.qq.com/?lang=zh_CN
Accept-Encoding: gzip, deflate, br
Accept-Language: zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7
Cookie: mm_lang=zh_CN; MM_WX_NOTIFY_STATE=1; MM_WX_SOUND_STATE=1; wxuin=176666155; wxsid=5YpLHmVgWwH5LxN1; wxloadtime=1546211541; webwx_data_ticket=gSehWtnDWTMh38gmz0IjaFv0; webwxuvid=fcc96ffbc5c1f0c49dba1a84cc9bceee1b7ea37d1100e803e3b4ff6f6ddecca5d210211a325524605501164faa3a5a61; webwx_auth_ticket=CIsBENjn/8ICGoABHRBIWkIstAR4f6vOhj3FDD/Yge5pisSrRuwZeblgB/4ltMu/WAwqySrzD4zsO7ADs9JpDrMyS+rxsLEgoVGaJbhb5iMyGRjTEOdP+ohY9S+6aZh8ADk/RdxE3SrHB5cNDdDw0t9ZAUx2fa35DiM8pGFJQa+5P7PqennD1Lsl3P8=; login_frequency=1; last_wxuin=176666155

HTTP/1.1 200 OK
Connection: keep-alive
Content-Type: image/jpeg
Last-Modified: Sun, 30 Dec 2018 23:12:22 GMT
Cache-control: max-age=604800
Strict-Transport-Security: max-age=31536000 ; includeSubDomains
Content-Length: 3592

data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQABAAD......
```

## 8. webwxgetheadimg

``` http
GET https://wx.qq.com/cgi-bin/mmwebwx-bin/webwxgetheadimg?seq=698322408&username=@@0d9ad8ab75097485e5f22cda0d84c837b086361207759fafbbf16316bbd22663&skey=@crypt_63d18dea_a7de6934c67e2f89ac399fa35b689a50 HTTP/1.1
Host: wx.qq.com
Connection: keep-alive
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36
Accept: image/webp,image/apng,image/*,*/*;q=0.8
Referer: https://wx.qq.com/?lang=zh_CN
Accept-Encoding: gzip, deflate, br
Accept-Language: zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7
Cookie: mm_lang=zh_CN; MM_WX_NOTIFY_STATE=1; MM_WX_SOUND_STATE=1; wxuin=176666155; wxsid=5YpLHmVgWwH5LxN1; wxloadtime=1546211541; webwx_data_ticket=gSehWtnDWTMh38gmz0IjaFv0; webwxuvid=fcc96ffbc5c1f0c49dba1a84cc9bceee1b7ea37d1100e803e3b4ff6f6ddecca5d210211a325524605501164faa3a5a61; webwx_auth_ticket=CIsBENjn/8ICGoABHRBIWkIstAR4f6vOhj3FDD/Yge5pisSrRuwZeblgB/4ltMu/WAwqySrzD4zsO7ADs9JpDrMyS+rxsLEgoVGaJbhb5iMyGRjTEOdP+ohY9S+6aZh8ADk/RdxE3SrHB5cNDdDw0t9ZAUx2fa35DiM8pGFJQa+5P7PqennD1Lsl3P8=; login_frequency=1; last_wxuin=176666155

HTTP/1.1 200 OK
Connection: keep-alive
Accept-Ranges: bytes
Content-Type: image/jpeg
Last-Modified: Sun, 30 Dec 2018 23:12:23 GMT
Cache-control: max-age=604800
Strict-Transport-Security: max-age=31536000 ; includeSubDomains
Content-Length: 8990

data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQABAAD......
```

## 9. webwxgetcontact

``` js
// 置顶聊天
// CONTACTFLAG_TOPCONTACT: 2048,
isTop: function() {
    //   ‭1000_1000_0000_0011‬
    // &)0000‭_1000_0000_0000‬
    // ---------------------
    //   0000_1000_0000_0000
    return this.ContactFlag & i.CONTACTFLAG_TOPCONTACT
}

```

``` http
GET https://wx.qq.com/cgi-bin/mmwebwx-bin/webwxgetcontact?lang=zh_CN&pass_ticket=kbmy5Jt9nj%252BJzBaJZYNpUBQyBwRTejeXq1sM%252FYH7iOI%253D&r=1546211540630&seq=0&skey=@crypt_63d18dea_a7de6934c67e2f89ac399fa35b689a50 HTTP/1.1
Host: wx.qq.com
Connection: keep-alive
Accept: application/json, text/plain, */*
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36
Referer: https://wx.qq.com/?lang=zh_CN
Accept-Encoding: gzip, deflate, br
Accept-Language: zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7
Cookie: mm_lang=zh_CN; MM_WX_NOTIFY_STATE=1; MM_WX_SOUND_STATE=1; wxuin=176666155; wxsid=5YpLHmVgWwH5LxN1; wxloadtime=1546211541; webwx_data_ticket=gSehWtnDWTMh38gmz0IjaFv0; webwxuvid=fcc96ffbc5c1f0c49dba1a84cc9bceee1b7ea37d1100e803e3b4ff6f6ddecca5d210211a325524605501164faa3a5a61; webwx_auth_ticket=CIsBENjn/8ICGoABHRBIWkIstAR4f6vOhj3FDD/Yge5pisSrRuwZeblgB/4ltMu/WAwqySrzD4zsO7ADs9JpDrMyS+rxsLEgoVGaJbhb5iMyGRjTEOdP+ohY9S+6aZh8ADk/RdxE3SrHB5cNDdDw0t9ZAUx2fa35DiM8pGFJQa+5P7PqennD1Lsl3P8=; login_frequency=1; last_wxuin=176666155

HTTP/1.1 200 OK
Connection: keep-alive
Content-Type: text/plain
Strict-Transport-Security: max-age=31536000 ; includeSubDomains
Content-Encoding: gzip
Content-Length: 9928

{
    "BaseResponse": {"Ret": 0,"ErrMsg": ""},
    "Seq": 0,
    "MemberCount": 64,
    "MemberList": [{
        "Uin": 0,
        "UserName": "@a53c1fbfce2d6ece47a39329ca2e95071e05cddbd94f0ec3973ffe39a06ccbe6",
        "NickName": "æ¨çˆ½",
        "HeadImgUrl": "/cgi-bin/mmwebwx-bin/webwxgeticon?seq=696335334&username=@a53c1fbfce2d6ece47a39329ca2e95071e05cddbd94f0ec3973ffe39a06ccbe6&skey=@crypt_63d18dea_a7de6934c67e2f89ac399fa35b689a50",
        "ContactFlag": 34819,
        "MemberCount": 0,
        "MemberList": [],
        "RemarkName": "äº²çˆ±çš„å¤§å®è´",
        "HideInputBarFlag": 0,
        "Sex": 0,
        "Signature": "",
        "VerifyFlag": 0,
        "OwnerUin": 0,
        "PYInitial": "YS",
        "PYQuanPin": "yangshuang",
        "RemarkPYInitial": "QADDBB",
        "RemarkPYQuanPin": "qinaidedabaobei",
        "StarFriend": 0,
        "AppAccountFlag": 0,
        "Statues": 0,
        "AttrStatus": 135461,
        "Province": "",
        "City": "",
        "Alias": "",
        "SnsFlag": 1,
        "UniFriend": 0,
        "DisplayName": "",
        "ChatRoomId": 0,
        "KeyWord": "",
        "EncryChatRoomId": "",
        "IsOwner": 0
    }]
}
```

## 10. webwxbatchgetcontact
``` http
POST https://wx.qq.com/cgi-bin/mmwebwx-bin/webwxbatchgetcontact?type=ex&r=1546211540656&lang=zh_CN&pass_ticket=kbmy5Jt9nj%252BJzBaJZYNpUBQyBwRTejeXq1sM%252FYH7iOI%253D HTTP/1.1
Host: wx.qq.com
Connection: keep-alive
Content-Length: 361
Accept: application/json, text/plain, */*
Origin: https://wx.qq.com
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36
Content-Type: application/json;charset=UTF-8
Referer: https://wx.qq.com/?lang=zh_CN
Accept-Encoding: gzip, deflate, br
Accept-Language: zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7
Cookie: mm_lang=zh_CN; MM_WX_NOTIFY_STATE=1; MM_WX_SOUND_STATE=1; wxuin=176666155; wxsid=5YpLHmVgWwH5LxN1; wxloadtime=1546211541; webwx_data_ticket=gSehWtnDWTMh38gmz0IjaFv0; webwxuvid=fcc96ffbc5c1f0c49dba1a84cc9bceee1b7ea37d1100e803e3b4ff6f6ddecca5d210211a325524605501164faa3a5a61; webwx_auth_ticket=CIsBENjn/8ICGoABHRBIWkIstAR4f6vOhj3FDD/Yge5pisSrRuwZeblgB/4ltMu/WAwqySrzD4zsO7ADs9JpDrMyS+rxsLEgoVGaJbhb5iMyGRjTEOdP+ohY9S+6aZh8ADk/RdxE3SrHB5cNDdDw0t9ZAUx2fa35DiM8pGFJQa+5P7PqennD1Lsl3P8=; login_frequency=1; last_wxuin=176666155

{"BaseRequest":{"Uin":176666155,"Sid":"5YpLHmVgWwH5LxN1","Skey":"@crypt_63d18dea_a7de6934c67e2f89ac399fa35b689a50","DeviceID":"e052188541765358"},"Count":2,"List":[{"UserName":"@@35c78783e0906eb43b070ef4425a965cf06933f67cc45a436d89d7e31267348e","ChatRoomId":""},{"UserName":"@@0d9ad8ab75097485e5f22cda0d84c837b086361207759fafbbf16316bbd22663","ChatRoomId":""}]}

HTTP/1.1 200 OK
Connection: keep-alive
Content-Type: text/plain
Strict-Transport-Security: max-age=31536000 ; includeSubDomains
Content-Encoding: gzip
Content-Length: 3262

{
    "BaseResponse": {"Ret": 0,"ErrMsg": ""},
    "Count": 2,
    "ContactList": [{
        "Uin": 0,
        "UserName": "@@35c78783e0906eb43b070ef4425a965cf06933f67cc45a436d89d7e31267348e",
        "NickName": "ç›¸äº²ç›¸çˆ±çš„ä¸€å®¶äºº",
        "HeadImgUrl": "/cgi-bin/mmwebwx-bin/webwxgetheadimg?seq=698321427&username=@@35c78783e0906eb43b070ef4425a965cf06933f67cc45a436d89d7e31267348e&skey=",
        "ContactFlag": 2,
        "MemberCount": 14,
        "MemberList": [{
            "Uin": 0,
            "UserName": "@a53c1fbfce2d6ece47a39329ca2e95071e05cddbd94f0ec3973ffe39a06ccbe6",
            "NickName": "äº²çˆ±çš„å¤§å®è´",
            "AttrStatus": 135461,
            "PYInitial": "",
            "PYQuanPin": "",
            "RemarkPYInitial": "",
            "RemarkPYQuanPin": "",
            "MemberStatus": 0,
            "DisplayName": "",
            "KeyWord": ""
        }],
        "RemarkName": "",
        "HideInputBarFlag": 0,
        "Sex": 0,
        "Signature": "",
        "VerifyFlag": 0,
        "OwnerUin": 0,
        "PYInitial": "XQXADYJR",
        "PYQuanPin": "xiangqinxiangaideyijiaren",
        "RemarkPYInitial": "",
        "RemarkPYQuanPin": "",
        "StarFriend": 0,
        "AppAccountFlag": 0,
        "Statues": 0,
        "AttrStatus": 0,
        "Province": "",
        "City": "",
        "Alias": "",
        "SnsFlag": 0,
        "UniFriend": 0,
        "DisplayName": "",
        "ChatRoomId": 0,
        "KeyWord": "",
        "EncryChatRoomId": "@a8893c6bf328cdbca926222211bc33dc",
        "IsOwner": 0
    }]
}
```

## 11. webwxsync
``` http
POST https://wx.qq.com/cgi-bin/mmwebwx-bin/webwxsync?sid=5YpLHmVgWwH5LxN1&skey=@crypt_63d18dea_a7de6934c67e2f89ac399fa35b689a50&lang=zh_CN&pass_ticket=kbmy5Jt9nj%252BJzBaJZYNpUBQyBwRTejeXq1sM%252FYH7iOI%253D HTTP/1.1
Host: wx.qq.com
Connection: keep-alive
Content-Length: 300
Accept: application/json, text/plain, */*
Origin: https://wx.qq.com
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36
Content-Type: application/json;charset=UTF-8
Referer: https://wx.qq.com/?lang=zh_CN
Accept-Encoding: gzip, deflate, br
Accept-Language: zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7
Cookie: mm_lang=zh_CN; MM_WX_NOTIFY_STATE=1; MM_WX_SOUND_STATE=1; wxuin=176666155; wxsid=5YpLHmVgWwH5LxN1; wxloadtime=1546211541; webwx_data_ticket=gSehWtnDWTMh38gmz0IjaFv0; webwxuvid=fcc96ffbc5c1f0c49dba1a84cc9bceee1b7ea37d1100e803e3b4ff6f6ddecca5d210211a325524605501164faa3a5a61; webwx_auth_ticket=CIsBENjn/8ICGoABHRBIWkIstAR4f6vOhj3FDD/Yge5pisSrRuwZeblgB/4ltMu/WAwqySrzD4zsO7ADs9JpDrMyS+rxsLEgoVGaJbhb5iMyGRjTEOdP+ohY9S+6aZh8ADk/RdxE3SrHB5cNDdDw0t9ZAUx2fa35DiM8pGFJQa+5P7PqennD1Lsl3P8=; login_frequency=1; last_wxuin=176666155

{"BaseRequest":{"Uin":176666155,"Sid":"5YpLHmVgWwH5LxN1","Skey":"@crypt_63d18dea_a7de6934c67e2f89ac399fa35b689a50","DeviceID":"e628821606540135"},"SyncKey":{"Count":4,"List":[{"Key":1,"Val":698325105},{"Key":2,"Val":698325109},{"Key":3,"Val":698325051},{"Key":1000,"Val":1546210682}]},"rr":-23314857}

HTTP/1.1 200 OK
Connection: keep-alive
Content-Type: text/plain
Set-Cookie: wxloadtime=1546211541_expired; Domain=wx.qq.com; Path=/; Expires=Mon, 31-Dec-2018 11:12:23 GMT; Secure
Set-Cookie: wxpluginkey=1546210682; Domain=wx.qq.com; Path=/; Expires=Mon, 31-Dec-2018 11:12:23 GMT; Secure
Set-Cookie: wxuin=176666155; Domain=wx.qq.com; Path=/; Expires=Wed, 02-Jan-2019 23:12:23 GMT; Secure
Set-Cookie: wxsid=5YpLHmVgWwH5LxN1; Domain=wx.qq.com; Path=/; Expires=Mon, 31-Dec-2018 11:12:23 GMT; Secure
Set-Cookie: webwx_data_ticket=gSehWtnDWTMh38gmz0IjaFv0; Domain=.qq.com; Path=/; Expires=Mon, 31-Dec-2018 11:12:23 GMT; Secure
Strict-Transport-Security: max-age=31536000 ; includeSubDomains
Content-Encoding: gzip
Content-Length: 2173

{
    "BaseResponse": {"Ret": 0,"ErrMsg": ""},
    "AddMsgCount": 1,
    "AddMsgList": [{
        "MsgId": "6929942228714508460",
        "FromUserName": "@d77f22c1444fcbd7092fa2464899391a3cc0d6a9ec79b0770ce91be01a66a09b",
        "ToUserName": "@f0c04abe389590b02d91684d99584439",
        "MsgType": 47,  // 图片
        "Content": "&lt;msg&gt;&lt;emoji fromusername=\"xxx\" tousername=\"xxx\" type=\"2\" idbuffer=\"media:0_0\" md5=\"594560af0d32d92224682ee2c292877a\" len=\"447446\" productid=\"\" androidmd5=\"594560af0d32d92224682ee2c292877a\" androidlen=\"447446\" s60v3md5=\"594560af0d32d92224682ee2c292877a\" s60v3len=\"447446\" s60v5md5=\"594560af0d32d92224682ee2c292877a\" s60v5len=\"447446\" cdnurl=\"http://emoji.qpic.cn/wx_emoji/NBYII27icz3UvDVcCrK2sXzjZeTXzicxmtdRw9NT55ONgEB4VQHCsmTA/\" designerid=\"\" thumburl=\"\" encrypturl=\"http://emoji.qpic.cn/wx_emoji/NBYII27icz3UvDVcCrK2sXzjZeTXzicxmtgwJAQBfFGKalttRyL1HHTQ/\" aeskey=\"f63eaffcd7e1b1efce4ca891a7034ade\" externurl=\"http://emoji.qpic.cn/wx_emoji/nqlcrTzic7Dp3lMHWq1iczdNbFKgUpk5Sib5zZZqNhw1yib6cBTXSicEMPR7lyx7HBRhr/\" externmd5=\"627c87c363940e825cc92bbfc09235b7\" width=\"240\" height=\"210\" tpurl=\"\" tpauthkey=\"\" attachedtext=\"\"&gt;&lt;/emoji&gt;&lt;gameext type=\"0\" content=\"0\"&gt;&lt;/gameext&gt;&lt;/msg&gt;",
        "Status": 3,
        "ImgStatus": 2,
        "CreateTime": 1546215540,
        "VoiceLength": 0,
        "PlayLength": 0,
        "FileName": "",
        "FileSize": "",
        "MediaId": "",
        "Url": "",
        "AppMsgType": 0,
        "StatusNotifyCode": 0,
        "StatusNotifyUserName": "",
        "RecommendInfo": {
            "UserName": "",
            "NickName": "",
            "QQNum": 0,
            "Province": "",
            "City": "",
            "Content": "",
            "Signature": "",
            "Alias": "",
            "Scene": 0,
            "VerifyFlag": 0,
            "AttrStatus": 0,
            "Sex": 0,
            "Ticket": "",
            "OpCode": 0
        },
        "ForwardFlag": 0,
        "AppInfo": {
            "AppID": "",
            "Type": 0
        },
        "HasProductId": 0,
        "Ticket": "",
        "ImgHeight": 210,
        "ImgWidth": 240,
        "SubMsgType": 0,
        "NewMsgId": 6929942228714508460,
        "OriContent": "",
        "EncryFileName": ""
    }, {
        "MsgId": "1406220077798918330",
        "FromUserName": "@f0c04abe389590b02d91684d99584439",
        "ToUserName": "@d77f22c1444fcbd7092fa2464899391a3cc0d6a9ec79b0770ce91be01a66a09b",
        "MsgType": 51,  // 心跳包
        "Content": "",
        "Status": 3,
        "ImgStatus": 1,
        "CreateTime": 1546215541,
        "VoiceLength": 0,
        "PlayLength": 0,
        "FileName": "",
        "FileSize": "",
        "MediaId": "",
        "Url": "",
        "AppMsgType": 0,
        "StatusNotifyCode": 1,
        "StatusNotifyUserName": "@d77f22c1444fcbd7092fa2464899391a3cc0d6a9ec79b0770ce91be01a66a09b",
        "RecommendInfo": {
            "UserName": "",
            "NickName": "",
            "QQNum": 0,
            "Province": "",
            "City": "",
            "Content": "",
            "Signature": "",
            "Alias": "",
            "Scene": 0,
            "VerifyFlag": 0,
            "AttrStatus": 0,
            "Sex": 0,
            "Ticket": "",
            "OpCode": 0
        },
        "ForwardFlag": 0,
        "AppInfo": {
            "AppID": "",
            "Type": 0
        },
        "HasProductId": 0,
        "Ticket": "",
        "ImgHeight": 0,
        "ImgWidth": 0,
        "SubMsgType": 0,
        "NewMsgId": 1406220077798918330,
        "OriContent": "",
        "EncryFileName": ""
    },{
        "MsgId": "3835382370273594705",
        "FromUserName": "@d77f22c1444fcbd7092fa2464899391a3cc0d6a9ec79b0770ce91be01a66a09b",
        "ToUserName": "@f0c04abe389590b02d91684d99584439",
        "MsgType": 43,  // 视频
        "Content": "@70a713006499022939c7cd2c664d806e769435aa95ce58f7f56a62de9500c2d9b09e445bd98685910f6fa351e97657c88ffe5de3b2ba26a2951281f0ef1a293a8b2c611d7de91c28fd31f1f8d8fcbd0d4ddce2abf18d5af93072a206644c148e7a9035d050634d245fdbcf9420435a045115e8583cbd81f43d0073794f0c989472a3b6c79907e8593baf003f160671acaf6de658344cada2badd40a96d253a904a26cb6177af9d7d8f23254a38e884ba5b9560da9f8fd3c2a89d6d07bf69a88f259a440b7823e434668c7935301d11d3520332921735a4cab6019027f1f9edccb45107df41339602853c843b29abe2d9455d635652bc654d218d7d62ae6793c483a9eb49fc58d06b5526b4a7426acd6e4feb79b70fc101ccad7fb1e9634574bb98910a79fc6cd48b92260c8d80ae502cc4137abb5468a2fc33bf9da160abf7221d8ea7ffe9f9396dcd8d147c7653c8ec98a60eba0027a07face94322e7546a65038aa2cde6328ef9fd53a7e065233b59385374758a1843a7f3f894700c8cf3197fdb6ade9122fde3b8a325b4f09f7d0db26aa82e1ff65bcb5e0953d35673d0b547779bf5d328f3e67fe4cec3679bd110ae1c51d4cd8a5694677a8d22ab10036ea90c454071eaf982201f3794bfea9190ec39a0958ba6133ef508c7bc5024245adf915779b52c84ca59b219f9d2e99f5d45a74467291e6867573f0c1010ef171ac1f4c1d7daab411c764e4dd81c46a288e26a832095cdb0c4e0ee4804413d22e5eca84ddc6860631d003cabaff106a731bb2de3bf923f2534394588b691db0361c3c1be103888917d9123986febf4c5990b533611042e9a186622106e4f0520325742b81a1f82a0a0c51d2927df63114daa387638966d2173bc150f17e668db83ea41ce1fbe0d1ec5893e6c6927321a68a8a690a3a162704d116615dd45316a5a92f7614060bdd37c4933101a692d5d0767cd23c8725f98ceeacb9d3cefbeb77f89ad8c3c600977c7ee4c526eac40b42c760495b4856f3b30e77aec5127de50d26dd71a6e63e66e389f654f20ca1680b48b942ad8dbfdb6a89dbeb7c85790822758e5e401f236e460d0a69aa6166e48c43273305526df2f2146b8ae18cffef71996beee423705a132e368685da6ee95bcfb7f55e79a254f2f27efcdeb471946a189aa595ce587b5ca69455c96128d1c9d",
        "Status": 3,
        "ImgStatus": 1,
        "CreateTime": 1546216601,
        "VoiceLength": 0,
        "PlayLength": 10,
        "FileName": "",
        "FileSize": "",
        "MediaId": "",
        "Url": "",
        "AppMsgType": 0,
        "StatusNotifyCode": 0,
        "StatusNotifyUserName": "",
        "RecommendInfo": {
            "UserName": "",
            "NickName": "",
            "QQNum": 0,
            "Province": "",
            "City": "",
            "Content": "",
            "Signature": "",
            "Alias": "",
            "Scene": 0,
            "VerifyFlag": 0,
            "AttrStatus": 0,
            "Sex": 0,
            "Ticket": "",
            "OpCode": 0
        },
        "ForwardFlag": 0,
        "AppInfo": {
            "AppID": "",
            "Type": 0
        },
        "HasProductId": 0,
        "Ticket": "",
        "ImgHeight": 512,
        "ImgWidth": 290,
        "SubMsgType": 0,
        "NewMsgId": 3835382370273594705,
        "OriContent": "",
        "EncryFileName": ""
    },{
        "MsgId": "6469713913745187012",
        "FromUserName": "@d77f22c1444fcbd7092fa2464899391a3cc0d6a9ec79b0770ce91be01a66a09b",
        "ToUserName": "@f0c04abe389590b02d91684d99584439",
        "MsgType": 1,   // 文本消息
        "Content": "ä¸ºä»€ä¹ˆè¿™ä¹ˆå†·",
        "Status": 3,
        "ImgStatus": 1,
        "CreateTime": 1546217453,
        "VoiceLength": 0,
        "PlayLength": 0,
        "FileName": "",
        "FileSize": "",
        "MediaId": "",
        "Url": "",
        "AppMsgType": 0,
        "StatusNotifyCode": 0,
        "StatusNotifyUserName": "",
        "RecommendInfo": {
            "UserName": "",
            "NickName": "",
            "QQNum": 0,
            "Province": "",
            "City": "",
            "Content": "",
            "Signature": "",
            "Alias": "",
            "Scene": 0,
            "VerifyFlag": 0,
            "AttrStatus": 0,
            "Sex": 0,
            "Ticket": "",
            "OpCode": 0
        },
        "ForwardFlag": 0,
        "AppInfo": {
            "AppID": "",
            "Type": 0
        },
        "HasProductId": 0,
        "Ticket": "",
        "ImgHeight": 0,
        "ImgWidth": 0,
        "SubMsgType": 0,
        "NewMsgId": 6469713913745187012,
        "OriContent": "",
        "EncryFileName": ""
    }],
    "ModContactCount": 0,
    "ModContactList": [],
    "DelContactCount": 0,
    "DelContactList": [],
    "ModChatRoomMemberCount": 0,
    "ModChatRoomMemberList": [],
    "Profile": {
        "BitFlag": 0,
        "UserName": {"Buff": ""},
        "NickName": {"Buff": ""},
        "BindUin": 0,
        "BindEmail": {"Buff": ""},
        "BindMobile": {"Buff": ""},
        "Status": 0,
        "Sex": 0,
        "PersonalCard": 0,
        "Alias": "",
        "HeadImgUpdateFlag": 0,
        "HeadImgUrl": "",
        "Signature": ""
    },
    "ContinueFlag": 0,
    "SyncKey": {
        "Count": 7,
        "List": [{"Key": 1,"Val": 698325105}, {"Key": 2,"Val": 698325111}, {"Key": 3,"Val": 698325051}, {"Key": 11,"Val": 698322408}, {"Key": 201,"Val": 1546211542}, {"Key": 1000,"Val": 1546210682}, {"Key": 1001,"Val": 1546210755}]
    },
    "SKey": "",
    "SyncCheckKey": {
        "Count": 7,
        "List": [{"Key": 1,"Val": 698325105}, {"Key": 2,"Val": 698325111}, {"Key": 3,"Val": 698325051}, {"Key": 11,"Val": 698322408}, {"Key": 201,"Val": 1546211542}, {"Key": 1000,"Val": 1546210682}, {"Key": 1001,"Val": 1546210755}]
    }
}
```

## 12. synccheck

``` http
GET https://webpush.wx.qq.com/cgi-bin/mmwebwx-bin/synccheck?r=1546211541936&skey=%40crypt_63d18dea_a7de6934c67e2f89ac399fa35b689a50&sid=5YpLHmVgWwH5LxN1&uin=176666155&deviceid=e152684595719975&synckey=1_698325105%7C2_698325111%7C3_698325051%7C11_698322408%7C201_1546211542%7C1000_1546210682%7C1001_1546210755&_=1546211298751 HTTP/1.1
Host: webpush.wx.qq.com
Connection: keep-alive
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36
Accept: */*
Referer: https://wx.qq.com/?lang=zh_CN
Accept-Encoding: gzip, deflate, br
Accept-Language: zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7
Cookie: mm_lang=zh_CN; wxuin=176666155; wxsid=5YpLHmVgWwH5LxN1; webwx_data_ticket=gSehWtnDWTMh38gmz0IjaFv0; webwxuvid=fcc96ffbc5c1f0c49dba1a84cc9bceee1b7ea37d1100e803e3b4ff6f6ddecca5d210211a325524605501164faa3a5a61; webwx_auth_ticket=CIsBENjn/8ICGoABHRBIWkIstAR4f6vOhj3FDD/Yge5pisSrRuwZeblgB/4ltMu/WAwqySrzD4zsO7ADs9JpDrMyS+rxsLEgoVGaJbhb5iMyGRjTEOdP+ohY9S+6aZh8ADk/RdxE3SrHB5cNDdDw0t9ZAUx2fa35DiM8pGFJQa+5P7PqennD1Lsl3P8=; wxloadtime=1546211541_expired; wxpluginkey=1546210682

HTTP/1.1 200 OK
Connection: keep-alive
Access-Control-Allow-Origin: *
Content-Type: text/javascript
Content-Length: 43

window.synccheck={retcode:"0",selector:"0"} 正常状态
window.synccheck={retcode:"0",selector:"2"} 有新消息
```

## 12. webwxsendmsg

``` http
POST https://wx.qq.com/cgi-bin/mmwebwx-bin/webwxsendmsg?lang=zh_CN&pass_ticket=kbmy5Jt9nj%252BJzBaJZYNpUBQyBwRTejeXq1sM%252FYH7iOI%253D HTTP/1.1
Host: wx.qq.com
Connection: keep-alive
Content-Length: 383
Accept: application/json, text/plain, */*
Origin: https://wx.qq.com
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36
Content-Type: application/json;charset=UTF-8
Referer: https://wx.qq.com/?lang=zh_CN
Accept-Encoding: gzip, deflate, br
Accept-Language: zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7
Cookie: mm_lang=zh_CN; MM_WX_NOTIFY_STATE=1; MM_WX_SOUND_STATE=1; wxuin=176666155; wxsid=5YpLHmVgWwH5LxN1; webwx_data_ticket=gSehWtnDWTMh38gmz0IjaFv0; webwxuvid=fcc96ffbc5c1f0c49dba1a84cc9bceee1b7ea37d1100e803e3b4ff6f6ddecca5d210211a325524605501164faa3a5a61; webwx_auth_ticket=CIsBENjn/8ICGoABHRBIWkIstAR4f6vOhj3FDD/Yge5pisSrRuwZeblgB/4ltMu/WAwqySrzD4zsO7ADs9JpDrMyS+rxsLEgoVGaJbhb5iMyGRjTEOdP+ohY9S+6aZh8ADk/RdxE3SrHB5cNDdDw0t9ZAUx2fa35DiM8pGFJQa+5P7PqennD1Lsl3P8=; login_frequency=1; last_wxuin=176666155; wxloadtime=1546211541_expired; wxpluginkey=1546210682

{"BaseRequest":{"Uin":176666155,"Sid":"5YpLHmVgWwH5LxN1","Skey":"@crypt_63d18dea_a7de6934c67e2f89ac399fa35b689a50","DeviceID":"e376288520542476"},"Msg":{"Type":1,"Content":"s","FromUserName":"@f0c04abe389590b02d91684d99584439","ToUserName":"@d77f22c1444fcbd7092fa2464899391a3cc0d6a9ec79b0770ce91be01a66a09b","LocalID":"15462147414160521","ClientMsgId":"15462147414160521"},"Scene":0}

HTTP/1.1 200 OK
Connection: keep-alive
Content-Type: text/plain
Strict-Transport-Security: max-age=31536000 ; includeSubDomains
Content-Length: 112

{"BaseResponse": {"Ret": 0,"ErrMsg": ""},"MsgID": "3053194738703673101","LocalID": "15462147414160521"}
```

## 13. webwxgetmsgimg
``` http
GET https://wx.qq.com/cgi-bin/mmwebwx-bin/webwxgetmsgimg?&MsgID=6929942228714508460&skey=%40crypt_63d18dea_a7de6934c67e2f89ac399fa35b689a50&type=big HTTP/1.1
Host: wx.qq.com
Connection: keep-alive
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36
Accept: image/webp,image/apng,image/*,*/*;q=0.8
Referer: https://wx.qq.com/?lang=zh_CN
Accept-Encoding: gzip, deflate, br
Accept-Language: zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7
Cookie: mm_lang=zh_CN; MM_WX_NOTIFY_STATE=1; MM_WX_SOUND_STATE=1; wxuin=176666155; wxsid=5YpLHmVgWwH5LxN1; webwx_data_ticket=gSehWtnDWTMh38gmz0IjaFv0; webwxuvid=fcc96ffbc5c1f0c49dba1a84cc9bceee1b7ea37d1100e803e3b4ff6f6ddecca5d210211a325524605501164faa3a5a61; webwx_auth_ticket=CIsBENjn/8ICGoABHRBIWkIstAR4f6vOhj3FDD/Yge5pisSrRuwZeblgB/4ltMu/WAwqySrzD4zsO7ADs9JpDrMyS+rxsLEgoVGaJbhb5iMyGRjTEOdP+ohY9S+6aZh8ADk/RdxE3SrHB5cNDdDw0t9ZAUx2fa35DiM8pGFJQa+5P7PqennD1Lsl3P8=; login_frequency=1; last_wxuin=176666155; wxloadtime=1546211541_expired; wxpluginkey=1546212722

HTTP/1.1 200 OK
Connection: keep-alive
Content-Type: image/jpeg
Last-Modified: Mon, 31 Dec 2018 00:19:01 GMT
Cache-control: max-age=604800
Strict-Transport-Security: max-age=31536000 ; includeSubDomains
Content-Length: 447446

data:image/jpeg;base64,R0lGODlh8ADSANU......
```

## 14. msg.mp3
``` http
GET /zh_CN/htmledition/v2/sound/msg.mp3 HTTP/1.1
Host: wx.qq.com
Connection: keep-alive
Upgrade-Insecure-Requests: 1
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36
Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8
Accept-Encoding: gzip, deflate, br
Accept-Language: zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7
Cookie: mm_lang=zh_CN; MM_WX_NOTIFY_STATE=1; MM_WX_SOUND_STATE=1; wxuin=176666155; wxsid=5YpLHmVgWwH5LxN1; webwx_data_ticket=gSehWtnDWTMh38gmz0IjaFv0; webwxuvid=fcc96ffbc5c1f0c49dba1a84cc9bceee1b7ea37d1100e803e3b4ff6f6ddecca5d210211a325524605501164faa3a5a61; webwx_auth_ticket=CIsBENjn/8ICGoABHRBIWkIstAR4f6vOhj3FDD/Yge5pisSrRuwZeblgB/4ltMu/WAwqySrzD4zsO7ADs9JpDrMyS+rxsLEgoVGaJbhb5iMyGRjTEOdP+ohY9S+6aZh8ADk/RdxE3SrHB5cNDdDw0t9ZAUx2fa35DiM8pGFJQa+5P7PqennD1Lsl3P8=; login_frequency=1; last_wxuin=176666155; wxloadtime=1546211541_expired; wxpluginkey=1546212722

HTTP/1.1 200 OK
Connection: keep-alive
Last-Modified: Fri, 18 Nov 2016 05:57:00 GMT
Content-Type: audio/mpeg
Cache-control: max-age=31536000
Strict-Transport-Security: max-age=31536000 ; includeSubDomains
Content-Length: 14639


```

## 15. webwxgetvideo
``` http
GET /cgi-bin/mmwebwx-bin/webwxgetvideo?msgid=3835382370273594705&skey=%40crypt_63d18dea_a7de6934c67e2f89ac399fa35b689a50 HTTP/1.1
Host: wx.qq.com
Connection: keep-alive
Accept-Encoding: identity;q=1, *;q=0
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36
chrome-proxy: frfr
Accept: */*
Referer: https://wx.qq.com/?lang=zh_CN
Accept-Language: zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7
Cookie: mm_lang=zh_CN; MM_WX_NOTIFY_STATE=1; MM_WX_SOUND_STATE=1; wxuin=176666155; wxsid=5YpLHmVgWwH5LxN1; webwx_data_ticket=gSehWtnDWTMh38gmz0IjaFv0; webwxuvid=fcc96ffbc5c1f0c49dba1a84cc9bceee1b7ea37d1100e803e3b4ff6f6ddecca5d210211a325524605501164faa3a5a61; webwx_auth_ticket=CIsBENjn/8ICGoABHRBIWkIstAR4f6vOhj3FDD/Yge5pisSrRuwZeblgB/4ltMu/WAwqySrzD4zsO7ADs9JpDrMyS+rxsLEgoVGaJbhb5iMyGRjTEOdP+ohY9S+6aZh8ADk/RdxE3SrHB5cNDdDw0t9ZAUx2fa35DiM8pGFJQa+5P7PqennD1Lsl3P8=; login_frequency=1; last_wxuin=176666155; wxloadtime=1546211541_expired; wxpluginkey=1546212722
Range: bytes=0-

HTTP/1.1 206 Partial Content
Connection: keep-alive
Content-Type: video/mp4
Accept-Ranges: bytes
Last-Modified: Thu, 01 Jan 1970 00:00:00 GMT
ETag: 3313f-4e6206-5ca584da4e6bf21a6bf7
Strict-Transport-Security: max-age=31536000 ; includeSubDomains
Content-Range: bytes 0-1522083/1522084
Content-Length: 1522084


```

## 15. webwxuploadmedia
``` http
POST /cgi-bin/mmwebwx-bin/webwxuploadmedia?f=json HTTP/1.1
Host: file.wx.qq.com
Connection: keep-alive
Content-Length: 526107
Origin: https://wx.qq.com
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36
Content-Type: multipart/form-data; boundary=----WebKitFormBoundaryCFAuSFyYgivpBuGp
Accept: */*
Referer: https://wx.qq.com/?lang=zh_CN
Accept-Encoding: gzip, deflate, br
Accept-Language: zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7
Cookie: mm_lang=zh_CN; wxuin=176666155; wxsid=5YpLHmVgWwH5LxN1; webwx_data_ticket=gSehWtnDWTMh38gmz0IjaFv0; webwxuvid=fcc96ffbc5c1f0c49dba1a84cc9bceee1b7ea37d1100e803e3b4ff6f6ddecca5d210211a325524605501164faa3a5a61; webwx_auth_ticket=CIsBENjn/8ICGoABHRBIWkIstAR4f6vOhj3FDD/Yge5pisSrRuwZeblgB/4ltMu/WAwqySrzD4zsO7ADs9JpDrMyS+rxsLEgoVGaJbhb5iMyGRjTEOdP+ohY9S+6aZh8ADk/RdxE3SrHB5cNDdDw0t9ZAUx2fa35DiM8pGFJQa+5P7PqennD1Lsl3P8=; wxloadtime=1546211541_expired; wxpluginkey=1546212722

------WebKitFormBoundaryCFAuSFyYgivpBuGp
Content-Disposition: form-data; name="id"

WU_FILE_0
------WebKitFormBoundaryCFAuSFyYgivpBuGp
Content-Disposition: form-data; name="name"

IMG_1002.JPG
------WebKitFormBoundaryCFAuSFyYgivpBuGp
Content-Disposition: form-data; name="type"

image/jpeg
------WebKitFormBoundaryCFAuSFyYgivpBuGp
Content-Disposition: form-data; name="lastModifiedDate"

Sat Jun 23 2018 16:45:07 GMT+0800 (中国标准时间)
------WebKitFormBoundaryCFAuSFyYgivpBuGp
Content-Disposition: form-data; name="size"

3213741
------WebKitFormBoundaryCFAuSFyYgivpBuGp
Content-Disposition: form-data; name="chunks"

7
------WebKitFormBoundaryCFAuSFyYgivpBuGp
Content-Disposition: form-data; name="chunk"

1
------WebKitFormBoundaryCFAuSFyYgivpBuGp
Content-Disposition: form-data; name="mediatype"

pic
------WebKitFormBoundaryCFAuSFyYgivpBuGp
Content-Disposition: form-data; name="uploadmediarequest"

{"UploadType":2,"BaseRequest":{"Uin":176666155,"Sid":"5YpLHmVgWwH5LxN1","Skey":"@crypt_63d18dea_a7de6934c67e2f89ac399fa35b689a50","DeviceID":"e623120044971769"},"ClientMediaId":1546217858497,"TotalLen":3213741,"StartPos":0,"DataLen":3213741,"MediaType":4,"FromUserName":"@f0c04abe389590b02d91684d99584439","ToUserName":"@d77f22c1444fcbd7092fa2464899391a3cc0d6a9ec79b0770ce91be01a66a09b","FileMd5":"cc6bbb277bc836b5726cc9d9ec7549c2"}
------WebKitFormBoundaryCFAuSFyYgivpBuGp
Content-Disposition: form-data; name="webwx_data_ticket"

gSehWtnDWTMh38gmz0IjaFv0
------WebKitFormBoundaryCFAuSFyYgivpBuGp
Content-Disposition: form-data; name="pass_ticket"

undefined
------WebKitFormBoundaryCFAuSFyYgivpBuGp
Content-Disposition: form-data; name="filename"; filename="IMG_1002.JPG"
Content-Type: application/octet-stream

ÍñÇ¨"ØHÜTñQÁíWl>Ø[MåÍÌ±ôú......
------WebKitFormBoundaryCFAuSFyYgivpBuGp

HTTP/1.1 200 OK
Connection: keep-alive
Content-Type: text/plain
Date: Mon, 31-Dec-2018 00:57:48 GMT
Access-Control-Allow-Origin: https://wx.qq.com
Access-Control-Allow-Credentials: true
Strict-Transport-Security: max-age=31536000 ; includeSubDomains
Content-Length: 162

```

## 16. webwxuploadmedia
``` http
POST /cgi-bin/mmwebwx-bin/webwxuploadmedia?f=json HTTP/1.1
Host: file.wx.qq.com
Connection: keep-alive
Content-Length: 64660
Origin: https://wx.qq.com
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36
Content-Type: multipart/form-data; boundary=----WebKitFormBoundary0ywlHc0PGGTxcqrt
Accept: */*
Referer: https://wx.qq.com/?lang=zh_CN
Accept-Encoding: gzip, deflate, br
Accept-Language: zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7
Cookie: mm_lang=zh_CN; wxuin=176666155; wxsid=5YpLHmVgWwH5LxN1; webwx_data_ticket=gSehWtnDWTMh38gmz0IjaFv0; webwxuvid=fcc96ffbc5c1f0c49dba1a84cc9bceee1b7ea37d1100e803e3b4ff6f6ddecca5d210211a325524605501164faa3a5a61; webwx_auth_ticket=CIsBENjn/8ICGoABHRBIWkIstAR4f6vOhj3FDD/Yge5pisSrRuwZeblgB/4ltMu/WAwqySrzD4zsO7ADs9JpDrMyS+rxsLEgoVGaJbhb5iMyGRjTEOdP+ohY9S+6aZh8ADk/RdxE3SrHB5cNDdDw0t9ZAUx2fa35DiM8pGFJQa+5P7PqennD1Lsl3P8=; wxloadtime=1546211541_expired; wxpluginkey=1546212722

------WebKitFormBoundary0ywlHc0PGGTxcqrt
Content-Disposition: form-data; name="id"

WU_FILE_1
------WebKitFormBoundary0ywlHc0PGGTxcqrt
Content-Disposition: form-data; name="name"

IMG_1854.JPG
------WebKitFormBoundary0ywlHc0PGGTxcqrt
Content-Disposition: form-data; name="type"

image/jpeg
------WebKitFormBoundary0ywlHc0PGGTxcqrt
Content-Disposition: form-data; name="lastModifiedDate"

Tue Nov 06 2018 08:45:19 GMT+0800 (中国标准时间)
------WebKitFormBoundary0ywlHc0PGGTxcqrt
Content-Disposition: form-data; name="size"

63048
------WebKitFormBoundary0ywlHc0PGGTxcqrt
Content-Disposition: form-data; name="mediatype"

pic
------WebKitFormBoundary0ywlHc0PGGTxcqrt
Content-Disposition: form-data; name="uploadmediarequest"

{"UploadType":2,"BaseRequest":{"Uin":176666155,"Sid":"5YpLHmVgWwH5LxN1","Skey":"@crypt_63d18dea_a7de6934c67e2f89ac399fa35b689a50","DeviceID":"e233776295159507"},"ClientMediaId":1546218776042,"TotalLen":63048,"StartPos":0,"DataLen":63048,"MediaType":4,"FromUserName":"@f0c04abe389590b02d91684d99584439","ToUserName":"@d77f22c1444fcbd7092fa2464899391a3cc0d6a9ec79b0770ce91be01a66a09b","FileMd5":"d5703346bc99a77c1fe276f94b2d9fd4"}
------WebKitFormBoundary0ywlHc0PGGTxcqrt
Content-Disposition: form-data; name="webwx_data_ticket"

gSehWtnDWTMh38gmz0IjaFv0
------WebKitFormBoundary0ywlHc0PGGTxcqrt
Content-Disposition: form-data; name="pass_ticket"

undefined
------WebKitFormBoundary0ywlHc0PGGTxcqrt
Content-Disposition: form-data; name="filename"; filename="IMG_1854.JPG"
Content-Type: image/jpeg


------WebKitFormBoundary0ywlHc0PGGTxcqrt--

HTTP/1.1 200 OK
Connection: keep-alive
Content-Type: text/plain
Date: Mon, 31-Dec-2018 01:12:59 GMT
Access-Control-Allow-Origin: https://wx.qq.com
Access-Control-Allow-Credentials: true
Strict-Transport-Security: max-age=31536000 ; includeSubDomains
Content-Encoding: gzip
Content-Length: 549

{
    "BaseResponse": {"Ret": 0,"ErrMsg": ""},
    "MediaId": "@crypt_ca8428f8_3ab89b8f73dbb0f55d7d7f65b3370ca6e097140bf7465c1b1960b9cdecbeceacfd0fe7ec8ec46e9dbaea4aa7e8b00accada3573226581b94d99e0bb6671ea958b3b403b75af907dac90102e56fcf1c679960fcbba11814a2aa5bb243f46bd23950a7a0823e5d143120126e501b819b6097b4f99c7718d5f5bf68316d875430e12d2b785dc3241872e6a96c9c06f14559cceb24fe02f8ed8b09a5fecb8d90c5a25f1be86248936302ce5a7227459c0c41b23156589f820465b69c4a06e53df9bd5cb2cbcaff7dfdad60af47dbdf01a2bb6c909b48e39a2c87a4202ddb30cab1200443fad08269ffd2ce14e99b20537f6758f7672543ed01750be8bb8b04a2cd9602b685968069aa7ef30077b77b0534613ba51a0ef45e89ec45e6ff8e1c5b93d0f6fbd99f76d4e7df1cfb2c0015baa8aec0049bd2514cd6eaeade2cfe96854053",
    "StartPos": 63048,
    "CDNThumbImgHeight": 100,
    "CDNThumbImgWidth": 66,
    "EncryFileName": "IMG%5F1854%2EJPG"
}
```