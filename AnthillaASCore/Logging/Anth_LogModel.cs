using System.ComponentModel.DataAnnotations;
using AnthillaASCore.Models;

namespace AnthillaASCore.Logging
{
    public class Anth_LogModel : Anth_DelEntity
    {
        [Key]
        public string LogId { get { return _Id; } set { _Id = value; } }

        public string LogGuid { get; set; }

        public string AnthillaID { get; set; }

        public System.DateTime DateTime { get; set; }

        public string Level { get; set; } //error info warning

        public string Source { get; set; } //origine

        public string EventID { get; set; }

        public string Activity { get; set; }

        public string Keyword { get; set; }

        public string User { get; set; }

        public string OperativeCode { get; set; }

        public string Reg { get; set; }

        public string SessionID { get; set; }

        public string RelationID { get; set; }

        public string EventsSourceName { get; set; }

        public string Message { get; set; }
    }
}