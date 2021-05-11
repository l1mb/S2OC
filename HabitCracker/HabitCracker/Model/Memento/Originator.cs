using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HabitCracker.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace HabitCracker.Model.Memento
{
    internal class Originator
    {
        public CareTaker Caretaker = new();

        public CareTaker FillDays(System.Linq.IQueryable<HabitCracker.Model.Entities.Weekprogress> origin)
        {
            List<DayOfWeek> tempDayOfWeeks = new();

            DateTime currenTime = DateTime.Today.AddDays(15);
            var secDate = origin.OrderBy(p => p.Weekday).First().Weekday.Value.Date;
            //пока год и номер недели не станут ==
            while ((dates(currenTime).Item1 != dates(secDate).Item1 - 1) ^ (dates(currenTime).Item2 != dates(secDate).Item2))
            {
                while (currenTime.DayOfWeek != DayOfWeek.Monday)
                {
                    currenTime = currenTime.AddDays(-1);
                }
                //add null check
                foreach (var weekprogress in origin.OrderByDescending(p => p.Weekday))
                {
                    if (dates(currenTime).Item1 == dates((DateTime)weekprogress.Weekday).Item1)
                    {
                        if (weekprogress.Weekday != null && !tempDayOfWeeks.Contains(weekprogress.Weekday.Value.DayOfWeek)) tempDayOfWeeks.Add(weekprogress.Weekday.Value.DayOfWeek);
                    }
                }

                Caretaker.Save(new WeekMemento(JsonConvert.SerializeObject(tempDayOfWeeks)));
                currenTime = currenTime.AddDays(-7);
                tempDayOfWeeks = new();
            }

            Caretaker.ReverseUndoMementoes();
            return Caretaker;
        }

        public Tuple<int, int> dates(DateTime date)
        {
            var d = date;
            CultureInfo cul = CultureInfo.CurrentCulture;

            var firstDayWeek = cul.Calendar.GetWeekOfYear(
                d,
                CalendarWeekRule.FirstDay,
                DayOfWeek.Monday);

            int weekNum = cul.Calendar.GetWeekOfYear(
                d,
                CalendarWeekRule.FirstDay,
                DayOfWeek.Monday);

            int year = weekNum == 52 && d.Month == 1 ? d.Year - 1 : d.Year;
            return new(weekNum, year);
        }
    }
}