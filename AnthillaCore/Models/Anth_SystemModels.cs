using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnthillaCore.Models {

    public class Anth_SessionModel : Anth_DelEntity {

        [Key]
        public string SessionId { get { return _Id; } set { _Id = value; } }

        public string SessionGuid { get; set; }

        public string ApplicationName { get; set; }

        public DateTime Created { get; set; }

        public DateTime Expires { get; set; }

        public DateTime LockDate { get; set; }

        public int LockId { get; set; }

        public int GoTimeout { get; set; }

        public int Locked { get; set; }

        public int Flags { get; set; }

        public string UserId { get; set; }

        public string UserUid { get; set; }
    }

    public class Anth_FeedbackModel : Anth_DelEntity {

        [Key]
        public string FeedbackId { get { return _Id; } set { _Id = value; } }

        public string FeedbackGuid { get; set; }

        public string User { get; set; }

        public string Comment { get; set; }

        public string Type { get; set; }

        public List<AnthillaCore.Logging.Anth_LogModel> Log { get; set; }
    }

    public class Anth_NamingModel : Anth_DelEntity {

        [Key]
        public string NamingId { get { return _Id; } set { _Id = value; } }

        public string NamingGuid { get; set; }

        public string NamingType { get; set; }

        public int CapInt { get; set; }

        public Tuple<bool, bool, bool> Capitalization { get; set; }

        public bool AliasRule { get; set; }

        public string Spacing { get; set; }

        public char SpaceChar { get; set; }

        public string Prefix { get; set; }

        public string Suffix { get; set; }

        public dynamic[] FileAliasElements { get; set; }

        public int FileAliasPosition { get; set; }
    }

    public class Anth_FunctionModel : Anth_DelEntity {

        [Key]
        public string FunctionId { get { return _Id; } set { _Id = value; } }

        public string FunctionGuid { get; set; }

        public string FunctionAlias { get; set; }

        public string FunctionValue { get; set; } //api

        public string FunctionType { get; set; }
    }

    public class Anth_VerificationUrlModel : Anth_DelEntity {

        [Key]
        public string VerificationUrlId { get { return _Id; } set { _Id = value; } }

        public string VerificationUrlGuid { get; set; }

        public string VerificationUrl { get; set; }

        public DateTime UrlCreation { get; set; }

        public int UrlValidity { get; set; }

        public string EmailToAddress { get; set; }

        public string UserGuid { get; set; }
    }

    public class smtpConfig : Anth_DelEntity {

        [Key]
        public string SmtpId { get { return _Id; } set { _Id = value; } }

        public string SmtpGuid { get; set; }

        public byte[] SmtpUrl { get; set; }

        public byte[] SmtpAccount { get; set; }

        public byte[] SmtpPassword { get; set; }
    }
}