using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnthillaASCore.Models
{
    public class Anth_DayModel : Anth_DelEntity
    {
        [Key]
        public string DayId { get { return _Id; } set { _Id = value; } }

        public string DayGuid { get; set; }

        public string DayName { get; set; }

        public string DayInt { get; set; }

        public string MonthName { get; set; }

        public string MonthInt { get; set; }

        public string Year { get; set; }
    }

    public class Anth_EventModel : Anth_DelEntity
    {
        [Key]
        public string EventId { get { return _Id; } set { _Id = value; } }

        public string EventGuid { get; set; }

        public byte[] EventAlias { get; set; }

        public byte[] EventType { get; set; }

        public byte[] EventDetail { get; set; }

        public string EventDate { get; set; } //yyyyMMdd

        public byte[] EventLenght { get; set; }

        public string EventStart { get; set; }

        public string EventEnd { get; set; }

        public IEnumerable<string> EventUserIds { get; set; }

        public IEnumerable<string> EventProjectIds { get; set; }

        public IEnumerable<string> EventCompanyIds { get; set; }
    }
}