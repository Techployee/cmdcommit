using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CmdCommit.Tests
{
    public class BaseTest
    {
        public string JsonFilePath => Path.GetFullPath(Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\tests\", "cmdcommand.json"));

        public string TestDatafolder => Path.GetFullPath(Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\tests\testdata"));


        public BaseTest()
        {

        }

        [Test]
        public void JsonFileExists()
        {
            Console.WriteLine(JsonFilePath);
            Assert.True(File.Exists(JsonFilePath));
        }



    }
}
