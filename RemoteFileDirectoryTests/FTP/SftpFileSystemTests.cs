using Microsoft.VisualStudio.TestTools.UnitTesting;
using RemoteFileDirectory.FTP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteFileDirectory.FTP.Tests
{
    [TestClass]
    public class SftpFileSystemTests
    {
        private const string RemoteSftpDirectory = "unit_test_base_directory";
        private const string NewDirectory = "new_directory";
        private const string CopyToDirectoryName = "unit_test_copy_to";
        private const string CopyFromDirectoryName = "unit_test_copy_from";

        private static IFileSystem? _sftpClient;

        [ClassInitialize]
        public static void SetUpClass(TestContext context)
        {
            _sftpClient = new SftpFileSystem(new TestSftpCredentials());
            Assert.IsTrue(_sftpClient.Connect());
            _sftpClient.MoveDirectory(CopyToDirectoryName, CopyFromDirectoryName);

            _sftpClient.DeleteFilesAndFoldersInDirectory(RemoteSftpDirectory);
            

            string sourceFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SampleData/SFtpTestFile1.txt");
            FileInfo fi = new FileInfo(sourceFilePath);
            _sftpClient.UploadFiles($"{RemoteSftpDirectory}", fi);
        }

        [TestInitialize]
        public void SetUpEachTest()
        {
            _sftpClient!.Connect();
            _sftpClient!.DeleteFilesAndFoldersInDirectory(RemoteSftpDirectory);
        }

        [TestMethod]
        public void MoveDirectory_MovesADirectoryFromCurrentPathToAnother_ShouldSucceed()
        {
            _sftpClient!.MoveDirectory(CopyFromDirectoryName, CopyToDirectoryName);

        }

        [TestMethod]
        public void Connect_ConnectsToTheSFTPClient_ShouldSucceed()
        {
            Assert.IsTrue(_sftpClient!.Connect());
        }

        [TestMethod]
        public void UploadFile_SFTPsSingleFileToConfiguredLocation_ShouldSucceed()
        {
            string sourceFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SampleData/SFtpTestFile1.txt");
            FileInfo fi = new FileInfo(sourceFilePath);
            
            _sftpClient!.UploadFiles(RemoteSftpDirectory, fi);
            
            List<string> files = _sftpClient.ListFilesAndFoldersInDirectory(RemoteSftpDirectory);
            
            Assert.AreEqual(1, files.Count);
            Assert.AreEqual("SFtpTestFile1.txt", files[0]);
        }

        [TestMethod]
        public void UploadFiles_UploadToDirectoryThatDoesNotExist_ShouldCreateDirectoryAndSucceed()
        {
            string sourceFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SampleData/SFtpTestFile1.txt");
            FileInfo fi = new FileInfo(sourceFilePath);
            
            _sftpClient!.UploadFiles(NewDirectory, fi);
            
            List<string> files = _sftpClient.ListFilesAndFoldersInDirectory(NewDirectory);

            Assert.AreEqual(1, files.Count);
            Assert.AreEqual("SFtpTestFile1.txt", files[0]);
        }

        [TestMethod]
        public void UploadFiles_SFTPsMultipleFilesToConfiguredLocation_ShouldSucceed()
        {
            string sourceFilePathOne = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SampleData/SFtpTestFile1.txt");
            FileInfo fiOne = new FileInfo(sourceFilePathOne);

            string sourceFilePathTwo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SampleData/SFtpTestFile2.txt");
            FileInfo fiTwo = new FileInfo(sourceFilePathTwo);

            FileInfo[] fileInfos =  { fiOne, fiTwo };

            _sftpClient!.UploadFiles(RemoteSftpDirectory, fileInfos);

            List<string> files = _sftpClient.ListFilesAndFoldersInDirectory(RemoteSftpDirectory);

            Assert.AreEqual(2, files.Count);
            Assert.AreEqual("SFtpTestFile1.txt", files[0]);
            Assert.AreEqual("SFtpTestFile2.txt", files[1]);
        }
    }
}