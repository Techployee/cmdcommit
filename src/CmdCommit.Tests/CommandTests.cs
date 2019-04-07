using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CmdCommit.Commands;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace CmdCommit.Tests
{
    public class CommandTests : BaseTest
    {

        [Test]
        public void ConvertCommands()
        {
            dynamic json = JsonConvert.DeserializeObject(File.ReadAllText(JsonFilePath));
            List<Command> jsonCommands = JsonConvert.DeserializeObject<List<Command>>(json.commands.ToString());
        }


        [Test]
        public void InvokeCopyCommand()
        {
            Command command = new Command()
            {
                Class = "Copy",
                Function = "CopyFolder",
                Parameters = new Dictionary<string, object>
                {
                    { "source", Path.Combine(TestDatafolder, "folder1") },
                    { "destination", Path.Combine(TestDatafolder, "folder2") },
                    { "searchPattern", "*.*" }
                }
            };
            command.Invoke();
        }

        [Test]
        public void InvokeDeleteFileCommand()
        {
            Command command = new Command()
            {
                Class = "Delete",
                Function = "DeleteFile",
                Parameters = new Dictionary<string, object>
                {
                    { "source", Path.Combine(TestDatafolder, "folder1", "test123.txt") }
                }
            };
            command.Invoke();
        }

        [Test]
        public void InvokeDeleteFolderCommand()
        {
            Command command = new Command()
            {
                Class = "Delete",
                Function = "DeleteFolder",
                Parameters = new Dictionary<string, object>
                {
                    { "source", Path.Combine(TestDatafolder, "folder2") }
                }
            };
            command.Invoke();
        }

        [Test]
        public void InvokeZipExtractCommand()
        {
            Command command = new Command()
            {
                Class = "Zip",
                Function = "Extract",
                Parameters = new Dictionary<string, object>
                {
                    { "zip", Path.Combine(TestDatafolder, "folder2.zip") },
                    { "destination", Path.Combine(TestDatafolder) },
                    { "overwrite", true }
                }
            };
            command.Invoke();
        }

        [Test]
        public void InvokeZipExtractFileCommand()
        {
            Command command = new Command()
            {
                Class = "Zip",
                Function = "ExtractFile",
                Parameters = new Dictionary<string, object>
                {
                    { "zip", Path.Combine(TestDatafolder, "folder2.zip") },
                    { "source", "folder2/test1/test789.txt" },
                    { "destination", Path.Combine(TestDatafolder) },
                    { "overwrite", true }
                }
            };
            command.Invoke();
        }

        [Test]
        public void InvokeZipExtractFolderCommand()
        {
            Command command = new Command()
            {
                Class = "Zip",
                Function = "ExtractFolder",
                Parameters = new Dictionary<string, object>
                {
                    { "zip", Path.Combine(TestDatafolder, "folder2.zip") },
                    { "source", "folder2/test1/" },
                    { "destination", Path.Combine(TestDatafolder) },
                    { "overwrite", true }
                }
            };
            command.Invoke();
        }

    }
}
