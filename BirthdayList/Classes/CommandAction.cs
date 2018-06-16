using BirthdayList.Classes;
using BirthdayListModel;
using BirthdayListModel.DAO;
using System;
using System.Collections.Generic;

namespace BirthdayList
{
    class CommandAction
    {
        public bool Init()
        {
            DateTime now = DateTime.Now;
            Console.WriteLine(string.Format("Pessoas que fazem aniversário hoje ({0}):", now.ToString("dd/MM")));
            List<Person> people = getTodayBirthdays();
            if (people.Count > 0)
            {
                foreach (var person in people)
                {
                    Console.WriteLine(string.Format("> {0} {1}", person.Name, person.Surname));
                }
            }
            else
            {
                Console.WriteLine("Ninguém faz aniversário hoje.");
            }
            ShowCommands();
            return true;
        }

        public bool Novo()
        {
            Person person = new Person();
            person.Name = ReadFirstName();
            person.Surname = ReadSurname();
            person.Birthdate = ReadBirthdate();
            PersonDAO.Instance.Add(person);
            return true;
        }

        public bool Buscar()
        {
            return false;
        }

        public bool Listar()
        {
            return false;
        }

        public bool Editar()
        {
            return false;
        }

        public bool Remover()
        {
            return false;
        }

        public bool Sair()
        {
            return false;
        }

        private void ShowCommands()
        {
            Console.WriteLine();
            foreach (var letter in Enum.GetValues(typeof(Command.Letter)))
            {
                if (!letter.Equals(Command.Letter.I))
                {
                    Console.WriteLine(String.Format("{0} - {1}", letter, Command.GetDescription((Command.Letter)letter)));
                }
            }
        }

        private String ReadName()
        {
            Console.Write("Digite nome e/ou sobrenome: ");
            return Console.ReadLine();
        }

        private String ReadFirstName()
        {
            Console.Write("Primeiro nome: ");
            return Console.ReadLine();
        }

        private String ReadSurname()
        {
            Console.Write("Sobrenome: ");
            return Console.ReadLine();
        }

        private DateTime ReadBirthdate()
        {
            while (true)
            {
                try
                {
                    Console.Write("Nascimento (dd/MM/yyyy): ");
                    return DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy",
                        System.Globalization.CultureInfo.InvariantCulture);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Algo deu errado! Verifique se o formato de data digitado está correto!");
                }
            }
        }

        private List<Person> getTodayBirthdays()
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
