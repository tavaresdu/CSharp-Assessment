using System.Collections.Generic;
using BirthdayListModel.DAO;
using BirthdayListModel;
using System;

namespace BirthdayListBO
{
    internal class CommandAction : ICommandAction
    {
        public bool Init()
        {
            Console.WriteLine(string.Format("Pessoas que fazem aniversário hoje ({0}):", DateTime.Now.ToString("dd/MM")));
            List<Person> people = GetTodayBirthdays();
            if (people.Count > 0)
            {
                foreach (var person in people)
                {
                    int age = GetAge(person);
                    Console.WriteLine(string.Format("> {0} {1} ({2} {3})", 
                        person.Name, person.Surname, age, age > 1? "anos":"ano"));
                }
            }
            else
            {
                Console.WriteLine("Ninguém faz aniversário hoje.");
            }
            ShowCommands();
            return true;
        }

        public bool New()
        {
            Console.WriteLine("Novo cadastro:");
            Console.WriteLine();
            Person person = new Person();
            person.Name = ReadString("Primeiro nome: ");
            person.Surname = ReadString("Sobrenome: ");
            person.Birthdate = ReadDateTime("Data de Nascimento (dd/MM/yyyy): ");
            PersonDAO.Instance.Add(person);
            return true;
        }

        public bool Find()
        {
            string search = ReadName();
            Console.WriteLine("Sua busca encontrou os seguintes resultados:");
            ListPeople(search);
            ShowMessageAndWaitKeyPress("Aperte qualquer tecla para voltar ao menu principal...");

            return true;
        }

        public bool ListPeople()
        {
            Console.WriteLine("ID - Nome Sobrenome (Nascimento)");
            ListPeople("");
            ShowMessageAndWaitKeyPress("Aperte qualquer tecla para sair da listagem...");
            return true;
        }

        // Método que lista as pessoas filtrando por uma palavra-chave.
        // Caso a string seja vazia, lista todas as pessoas.
        public bool ListPeople(string search)
        {
            Console.WriteLine();
            foreach (var person in PersonDAO.Instance.GetAll())
            {
                if (PersonMatch(person, search))
                {
                    int days = GetDaysToNextBirthday(person);
                    string message = "Faz aniversário hoje!";
                    if (days > 1)
                    {
                        message = string.Format("Faltam {0} dias para seu aniversário.", days);
                    }
                    else if (days == 1)
                    {
                        message = "Seu aniversário é amanhã!";
                    }
                    Console.WriteLine(string.Format("{0} - {1} {2} ({3}) - {4}",
                        person.Id, person.Name, person.Surname,
                        person.Birthdate.ToString("dd/MM/yyyy"), message));
                }
            }
            Console.WriteLine();
            return true;
        }

        public bool Edit()
        {
            Console.WriteLine("Selecione uma pessoa para editar:");
            ListPeople("");
            int id = ReadId();
            if (id != 0)
            {
                Console.WriteLine(string.Format("Você está editando a pessoa de ID {0}!", id));
                Console.WriteLine("(Para manter o campo com o valor atual, deixe-o em branco)");
                Console.WriteLine();
                Person person = PersonDAO.Instance.FindById(id);
                person.Name = ReadString(string.Format("Primeiro nome (atual = {0}): ", person.Name), person.Name);
                person.Surname = ReadString(string.Format("Sobrenome (atual = {0}): ", person.Surname), person.Surname);
                person.Birthdate = ReadDateTime(string.Format("Data de Nascimento (atual = {0}): ", person.Birthdate.ToString("dd/MM/yyyy")), person.Birthdate);
                PersonDAO.Instance.Update(person);
                ShowMessageAndWaitKeyPress("Registro editado com sucesso! Aperte qualquer tecla para continuar...");
            }
            return true;
        }

        public bool Remove()
        {
            Console.WriteLine("Selecione uma pessoa para remover:");
            ListPeople("");
            int id = ReadId();
            if (id != 0)
            {
                PersonDAO.Instance.Remove(id);
                Console.WriteLine();
                ShowMessageAndWaitKeyPress("Registro removido com sucesso! Aperte qualquer tecla para continuar...");
            }
            return true;
        }

        public bool Exit()
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
                    Console.WriteLine(string.Format("{0} - {1}", letter, Command.GetDescription((Command.Letter)letter)));
                }
            }
        }

        private int ReadId()
        {
            while(true)
            {
                try
                {
                    Console.Write("Digite um ID (0 para sair): ");
                    int id = Int32.Parse(Console.ReadLine());
                    if (id == 0 || PersonDAO.Instance.FindById(id) != null)
                    {
                        return id;
                    }
                    else
                    {
                        Console.WriteLine("Esse ID não existe!");
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Digite apenas números!");
                }
            }
        }

        private string ReadName()
        {
            Console.Write("Digite nome e/ou sobrenome: ");
            return Console.ReadLine();
        }

        private string ReadString(string message)
        {
            return ReadString(message, null);
        }

            private string ReadString(string message, string defaultValue)
        {
            Console.Write(message);
            string value = Console.ReadLine().Trim();
            if (defaultValue != null && value == "")
            {
                return defaultValue;
            }
            return value;
        }

        private void ShowMessageAndWaitKeyPress(string message)
        {
            Console.Write(message);
            Console.ReadKey();
        }

        private DateTime ReadDateTime(string message)
        {
            return ReadDateTime(message, null);
        }

        private DateTime ReadDateTime(string message, DateTime? defaultValue)
        {
            while (true)
            {
                try
                {
                    Console.Write(message);
                    string value = Console.ReadLine().Trim();
                    if (defaultValue != null && value == "")
                    {
                        return defaultValue.Value;
                    }
                    return DateTime.ParseExact(value.Replace("/", ""), "ddMMyyyy",
                        System.Globalization.CultureInfo.InvariantCulture);
                }
                catch (Exception)
                {
                    Console.WriteLine("Algo deu errado! Verifique se a data é válida ou o formato digitado está correto!");
                }
            }
        }

        // Método que busca as pessoas que fazem aniversário no dia atual
        private List<Person> GetTodayBirthdays()
        {
            DateTime now = DateTime.Now;
            List<Person> birthdayNow = new List<Person>();
            foreach (var person in PersonDAO.Instance.GetAll())
            {
                if (now.Day == person.Birthdate.Day && now.Month == person.Birthdate.Month)
                {
                    birthdayNow.Add(person);
                }
            }
            return birthdayNow;
        }

        private int GetAge(Person person)
        {
            return DateTime.Now.Year - person.Birthdate.Year;
        }

        // Verifica se nome e sobrenome de uma certa pessoa bate com uma string de busca.
        private bool PersonMatch(Person person, string search)
        {
            return (person.Name.ToLower() + " " + person.Surname.ToLower()).Contains(search);
        }

        private int GetDaysToNextBirthday(Person person)
        {
            DateTime now = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            DateTime nextBirthday = new DateTime(now.Year, person.Birthdate.Month, person.Birthdate.Day);
            if (now.CompareTo(nextBirthday) > 0)
            {
                nextBirthday = new DateTime(now.Year + 1, person.Birthdate.Month, person.Birthdate.Day);
            }
            return Convert.ToInt32((nextBirthday - now).TotalDays);
        }
    }
}
