using System;

namespace BirthdayListBO
{
    public class Command
    {
        private CommandAction action;
        public enum Letter { N, B, L, E, R, S, I }

        public Command()
        {
            action = new CommandAction();
        }

        public static Letter getValue(char c)
        {
            switch (c)
            {
                case 'N':
                    return Letter.N;
                case 'B':
                    return Letter.B;
                case 'L':
                    return Letter.L;
                case 'E':
                    return Letter.E;
                case 'R':
                    return Letter.R;
                case 'S':
                    return Letter.S;
                default:
                    return Letter.I;
            }
        }

        public static string GetDescription(Letter cc)
        {
            switch (cc)
            {
                case Letter.N:
                    return "Novo";
                case Letter.B:
                    return "Buscar";
                case Letter.L:
                    return "Listar";
                case Letter.E:
                    return "Editar";
                case Letter.R:
                    return "Remover";
                case Letter.S:
                    return "Sair";
                default:
                    return null;
            }
        }

        public Func<bool> Execute(Letter cc)
        {
            Console.Clear();
            switch (cc)
            {
                case Letter.N:
                    return action.New;
                case Letter.B:
                    return action.Find;
                case Letter.L:
                    return action.ListPeople;
                case Letter.E:
                    return action.Edit;
                case Letter.R:
                    return action.Remove;
                case Letter.S:
                    return action.Exit;
                default:
                    return action.Init;
            }
        }
    }
}