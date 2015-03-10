using AnthillaCore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnthillaCore.Logging {

    public class Anth_RequestModel : Anth_DelEntity {

        [Key]
        public string RequestId { get { return _Id; } set { _Id = value; } }

        public string RequestGuid { get; set; }

        public DateTime DateTime { get; set; }

        public List<string> Accept { get; set; }

        public List<string> AcceptCharset { get; set; }

        public List<string> AcceptEncoding { get; set; }

        public List<string> AcceptLanguage { get; set; }

        public List<Anth_CookieModel> Cookies { get; set; }

        public string Host { get; set; }

        public string Method { get; set; }

        public string Url { get; set; }

        public string Path { get; set; }
    }

    public class Anth_CookieModel : Anth_DelEntity {

        [Key]
        public string CookieId { get { return _Id; } set { _Id = value; } }

        public string CookieGuid { get; set; }

        public string CookieName { get; set; }

        public string CookieValue { get; set; }

        public string CookieExpires { get; set; }

        public string CookiePath { get; set; }
    }
}