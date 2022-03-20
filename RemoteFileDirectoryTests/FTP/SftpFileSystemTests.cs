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
        private const string RemoteTestCopyTooDirectory = "unit_tests_copy_to";
        private const string RemoteTestCopyFromDirectory = "unit_tests_copy_from";

        private IFileSystem? _sftpClient;

        [ClassInitialize]
        public static void SetUpClass(TestContext context)
        {
            IFileSystem _sftpClient = new SftpFileSystem(new TestSftpCredentials());
            Assert.IsTrue(_sftpClient.Connect());

            //_sftpClient.DeleteFilesAndFoldersInDirectory(RemoteTestCopyFromDirectory);

            

            string sourceFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SampleData/SFtpTestFile1.txt");
            FileInfo fi = new FileInfo(sourceFilePath);
            _sftpClient.UploadFiles($"{RemoteSftpDirectory}/{RemoteTestCopyTooDirectory}", fi);
        }

        [TestInitialize]
        public void SetUpEachTest()
        {
            IFileSystem sftpClient = new SftpFileSystem(new TestSftpCredentials());
            sftpClient.Connect();
            sftpClient!.DeleteFilesAndFoldersInDirectory($"{RemoteSftpDirectory}");
        }

        [TestMethod]
        public void MoveDirectory_MovesADirectoryFromCurrentPathToAnother_ShouldSucceed()
        {
            _sftpClient.MoveDirectory($"{RemoteTestCopyTooDirectory}",
                $"{RemoteTestCopyFromDirectory}");

        }

        [TestMethod]
        public void Connect_ConnectsToTheSFTPClient_ShouldSucceed()
        {
            Assert.IsTrue(_sftpClient.Connect());
        }

        [TestMethod]
        public void UploadFile_SFTPsSingleFileToConfiguredLocation_ShouldSucceed()
        {
            string sourceFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SampleData/SFtpTestFile1.txt");
            FileInfo fi = new FileInfo(sourceFilePath);

            int actual = 0;

            _sftpClient.UploadFiles(RemoteSftpDirectory, fi);

            int count = 0;

            List<string> files = _sftpClient.ListFilesAndFoldersInDirectory(RemoteSftpDirectory);

            Assert.AreEqual(31, actual);
            Assert.AreEqual(1, count);
            Assert.AreEqual(1, files.Count);
            Assert.AreEqual("SFtpTestFile1.txt", files[0]);
        }

        [TestMethod]
        public void UploadFiles_UploadToDirectoryThatDoesNotExist_ShouldCreateDirectoryAndSucceed()
        {
            IFileSystem sftpClient = new SftpFileSystem(new TestSftpCredentials());

            string sourceFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SampleData/SFtpTestFile1.txt");
            FileInfo fi = new FileInfo(sourceFilePath);
            
            sftpClient.UploadFiles(NewDirectory, fi);
            
            List<string> files = sftpClient.ListFilesAndFoldersInDirectory(NewDirectory);

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

            FileInfo[] fileInfos = new FileInfo[] { fiOne, fiTwo };

            _sftpClient.UploadFiles(RemoteSftpDirectory, fileInfos);

            List<string> files = _sftpClient.ListFilesAndFoldersInDirectory(RemoteSftpDirectory);

            Assert.AreEqual(2, files.Count);
            Assert.AreEqual("SFtpTestFile1.txt", files[0]);
            Assert.AreEqual("SFtpTestFile2.txt", files[1]);
        }
    }
}