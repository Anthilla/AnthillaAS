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
    public class Anth_CompanyRepository
    {
        private Anth_Tagger tag = new Anth_Tagger();
        private Anth_Mapper map = new Anth_Mapper();
        public IEnumerable<Anth_CompanyModel> CompanyTable = DeNSo.Session.New.Get<Anth_CompanyModel>(model => model.IsDeleted == false).ToArray();

        public IEnumerable<Anth_Dump> GetAll()
        {
            var table = new List<Anth_Dump>();
            foreach (Anth_CompanyModel item in CompanyTable)
            {
                var i = map.CompanyToDump(item);
                table.Add(i);
            }
            Anth_Log.TraceEvent("Company Management", "Information", "tmp", "Company getalldec");
            return table;
        }

        public Anth_Dump GetById(string id)
        {
            var item = DeNSo.Session.New.Get<Anth_CompanyModel>(c => c.CompanyGuid == id).FirstOrDefault();
            var n = new Anth_Dump();
            if (item == null)
            {
                n.AnthillaError = "404 Item not found";
                Anth_Log.TraceEvent("Company Management", "Error", "tmp", "Item not found, id not exist");
                return n;
            }
            var i = map.CompanyToDump(item);
            Anth_Log.TraceEvent("Company Management", "Information", "tmp", "Company getbyId");
            return i;
        }

        public Anth_Dump GetByAlias(string alias)
        {
            var table = GetAll();
            Anth_Dump company = (from c in table
                                 where c.AnthillaAlias == alias
                                 select c).FirstOrDefault();
            if (company == null)
            {
                var n = new Anth_Dump();
                n.AnthillaError = "404 Item not found";
                Anth_Log.TraceEvent("Company Management", "Error", "tmp", "Item not found, id not exist");
                return n;
            }
            Anth_Log.TraceEvent("Company Management", "Information", "tmp", "Company getbyAlias");
            return company;
        }

        public bool CheckAliasExist(string alias)
        {
            var table = GetAll();
            Anth_Dump company = (from c in table
                                 where c.AnthillaAlias == alias
                                 select c).FirstOrDefault();
            if (company == null)
            { return false; }
            else return true;
        }

        public Anth_CompanyModel Create(string guid, string alias, string tags)
        {
            var model = new Anth_CompanyModel();
            model.ADt = DateTime.Now.ToString();
            model.Aned = "n";
            model.ARelGuid = Guid.NewGuid().ToString().Substring(0, 8);
            model.IsDeleted = false;
            model.StorIndexN2 = CoreSecurity.CreateRandomKey();
            model.StorIndexN1 = CoreSecurity.CreateRandomVector();
            model.CompanyId = Guid.NewGuid().ToString();
            model.CompanyGuid = guid;
            model.CompanyAlias = AnthillaSecurity.Encrypt(alias, model.StorIndexN2, model.StorIndexN1);

            model.CompanyLanguage = AnthillaSecurity.Encrypt("", model.StorIndexN2, model.StorIndexN1);
            model.CompanyRelation = AnthillaSecurity.Encrypt("", model.StorIndexN2, model.StorIndexN1);
            model.Tags = tag.Tagger(tags).ToList();

            #region Nulls

            model.Attribute001 = "";
            model.Attribute002 = "";
            model.Attribute003 = "";
            model.Attribute004 = "";
            model.Attribute005 = "";
            model.Attribute006 = "";
            model.Attribute007 = "";
            model.Attribute008 = "";
            model.Attribute009 = "";
            model.Attribute010 = "";
            model.TagInput = "";

            #endregion Nulls

            DeNSo.Session.New.Set(model);
            Anth_Log.TraceEvent("Company Management", "Information", "tmp", "Company Created");
            return model;
        }

        public Anth_AddressRepository addRepo = new Anth_AddressRepository();

        public Anth_CompanyModel AssignAddress(string compGuid, string street, string number, string city, string pc, string country)
        {
            var company = DeNSo.Session.New.Get<Anth_CompanyModel>(c => c.CompanyGuid == compGuid).FirstOrDefault();
            var address = new Anth_AddressModel();
            address.StorIndexN2 = CoreSecurity.CreateRandomKey();
            address.StorIndexN1 = CoreSecurity.CreateRandomVector();
            address.AddressId = Guid.NewGuid().ToString();
            address.AddressGuid = Guid.NewGuid().ToString();
            address.StreetName = AnthillaSecurity.Encrypt(street, address.StorIndexN2, address.StorIndexN1);
            address.StreetNumber = AnthillaSecurity.Encrypt(number, address.StorIndexN2, address.StorIndexN1);
            address.City = AnthillaSecurity.Encrypt(city, address.StorIndexN2, address.StorIndexN1);
            address.PostalCode = AnthillaSecurity.Encrypt(pc, address.StorIndexN2, address.StorIndexN1);
            address.Country = AnthillaSecurity.Encrypt(country, address.StorIndexN2, address.StorIndexN1);
            address.State = AnthillaSecurity.Encrypt("", address.StorIndexN2, address.StorIndexN1);
            //check for duplicati
            DeNSo.Session.New.Set(address);

            company.CompanyAddress = address;
            DeNSo.Session.New.Set(company);
            return company;
        }

        public Anth_CompanyModel MakeOwner(string guid)
        {
            var companies = DeNSo.Session.New.Get<Anth_CompanyModel>(c => c.IsDeleted == false && c.CompanyOwner == 1).ToList();
            foreach (var company in companies)
            {
                if (company != null)
                {
                    company.CompanyOwner = 0;
                    DeNSo.Session.New.Set(company);
                }
            }
            var item = DeNSo.Session.New.Get<Anth_CompanyModel>(model => model.CompanyGuid == guid && model.IsDeleted == false).FirstOrDefault();
            item.CompanyOwner = 1;
            DeNSo.Session.New.Set(item);
            return item;
        }

        public Anth_Dump GetOwner()
        {
            var table = GetAll();
            Anth_Dump company = (from c in table
                                 where c.AnthillaOwner == 1
                                 select c).FirstOrDefault();
            if (company == null)
            {
                var n = new Anth_Dump();
                n.AnthillaError = "404 Item not found";
                Anth_Log.TraceEvent("Company Management", "Error", "tmp", "Item not found, id not exist");
                return n;
            }
            Anth_Log.TraceEvent("Company Management", "Information", "tmp", "Company get owner");
            return company;
        }

        public Anth_CompanyModel Edit(string id, string alias, string tags)
        {
            var oldItem = DeNSo.Session.New.Get<Anth_CompanyModel>(m => m.CompanyGuid == id && m.IsDeleted == false).FirstOrDefault();
            oldItem.IsDeleted = true;
            oldItem.Aned = "e";
            var model = new Anth_CompanyModel();
            model.ADt = DateTime.Now.ToString();
            model.Aned = "n";
            model.ARelGuid = oldItem.ARelGuid;
            model.IsDeleted = false;
            model.StorIndexN2 = CoreSecurity.CreateRandomKey();
            model.StorIndexN1 = CoreSecurity.CreateRandomVector();
            model.CompanyId = Guid.NewGuid().ToString();
            model.CompanyGuid = oldItem.CompanyGuid;
            model.CompanyAlias = AnthillaSecurity.Encrypt(alias, model.StorIndexN2, model.StorIndexN1);

            model.CompanyLanguage = AnthillaSecurity.Encrypt("", model.StorIndexN2, model.StorIndexN1);
            model.CompanyRelation = AnthillaSecurity.Encrypt("", model.StorIndexN2, model.StorIndexN1);
            model.Tags = tag.Tagger(tags).ToList();

            #region Nulls

            model.Attribute001 = oldItem.Attribute001;
            model.Attribute002 = oldItem.Attribute002;
            model.Attribute003 = oldItem.Attribute003;
            model.Attribute004 = oldItem.Attribute004;
            model.Attribute005 = oldItem.Attribute005;
            model.Attribute006 = oldItem.Attribute006;
            model.Attribute007 = oldItem.Attribute007;
            model.Attribute008 = oldItem.Attribute008;
            model.Attribute009 = oldItem.Attribute009;
            model.Attribute010 = oldItem.Attribute010;
            model.TagInput = oldItem.TagInput;

            #endregion Nulls

            DeNSo.Session.New.Set(oldItem);
            DeNSo.Session.New.Set(model);
            Anth_Log.TraceEvent("Company Management", "Information", "tmp", "Company Edited");
            return model;
        }

        public Anth_CompanyModel Delete(string id)
        {
            var deletedItem = DeNSo.Session.New.Get<Anth_CompanyModel>(model => model.CompanyGuid == id && model.IsDeleted == false).FirstOrDefault();
            if (deletedItem != null)
            {
                deletedItem.ADt = DateTime.Now.ToString();
                deletedItem.Aned = "d";
                deletedItem.IsDeleted = true;
                DeNSo.Session.New.Set(deletedItem);
                Anth_Log.TraceEvent("Company Management", "Information", "tmp", "Company Deleted");
            }
            return deletedItem;
        }
    }
}