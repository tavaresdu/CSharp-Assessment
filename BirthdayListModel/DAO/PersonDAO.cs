using System;
using System.Collections.Generic;

namespace BirthdayListModel.DAO
{
    public class PersonDAO
    {
        private static PersonDAO instance = null;
        public List<Person> List { get; }
        private int index;

        private PersonDAO()
        {
            List = new List<Person>();
            index = 1;
        }

        public static PersonDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PersonDAO();
                }
                return instance;
            }
        }

        public void Add(Person person)
        {
            if (FindById(person.Id) == null)
            {
                if (person.Id < 1)
                {
                    person.Id = index;
                    index++;
                }
                List.Add(person);
            }
            else
            {
                throw new Exception("Id column already exists");
            }
        }

        public Person FindById(int id)
        {
            foreach (var person in List)
            {
                if (person.Id == id)
                {
                    return person;
                }
            }
            return null;
        }

        public List<Person> FindByNameAndSurname(string name)
        {
            List<Person> result = new List<Person>();
            name = name.Trim();
            if (name != "")
            {
                foreach (var person in List)
                {
                    if ((person.Name.ToLower() + " " + person.Surname.ToLower()).Contains(name.ToLower()))
                    {
                        result.Add(person);
                    }
                }
            }
            return result;
        }

        public void Update(Person person)
        {
            Person original = FindById(person.Id);
            if (original != null)
            {
                List.Insert(List.IndexOf(original), person);
                List.Remove(original);
            }
            else
            {
                throw new Exception("Id not found");
            }
        }

        public void Remove(int id)
        {
            List.Remove(FindById(id));
        }
    }
}
