using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnthillaASCore.Models
{
    public class Anth_SessionModel : Anth_DelEntity
    {
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

    public class Anth_FeedbackModel : Anth_DelEntity
    {
        [Key]
        public string FeedbackId { get { return _Id; } set { _Id = value; } }

        public string FeedbackGuid { get; set; }

        public string User { get; set; }

        public string Comment { get; set; }

        public string Type { get; set; }

        public List<AnthillaASCore.Logging.Anth_LogModel> Log { get; set; }
    }

    public class Anth_NamingModel : Anth_DelEntity
    {
        [Key]
        public string NamingId { get { return _Id; } set { _Id = value; } }

        public string NamingGuid { get; set; }

        public byte[] NamingAlias { get; set; }

        public string NamingType { get; set; }

        public byte[] Prefix { get; set; }

        public byte[] Suffix { get; set; }
    }

    public class Anth_FunctionModel : Anth_DelEntity
    {
        [Key]
        public string FunctionId { get { return _Id; } set { _Id = value; } }

        public string FunctionGuid { get; set; }

        public string FunctionAlias { get; set; }

        public string FunctionValue { get; set; } //api
    }
}