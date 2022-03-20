using Microsoft.VisualStudio.TestTools.UnitTesting;
using RemoteFileDirectory.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteFileDirectory.Utils.Tests
{
    [TestClass]
    public class HexByteConverterTests
    {
        [TestMethod]
        public void ConvertHexStringToByteArrayTest()
        {
            const string hex = "61a02bc07ddaa3c58db756bc5e43e75f";
            IHexByteConverter hexByteConverter = new HexByteConverter();
            byte[] buffer = hexByteConverter.ConvertHexStringToByteArray(hex);

            string actual = hexByteConverter.ConvertByteArrayToHexString(buffer);
            Assert.AreEqual(hex, actual);

        }
    }
}