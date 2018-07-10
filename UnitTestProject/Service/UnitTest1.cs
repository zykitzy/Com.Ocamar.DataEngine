using System;
using System.Reflection;
using Com.Ocamar.DataEngine.Common.Helper.Status;
using Com.Ocamar.DataEngine.Service.Formatter;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject.Service
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            byte[] test = { 0x3e, 0x0d, 0x25 };
            //FormatBase format = new FormatBase();
            //Console.WriteLine(format.DataFlag(test));
        }
    }
}
