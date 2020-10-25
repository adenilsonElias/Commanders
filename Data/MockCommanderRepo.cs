using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data
{
    public class MockCommanderRepo : ICommanderRepo
    {
        public void createCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public void deleteCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Command> getAllCommands()
        {
            var commands = new List<Command>
            {
                new Command{ Id=0, HowTo="Boil an egg", Line="Boil water", Plataform="Kettle & Pan"},
                new Command{ Id=1, HowTo="Boil an egg", Line="Boil water", Plataform="Kettle & Pan"},
                new Command{ Id=2, HowTo="Boil an egg", Line="Boil water", Plataform="Kettle & Pan"},
                new Command{ Id=3, HowTo="Boil an egg", Line="Boil water", Plataform="Kettle & Pan"},
            };
            return commands;
        }

        public Command getCommandById(int id)
        {
            return new Command
            {
                Id = 0,
                HowTo = "Boil an egg",
                Line = "Boil water",
                Plataform = "Kettle & Pan"
            };
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void updateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }
    }
}