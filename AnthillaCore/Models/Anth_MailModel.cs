using ImapX;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnthillaCore.Models {

    public class Anth_MailConfig : Anth_DelEntity {

        [Key]
        public string MailConfigId { get { return _Id; } set { _Id = value; } }

        public string MailConfigGuid { get; set; }

        public string UserGuid { get; set; }

        public List<MailSetting> Setting { get; set; }
    }

    public class MailSetting {

        public byte[] Type { get; set; }

        public byte[] ImapUrl { get; set; }

        public byte[] Account { get; set; }

        public byte[] Password { get; set; }
    }

    public class Anth_MailModel : Anth_DelEntity {

        [Key]
        public string MailId { get { return _Id; } set { _Id = value; } }

        public string MailGuid { get; set; }

        public MailAddress MailFrom { get; set; }

        public byte[] strMailFrom { get; set; }

        public IEnumerable<MailAddress> MailTo { get; set; }

        public IEnumerable<byte[]> strMailTo { get; set; }

        public MessageBody MailBody { get; set; }

        public byte[] strMailBody { get; set; }

        public byte[] MailBodyText { get; set; }

        public MessageContent[] MailBodyParts { get; set; }

        public string[] strMailBodyParts { get; set; }

        public DateTime? MailDate { get; set; }

        public byte[] MailSubject { get; set; }

        public string strMailSubject { get; set; }

        public string MailboxId { get; set; }

        public List<string> CompanyIds { get; set; }

        public List<string> ProjectIds { get; set; }

        public bool isRead { get; set; }

        public string MessageType { get; set; }
    }

    public class Anth_MailboxModel : Anth_DelEntity {

        [Key]
        public string MailboxId { get { return _Id; } set { _Id = value; } }

        public string MailboxGuid { get; set; }

        public byte[] MailboxAlias { get; set; }

        public string strMailboxAlias { get; set; }
    }

    public class Anth_ClientModel : Anth_DelEntity {

        [Key]
        public string ClientId { get { return _Id; } set { _Id = value; } }

        public string ClientGuid { get; set; }

        public string ClientAlias { get; set; }

        public string ClientAddress { get; set; }
    }

    public class Anth_AddressBookModel : Anth_DelEntity {

        [Key]
        public string AddressBookId { get { return _Id; } set { _Id = value; } }

        public string AddressBookGuid { get; set; }

        public string UserGuid { get; set; }

        public List<Contact> ContactList { get; set; }
    }

    public class Contact : Anth_DelEntity {

        [Key]
        public string ContactId { get { return _Id; } set { _Id = value; } }

        public string ContactGuid { get; set; }

        public string ContactCompanyGuid { get; set; }

        public byte[] ContactFirstName { get; set; }

        public byte[] ContactLastName { get; set; }

        public byte[] ContactEmail { get; set; }

        public Anth_AddressModel ContactAddress { get; set; }
    }
}