using BirthdayListModel;
using BirthdayListModel.DAO;
using System;
using System.Collections.Generic;

namespace BirthdayList
{
    class ScreenAction
    {
        public List<Person> getTodayBirthdays()
        {
            DateTime now = DateTime.Now;
            List<Person> birthdayNow = new List<Person>();
            foreach (var person in PersonDAO.Instance.List)
            {
                if (now.Day == person.Birthdate.Day && now.Month == person.Birthdate.Month)
                {
                    birthdayNow.Add(person);
                }
            }
            return birthdayNow;
        }
    }
}
