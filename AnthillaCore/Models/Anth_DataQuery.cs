using System;
using System.Collections.Generic;

namespace AnthillaCore.Models {

    public class Anth_DataQuery {

        public IEnumerable<string> ToStringList(string data) {
            var newList = new List<string>();
            var chr = new Char[] { '%' };
            string[] split = data.Split(chr);
            foreach (string s in split) { newList.Add(s); }
            return newList;
        }
    }
}