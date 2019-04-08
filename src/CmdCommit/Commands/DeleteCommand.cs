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

        /// <summary>
        /// Deletes a file.
        /// </summary>
        /// <param name="source"></param>
        public void DeleteFile(string source)
        {
            File.Delete(source);
        }

        /// <summary>
        /// Deletes a folder including all sub folders.
        /// </summary>
        /// <param name="source"></param>
        public void DeleteFolder(string source)
        {
            if (!Directory.Exists(source))
                return;

            foreach (var directory in Directory.EnumerateDirectories(source))
            {
                DeleteFolder(directory);
            }
            Directory.Delete(source, true);
        }
    }
}
