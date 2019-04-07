using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmdCommit.Commands
{
    public class DeleteCommand
    {
        public void DeleteFile(string source)
        {
            File.Delete(source);
        }

        public void DeleteFolder(string source)
        {
            if (!Directory.Exists(source))
                return;

            foreach (var dir in Directory.EnumerateDirectories(source))
            {
                DeleteFolder(dir);
            }
            Directory.Delete(source, true);
        }
    }
}
