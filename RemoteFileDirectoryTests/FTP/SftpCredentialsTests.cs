using Microsoft.VisualStudio.TestTools.UnitTesting;
using RemoteFileDirectory.FTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace RemoteFileDirectory.FTP.Tests
{
    [TestClass]
    public class SftpCredentialsTests
    {
        [TestMethod]
        public void SftpCredentialsTest()
        {
            var testConfigurationValues = new Dictionary<string, string>
            {
                {"App:RemoteUrl", "testUrl"},
                {"App:FtpUserName", "userName"},
                {"App:FtpPassword", "password"},
                {"App:FtpAuthMethod", "auth"},
                {"App:FtpServerFingerprintHex", "61a02bc07ddaa3c58db756bc5e43e75f"}
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(testConfigurationValues)
                .Build();

            ISftpCredentials credentials = new SftpCredentials(configuration);

            Assert.AreEqual("testUrl", credentials.RemoteUrl);
            Assert.AreEqual("userName", credentials.UserName);
            Assert.AreEqual("password", credentials.Password);
            Assert.AreEqual("auth", credentials.AuthenticationMethod);
            Assert.AreEqual("61a02bc07ddaa3c58db756bc5e43e75f", credentials.ServerFingerprintHex);
        }
    }
}