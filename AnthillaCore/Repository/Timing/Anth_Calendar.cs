using AnthillaCore.Models;
using System;
using System.Collections.Generic;

namespace AnthillaCore.Repository {

    public class Anth_Calendar {

        public Anth_DayModel SetDay(DateTime date) {
            var day = new Anth_DayModel();
            day.ADt = DateTime.Now.ToString();
            day.Aned = "n";
            day.ARelGuid = Guid.NewGuid().ToString().Substring(0, 8);
            day.IsDeleted = false;
            day.DayId = Guid.NewGuid().ToString();
            day.DayGuid = Guid.NewGuid().ToString();

            day.DayName = date.ToString("dddd");
            day.DayInt = date.ToString("dd");
            day.MonthName = date.ToString("MMMM");
            day.MonthInt = date.ToString("MM");
            day.Year = date.ToString("yyyy");

            return day;
        }

        public Anth_DayModel ConvertDay(string date) {
            var day = new Anth_DayModel();
            day.ADt = DateTime.Now.ToString();
            day.Aned = "n";
            day.ARelGuid = Guid.NewGuid().ToString().Substring(0, 8);
            day.IsDeleted = false;
            day.DayId = Guid.NewGuid().ToString();
            day.DayGuid = Guid.NewGuid().ToString();

            day.MonthInt = date.Substring(0, 2);
            day.DayInt = date.Substring(2, 2);
            day.Year = date.Substring(4, 4);

            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);
            string dtStr = day.MonthInt + "/" + day.DayInt + "/" + day.Year;
            DateTime dt = DateTime.Parse(dtStr, culture);

            day.MonthName = dt.ToString("MMMM");
            day.DayName = dt.ToString("dddd");

            return day;
        }

        public List<Anth_DayModel> ReturnWeek(DateTime date) {
            var week = new List<Anth_DayModel>();
            var today = SetDay(date);
            var to1 = SetDay(date.AddDays(1));
            var to2 = SetDay(date.AddDays(2));
            var ye1 = SetDay(date.AddDays(-1));
            week.Add(today);
            week.Add(to1);
            week.Add(to2);
            week.Add(ye1);
            return week;
        }

        public Anth_DayModel ReturnDay(DateTime date) {
            Anth_DayModel day = SetDay(date);
            return day;
        }

        public Anth_DayModel Day(string date) {
            Anth_DayModel day = ConvertDay(date);
            return day;
        }
    }
}