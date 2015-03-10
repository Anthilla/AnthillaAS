using AnthillaCore.Logging;
using AnthillaCore.Mapper;
using AnthillaCore.Models;
using AnthillaCore.Repository;
using AnthillaCore.Security;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnthillaCore.Mail {

    public class Anth_AddressBookRepository {
        private Anth_Mapper map = new Anth_Mapper();

        public IEnumerable<Anth_Dump> GetAll(string guid) {
            var abook = DeNSo.Session.New.Get<Anth_AddressBookModel>(model => model.UserGuid == guid && model.IsDeleted == false).FirstOrDefault();
            if (abook == null) {
                CreateBook(guid);
            }
            var table = abook.ContactList;
            var newtable = new List<Anth_Dump>();
            foreach (Contact item in table) {
                var i = map.ContactToDump(item);
                newtable.Add(i);
            }
            Anth_Log.TraceEvent("AddressBook Management", "Information", "tmp", "AddressBook getalldec");
            return newtable;
        }

        public bool GetCheck(string userguid) {
            var abook = DeNSo.Session.New.Get<Anth_AddressBookModel>(model => model.UserGuid == userguid && model.IsDeleted == false).FirstOrDefault();
            bool i;
            if (abook == null) { i = false; }
            else { i = true; }
            Anth_Log.TraceEvent("AddressBook check", "Information", "tmp", "AddressBook getbyId");
            return i;
        }

        public Anth_Dump GetById(string userguid, string id) {
            var abook = DeNSo.Session.New.Get<Anth_AddressBookModel>(model => model.UserGuid == userguid && model.IsDeleted == false).FirstOrDefault();
            if (abook == null) {
                CreateBook(userguid);
            }
            var table = abook.ContactList;

            var item = (from c in table
                        where c.ContactGuid == id
                        select c).FirstOrDefault();
            var n = new Anth_Dump();
            if (item == null) {
                n.AnthillaError = "404 Item not found";
                Anth_Log.TraceEvent("AddressBook Management", "Error", "tmp", "Item not found, id not exist");
                return n;
            }
            var i = map.ContactToDump(item);
            Anth_Log.TraceEvent("AddressBook Management", "Information", "tmp", "AddressBook getbyId");
            return i;
        }

        public Anth_AddressBookModel CreateBook(string userGuid) {
            var model = new Anth_AddressBookModel();
            model.ADt = DateTime.Now.ToString();
            model.Aned = "n";
            model.ARelGuid = Guid.NewGuid().ToString().Substring(0, 8);
            model.IsDeleted = false;
            model.StorIndexN2 = CoreSecurity.CreateRandomKey();
            model.StorIndexN1 = CoreSecurity.CreateRandomVector();
            model.AddressBookId = Guid.NewGuid().ToString();
            model.AddressBookGuid = userGuid;
            model.UserGuid = userGuid;
            model.ContactList = new List<Contact>() { };

            DeNSo.Session.New.Set(model);
            Anth_Log.TraceEvent("AddressBook Management", "Information", "tmp", "AddressBook Created");
            return model;
        }

        public Anth_AddressBookModel AddContact(string userGuid, string contactGuid, string fName, string lName, string email) {
            var model = new Contact();
            model.ADt = DateTime.Now.ToString();
            model.Aned = "n";
            model.ARelGuid = Guid.NewGuid().ToString().Substring(0, 8);
            model.IsDeleted = false;
            model.StorIndexN2 = CoreSecurity.CreateRandomKey();
            model.StorIndexN1 = CoreSecurity.CreateRandomVector();
            model.ContactId = Guid.NewGuid().ToString();
            model.ContactGuid = contactGuid;
            model.ContactFirstName = AnthillaSecurity.Encrypt(fName, model.StorIndexN2, model.StorIndexN1);
            model.ContactLastName = AnthillaSecurity.Encrypt(lName, model.StorIndexN2, model.StorIndexN1);
            model.ContactEmail = AnthillaSecurity.Encrypt(email, model.StorIndexN2, model.StorIndexN1);
            DeNSo.Session.New.Set(model);

            var addBook = DeNSo.Session.New.Get<Anth_AddressBookModel>(ab => ab.UserGuid == userGuid).FirstOrDefault();
            if (addBook == null) {
                CreateBook(userGuid);
            }
            addBook.ContactList.Add(model);
            DeNSo.Session.New.Set(addBook);
            return addBook;
        }

        public Anth_AddressRepository addRepo = new Anth_AddressRepository();

        public Contact AssignAddress(string contactGuid, string street, string number, string city, string pc, string country) {
            var contact = DeNSo.Session.New.Get<Contact>(c => c.ContactGuid == contactGuid).FirstOrDefault();
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
            DeNSo.Session.New.Set(address);

            contact.ContactAddress = address;
            DeNSo.Session.New.Set(contact);
            return contact;
        }

        public Anth_AddressBookModel RemoveContact(string userGuid, string contactGuid) {
            var addBook = DeNSo.Session.New.Get<Anth_AddressBookModel>(ab => ab.UserGuid == userGuid).FirstOrDefault();
            var item = (from i in addBook.ContactList
                        where i.ContactGuid == contactGuid
                        select i).FirstOrDefault();
            addBook.ContactList.Remove(item);
            Anth_Log.TraceEvent("AddressBook Management", "Information", "tmp", "AddressBook Deleted");
            DeNSo.Session.New.Set(addBook);
            return addBook;
        }
    }
}