using System;
using System.Collections.Generic;

namespace AnthillaCore {

    public static class RSGen {

        public static string Create() {
            string randomString = "";
            foreach (var s in RandomStrings(6)) {
                randomString += s.ToString();
            }
            return randomString;
        }

        public static string Create(int lenght) {
            string randomString = "";
            foreach (var s in RandomStrings(lenght)) {
                randomString += s.ToString();
            }
            return randomString;
        }

        private static List<char> RandomStrings(int length) {
            const string AllowedChars = "0123456789";
            char[] allChar = AllowedChars.ToCharArray();
            List<char> chars = new List<char>();

            for (int i = 1; i <= length; i++) {
                Random rnd = new Random(Guid.NewGuid().GetHashCode());
                chars.Add(allChar[rnd.Next(0, allChar.Length)]);
            }

            return chars;
        }
    }
}