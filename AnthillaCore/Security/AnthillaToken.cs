using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AnthillaCore.Security {

    public class Anth_TokenModel {

        [Key]
        public string _Id { get; set; }

        public string Guid { get; set; }

        public string Session { get; set; }

        public string Value { get; set; }
    }

    public class Anth_TokenRepository {

        public List<Anth_TokenModel> GetAll(string session) {
            List<Anth_TokenModel> list = DeNSo.Session.New.Get<Anth_TokenModel>(i => i.Session == session).ToList();
            return list;
        }

        public Anth_TokenModel GetBySession(string session) {
            Anth_TokenModel item = DeNSo.Session.New.Get<Anth_TokenModel>(i => i.Session == session).FirstOrDefault();
            return item;
        }

        public Anth_TokenModel Create(string session) {
            var captchas = GetAll(session);
            foreach (var c in captchas) {
                DeNSo.Session.New.Delete(c);
            }

            Anth_TokenModel item = new Anth_TokenModel();
            item._Id = Guid.NewGuid().ToString();
            item.Guid = Guid.NewGuid().ToString();
            item.Session = session;
            item.Value = TokTok.Gen();

            DeNSo.Session.New.Set(item);
            return item;
        }

        public void Delete(string session) {
            Anth_TokenModel item = DeNSo.Session.New.Get<Anth_TokenModel>(i => i.Session == session).FirstOrDefault();
            if (item != null) {
                DeNSo.Session.New.Delete(item);
            }
        }

        public string Fetch(string session) {
            var item = GetBySession(session);
            var value = item.Value;
            DeNSo.Session.New.Delete(item);
            return value;
        }
    }

    public static class TokTok {

        public static string Gen() {
            string randomString = "";
            foreach (var s in RandomStrings(6)) {
                randomString += s.ToString();
            }
            return randomString;
        }

        public static string Gen(int lenght) {
            string randomString = "";
            foreach (var s in RandomStrings(lenght)) {
                randomString += s.ToString();
            }
            return randomString;
        }

        private static List<char> RandomStrings(int lenght) {
            const string AllowedChars = "0123456789";
            char[] allChar = AllowedChars.ToCharArray();
            List<char> chars = new List<char>();

            for (int i = 1; i <= lenght; i++) {
                Random rnd = new Random(Guid.NewGuid().GetHashCode());
                chars.Add(allChar[rnd.Next(0, allChar.Length)]);
            }

            return chars;
        }
    }
}