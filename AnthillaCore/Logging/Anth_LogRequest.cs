using Nancy;
using Nancy.Cookies;
using System;
using System.Collections.Generic;

namespace AnthillaCore.Logging {

    public static class LogRequest {

        public static void Trace(Request request) {
            var date = DateTime.Now;
            Anth_LogRequest.WriteLog(request, date);
        }
    }

    public static class Anth_LogRequest {

        public static void WriteLog(Request request, DateTime date) {
            RequestHeaders header = request.Headers;

            Anth_RequestModel logItem = new Anth_RequestModel();

            logItem.RequestId = Guid.NewGuid().ToString();
            logItem.RequestGuid = Guid.NewGuid().ToString();
            logItem.DateTime = date;

            var alist = new List<string>();
            foreach (var a in header.Accept) {
                alist.Add(a.Item1);
            }
            logItem.Accept = alist;

            var aclist = new List<string>();
            foreach (var ac in header.AcceptCharset) {
                aclist.Add(ac.Item1);
            }
            logItem.AcceptCharset = aclist;

            var aelist = new List<string>();
            foreach (var ae in header.AcceptEncoding) {
                aelist.Add(ae);
            }
            logItem.AcceptEncoding = aelist;

            var allist = new List<string>();
            foreach (var al in header.AcceptLanguage) {
                allist.Add(al.Item1);
            }
            logItem.AcceptLanguage = allist;

            var clist = new List<Anth_CookieModel>();
            foreach (NancyCookie c in header.Cookie) {
                var cookie = new Anth_CookieModel();
                cookie.CookieName = c.Name;
                cookie.CookieValue = c.Value;
                cookie.CookieExpires = c.Expires.ToString();
                cookie.CookiePath = c.Path;
                clist.Add(cookie);
            }
            logItem.Cookies = clist;

            logItem.Host = header.Host;
            logItem.Method = request.Method;
            logItem.Url = request.Url;
            logItem.Path = request.Path;

            DeNSo.Session.New.Set(logItem);
        }
    }
}