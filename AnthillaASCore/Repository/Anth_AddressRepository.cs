using System;
using System.Collections.Generic;
using System.Linq;
using AnthillaASCore.Logging;
using AnthillaASCore.Mapper;
using AnthillaASCore.Models;
using AnthillaASCore.Security;
using AnthillaASCore.TagEngine;

namespace AnthillaASCore.Repositories
{
    public class Anth_AddressRepository
    {
        private Anth_Tagger tag = new Anth_Tagger();
        private Anth_Mapper map = new Anth_Mapper();
        public IEnumerable<Anth_AddressModel> AddressTable = DeNSo.Session.New.Get<Anth_AddressModel>(model => model.IsDeleted == false).ToArray();

        public IEnumerable<Anth_Dump> GetAll()
        {
            var table = new List<Anth_Dump>();
            foreach (Anth_AddressModel item in AddressTable)
            {
                var i = map.AddressToDump(item);
                table.Add(i);
            }
            Anth_Log.TraceEvent("Address Management", "Information", "tmp", "Address getalldec");
            return table;
        }

        public Anth_Dump GetById(string id)
        {
            var item = DeNSo.Session.New.Get<Anth_AddressModel>(c => c.AddressGuid == id).FirstOrDefault();
            var n = new Anth_Dump();
            if (item == null)
            {
                n.AnthillaError = "404 Item not found";
                Anth_Log.TraceEvent("Address Management", "Error", "tmp", "Item not found, id not exist");
                return n;
            }
            var i = map.AddressToDump(item);
            Anth_Log.TraceEvent("Address Management", "Information", "tmp", "Address getbyId");
            return i;
        }

        public Anth_AddressModel Create(string street, string number, string city, string pc, string country, string state)
        {
            var model = new Anth_AddressModel();

            model.ADt = DateTime.Now.ToString();
            model.Aned = "n";
            model.ARelGuid = Guid.NewGuid().ToString().Substring(0, 8);
            model.IsDeleted = false;
            model.StorIndexN2 = CoreSecurity.CreateRandomKey();
            model.StorIndexN1 = CoreSecurity.CreateRandomVector();
            model.AddressId = Guid.NewGuid().ToString();
            model.AddressGuid = Guid.NewGuid().ToString();

            model.StreetName = AnthillaSecurity.Encrypt(street, model.StorIndexN2, model.StorIndexN1);
            model.StreetNumber = AnthillaSecurity.Encrypt(number, model.StorIndexN2, model.StorIndexN1);
            model.City = AnthillaSecurity.Encrypt(city, model.StorIndexN2, model.StorIndexN1);
            model.PostalCode = AnthillaSecurity.Encrypt(pc, model.StorIndexN2, model.StorIndexN1);
            model.Country = AnthillaSecurity.Encrypt(country, model.StorIndexN2, model.StorIndexN1);
            model.State = AnthillaSecurity.Encrypt(state, model.StorIndexN2, model.StorIndexN1);

            DeNSo.Session.New.Set(model);
            Anth_Log.TraceEvent("Address Management", "Information", "tmp", "Address Created");
            return model;
        }

        public Anth_AddressModel Edit(string guid, string street, string number, string city, string pc, string country, string state)
        {
            var oldItem = DeNSo.Session.New.Get<Anth_AddressModel>(m => m.AddressGuid == guid && m.IsDeleted == false).FirstOrDefault();
            oldItem.IsDeleted = true;
            oldItem.Aned = "e";
            var model = new Anth_AddressModel();
            model.ADt = DateTime.Now.ToString();
            model.Aned = "n";
            model.ARelGuid = oldItem.ARelGuid;
            model.IsDeleted = false;
            model.StorIndexN2 = CoreSecurity.CreateRandomKey();
            model.StorIndexN1 = CoreSecurity.CreateRandomVector();
            model.AddressId = Guid.NewGuid().ToString();
            model.AddressGuid = oldItem.AddressGuid;

            model.StreetName = AnthillaSecurity.Encrypt(street, model.StorIndexN2, model.StorIndexN1);
            model.StreetNumber = AnthillaSecurity.Encrypt(number, model.StorIndexN2, model.StorIndexN1);
            model.City = AnthillaSecurity.Encrypt(city, model.StorIndexN2, model.StorIndexN1);
            model.PostalCode = AnthillaSecurity.Encrypt(pc, model.StorIndexN2, model.StorIndexN1);
            model.Country = AnthillaSecurity.Encrypt(country, model.StorIndexN2, model.StorIndexN1);
            model.State = AnthillaSecurity.Encrypt(state, model.StorIndexN2, model.StorIndexN1);

            DeNSo.Session.New.Set(oldItem);
            DeNSo.Session.New.Set(model);
            Anth_Log.TraceEvent("Address Management", "Information", "tmp", "Address Edited");
            return model;
        }

        public Anth_AddressModel Delete(string id)
        {
            var deletedItem = DeNSo.Session.New.Get<Anth_AddressModel>(model => model.AddressGuid == id && model.IsDeleted == false).FirstOrDefault();
            if (deletedItem != null)
            {
                deletedItem.ADt = DateTime.Now.ToString();
                deletedItem.Aned = "d";
                deletedItem.IsDeleted = true;
                DeNSo.Session.New.Set(deletedItem);
                Anth_Log.TraceEvent("Address Management", "Information", "tmp", "Address Deleted");
            }
            return deletedItem;
        }
    }
}