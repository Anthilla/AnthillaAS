using System;
using System.Collections.Generic;
using System.Linq;
using AnthillaASCore.Models;
using AnthillaASCore.TagEngine.Repository;

namespace AnthillaASCore.TagEngine
{
    public class Anth_Tagger
    {
        private Anth_TagRepository anth_tagRepository = new Anth_TagRepository();

        public IEnumerable<string> Tagger(string tagStr)
        {
            if (tagStr == null)
            {
                var emptylist = new List<string>() { };
                return emptylist;
            }
            var TagList = new List<string>();
            var sList = new List<string>();
            var chr = new Char[] { ',' };
            List<string> split = tagStr.Split(chr).ToList();
            foreach (string s in split)
            {
                SetTag(s.Trim());
                sList.Add(s.Trim());
                TagList.Add(s);
            }
            return TagList;
        }

        public List<Anth_TagModel> PresetTagger(string tagStr)
        {
            if (tagStr == null)
            {
                SetTag("");
            }
            var TagList = new List<Anth_TagModel>();
            var chr = new Char[] { ',' };
            string[] split = tagStr.Split(chr);
            foreach (string s in split)
            {
                var tagToCreate = PresetSetTag(s.Trim());
                TagList.Add(tagToCreate);
            }
            return TagList;
        }

        /// <summary>
        /// controlla se il valore che sta per essere inserito esiste e lo crea solo se è unico
        /// </summary>
        public void SetTag(string value)
        {
            var table = anth_tagRepository.GetAll();
            var valList = new List<string>();
            foreach (Anth_Dump t in table)
            {
                valList.Add(t.AnthillaAlias.Trim());
            }
            if (valList.Contains(value) || value == "" || value == " ") { }
            else { anth_tagRepository.Create(value); }
            //Clear();
        }

        public Anth_TagModel PresetSetTag(string value)
        {
            var tag = anth_tagRepository.Create(value);
            return tag;
        }

        public static void Clear()
        {
            Anth_TagRepository repo = new Anth_TagRepository();
            var table = repo.GetAll();
            var valList = new List<string>();
            foreach (Anth_Dump t in table)
            {
                valList.Add(t.AnthillaAlias.Trim());
            }
            foreach (var v in valList)
            {
                valList.Remove(v);
                var parList = valList.ToList(); //a qui la lista è uguale all'originale meno che l'oggetto preso in esame
                foreach (var p in parList)
                {
                    if (v.Trim() == p.Trim())
                    {
                        var del = repo.GetByAlias(p);
                        repo.Delete(del.AnthillaGuid);
                    }
                }
            }
        }
    }
}