using System;
using System.ComponentModel;

namespace BirthdayList
{
    class Screen
    {
        private Action action;
        enum Commands
        {
            [Description("Novo")] N,
            [Description("Buscar")] B,
            [Description("Listar")] L,
            [Description("Editar")] E,
            [Description("Remover")] R
        }

        public Screen()
        {
            action = new Action();
        }

        public void Init()
        {
            DateTime now = DateTime.Now;
            Console.WriteLine(string.Format("Pessoas que fazem aniversário hoje ({0}):", now.ToString("dd/MM")));
            foreach (var person in action.getTodayBirthdays())
            {
                Console.WriteLine(string.Format("> {0} {1}", person.Name, person.Surname));
            }
        }

        public void ShowCommands()
        {
            foreach (var )
        }
    }
}
