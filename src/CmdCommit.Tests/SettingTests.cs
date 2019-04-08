using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CmdCommit.Tests
{
    public class SettingTests : BaseTest
    {
        Setting Setting { get; set; }
        public SettingTests()
        {
            CreateSetting();
        }

        [Test]
        public void CreateSetting()
        {
            IDictionary<string, object> environment = new Dictionary<string, object>()
            {
                {"$(WorkingDirectory)", "C:\\test" },
                {"$(SubDirectory)", "\\sub" }
            };

            IList<Command> commands = new List<Command>()
            {
                new Command()
                {
                    Class = "Copy",
                    Function = "CopyFolder",
                    Parameters = new Dictionary<string, object>
                    {
                        { "source",  "$(WorkingDirectory)\\$(SubDirectory)\\folder1" },
                        { "destination", "$(WorkingDirectory)\\$(SubDirectory)\\folder2" },
                        { "searchPattern", "*.*" }
                    }
                }

            };
            Setting = new Setting(environment, commands);

        }
    }
}
