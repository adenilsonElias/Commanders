using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data
{
    public interface ICommanderRepo
    {
        bool SaveChanges();

        IEnumerable<Command> getAllCommands();
        Command getCommandById(int id);
        void createCommand(Command cmd);
        void updateCommand(Command cmd);
        void deleteCommand(Command cmd);
        
    }
}