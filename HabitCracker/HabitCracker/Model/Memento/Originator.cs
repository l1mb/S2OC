using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using HabitCracker.Model.Entities;


namespace HabitCracker.Model.Memento
{
    internal class Originator
    {
        public CareTaker FillDays(System.Linq.IQueryable<HabitCracker.Model.Entities.HabitProgress> origin)
        {
            CareTaker Caretaker = new();
            List<DayOfWeek> tempDayOfWeeks = new();

            DateTime currenTime = DateTime.Today;
            if (!origin.OrderBy(p => p.Weekday).Any())
            {
                return new CareTaker();
            }
            var dateTime = origin.OrderBy(p => p.Weekday).First().Weekday;
            {
                var secDate = dateTime.Date;
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
                            if (!tempDayOfWeeks.Contains(weekprogress.Weekday.DayOfWeek)) tempDayOfWeeks.Add(weekprogress.Weekday.DayOfWeek);
                        }
                    }

                    Caretaker.Save(new WeekMemento(JsonConvert.SerializeObject(tempDayOfWeeks)));
                    currenTime = currenTime.AddDays(-7);
                    tempDayOfWeeks = new();
                }
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