using System;
using System.Collections.Generic;

namespace AnthillaCore.FileManagement {

    public class KeyGenerator {

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