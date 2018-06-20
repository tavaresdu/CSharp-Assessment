using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace BirthdayListModel.DAO
{
    public class PersonDAO
    {
        private string databaseName;
        private static PersonDAO instance = null;
        private People People { get; set; }

        private PersonDAO()
        {
            People = new People();
            databaseName = "DATABASE";
            ReadFile();
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
            person.Name = person.Name.Trim();
            person.Surname = person.Surname.Trim();
            if (FindById(person.Id) == null)
            {
                if (person.Id < 1)
                {
                    person.Id = People.Index;
                    People.Index++;
                }
                People.List.Add(person);
            }
            else
            {
                throw new Exception("Id column already exists");
            }
            SaveFile();
        }

        public List<Person> GetAll()
        {
            return new List<Person>(People.List);
        }

        public Person FindById(int id)
        {
            foreach (var person in People.List)
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
                foreach (var person in People.List)
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
                People.List.Insert(People.List.IndexOf(original), person);
                People.List.Remove(original);
            }
            else
            {
                throw new Exception("Id not found");
            }
            SaveFile();
        }

        public void Remove(int id)
        {
            People.List.Remove(FindById(id));
            SaveFile();
        }

        private void ReadFile()
        {
            string json;
            try
            {
                using (StreamReader streamReader = new StreamReader(databaseName))
                {
                    json = streamReader.ReadLine();
                }
                People = JsonConvert.DeserializeObject<People>(json);
            }
            catch (FileNotFoundException)
            {
                StreamWriter streamWriter = new StreamWriter(databaseName);
                streamWriter.Close();
                People.List = new List<Person>();
                People.Index = 1;
            }
        }

        private void SaveFile()
        {
            string json = JsonConvert.SerializeObject(People);
            using (StreamWriter streamWriter = new StreamWriter(databaseName))
            {
                streamWriter.Write(json);
            }
        }
    }
}
