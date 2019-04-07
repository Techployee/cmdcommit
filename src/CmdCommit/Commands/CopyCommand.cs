using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmdCommit.Commands
{
    public class CopyCommand
    {

        public void CopyFile(string source, string destination, bool overwrite)
        {
            File.Copy(source, destination, overwrite);
        }

        public void CopyFolder(string source, string destination, string searchPattern, bool recursive, bool overwrite)
        {
            foreach (string dirPath in Directory.GetDirectories(source, searchPattern,
                recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly))
            {
                Directory.CreateDirectory(dirPath.Replace(source, destination));
            }

            foreach (string newPath in Directory.GetFiles(source, searchPattern,
                SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(source, destination), overwrite);
            }
        }

    }
}
