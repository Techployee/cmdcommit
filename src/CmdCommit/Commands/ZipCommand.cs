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
            using (ZipArchive archive = System.IO.Compression.ZipFile.OpenRead(zip))
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
            using (ZipArchive archive = System.IO.Compression.ZipFile.OpenRead(zip))
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
            using (ZipArchive archive = System.IO.Compression.ZipFile.OpenRead(zip))
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

        public void CompressFile(string zip, string source, string destination)
        {
            destination = destination.Replace("\\", "/"); 
            // If the file doesn't exist then create it.
            if (!File.Exists(zip))
            {
                File.Create(zip).Close();
            }

            using (FileStream zipToOpen = new FileStream(zip, FileMode.Open))
            {
                using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
                {
                    // If the file already exists.
                    if (archive.GetEntry(destination) != null)
                    {
                        // Delete the file.
                        archive.GetEntry(destination).Delete();
                    }

                    // Create the file from the full source to the destination location inside the zip file.
                    archive.CreateEntryFromFile(new FileInfo(source).FullName, destination);
                }
            }
        }

        public void CompressFolder(string zip, string source, string searchPattern, string destination)
        {
            searchPattern = String.IsNullOrEmpty(searchPattern) ? "*.*" : searchPattern;
            destination = destination.Replace("\\", "/");

            // If the file doesn't exist then create it.
            if (!File.Exists(zip))
            {
                File.Create(zip).Close();
            }

            using (FileStream zipToOpen = new FileStream(zip, FileMode.Open))
            {
                using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
                {
                    foreach (string file in Directory.GetFiles(source, searchPattern, SearchOption.AllDirectories))
                    {
                        string zipFilePath = String.Concat(destination, Path.GetFullPath(file).Replace(source, "").Replace("\\", "/"));

                        // If the file already exists.
                        if (archive.GetEntry(zipFilePath) != null)
                        {
                            // Delete the file.
                            archive.GetEntry(zipFilePath).Delete();
                        }

                        // Create the file from the full source to the destination location inside the zip file.
                        archive.CreateEntryFromFile(new FileInfo(file).FullName, zipFilePath);
                    }
                }
            }
        }


        /// <summary>
        /// Combines the zip and destination path into a final path where the file will be extracted.
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        private string GetDestinationPath(ZipArchiveEntry entry, string destination)
        {
            return Path.GetFullPath(Path.Combine(destination, entry.FullName));
        }

        /// <summary>
        /// Create file or folder based on the ZipArchiveEntry name.
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="destination"></param>
        /// <param name="destinationPath"></param>
        /// <param name="overwrite"></param>
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
