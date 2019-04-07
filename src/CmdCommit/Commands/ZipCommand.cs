using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmdCommit.Commands
{
    public class ZipCommand 
    {
        /// <summary>
        /// Extract a zip file into a directory
        /// </summary>
        /// <param name="zip"></param>
        /// <param name="destination"></param>
        /// <param name="overwrite"></param>
        public void Extract(string zip, string destination, bool overwrite)
        {
            using (ZipArchive archive = ZipFile.OpenRead(zip))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    CreateFolderOrFile(entry, destination, GetDestinationPath(entry, destination), overwrite);
                }
            }
        }

        /// <summary>
        /// Extract a single file inside of a zip file into a directory.
        /// </summary>
        /// <param name="zip"></param>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="overwrite"></param>
        public void ExtractFile(string zip, string source, string destination, bool overwrite)
        {
            using (ZipArchive archive = ZipFile.OpenRead(zip))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    // If the file doesn't match, continue the loop.
                    if(!entry.FullName.Equals(source))
                    {
                        continue;
                    }
                    string destinationPath = GetDestinationPath(entry, destination);
                    if (!Directory.Exists(Path.GetDirectoryName(destinationPath)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(destinationPath));
                    }
                    entry.ExtractToFile(destinationPath, overwrite);
                }
            }
        }

        /// <summary>
        /// Extract a folder inside of a zip file into a directory.
        /// </summary>
        /// <param name="zip"></param>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="overwrite"></param>
        public void ExtractFolder(string zip, string source, string destination, bool overwrite)
        {
            using (ZipArchive archive = ZipFile.OpenRead(zip))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    // If the file doesn't match, continue the loop.
                    if (!entry.FullName.Contains(source))
                    {
                        continue;
                    }
                    CreateFolderOrFile(entry, destination, GetDestinationPath(entry, destination), overwrite);
                }
            }
        }

        /*public void ZipFile(string zipFile, string source)
        {

        }

        public void ZipFolder(string zipFile, string source, bool recursive)
        {

        }*/


        private string GetDestinationPath(ZipArchiveEntry entry, string destination)
        {
            return Path.GetFullPath(Path.Combine(destination, entry.FullName));
        }

        private void CreateFolderOrFile(ZipArchiveEntry entry, string destination, string destinationPath, bool overwrite)
        {
            if (destinationPath.StartsWith(destination, StringComparison.Ordinal))
            {
                // Directory
                if (entry.FullName.EndsWith("/"))
                {
                    Directory.CreateDirectory(destinationPath);
                }
                // File
                else
                {
                    entry.ExtractToFile(destinationPath, overwrite);
                }
            }
        }
    }
}
