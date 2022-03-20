using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Renci.SshNet;

namespace RemoteFileDirectory.FTP
{
    internal class SftpFileSystem : IFileSystem
    {
        private readonly ISftpCredentials _credentials;
        private SftpClient? _client;

        public SftpFileSystem(ISftpCredentials credentials)
        {
            _credentials = credentials;
        }

        public bool Connect()
        {
            var connectionInfo = new ConnectionInfo(_credentials.RemoteUrl,
                _credentials.UserName,
                new PasswordAuthenticationMethod(_credentials.UserName, _credentials.Password),
                new PrivateKeyAuthenticationMethod(_credentials.AuthenticationMethod));

            _client = new SftpClient(connectionInfo);

            if (!IsTrustedConnection())
            {
                return false;
            }
            
            _client.Connect();
            return true;
            
        }

        public void UploadFiles(string remoteDirectory, FileInfo fileInfo)
        {
            throw new NotImplementedException();
        }

        public void UploadFiles(string remoteDirectory, FileInfo[] fileInfos)
        {
            throw new NotImplementedException();
        }

        public List<string> ListFilesAndFoldersInDirectory(string remoteDirectory)
        {
            throw new NotImplementedException();
        }

        public void DeleteFilesAndFoldersInDirectory(string remoteDirectory)
        {
            throw new NotImplementedException();
        }

        public void DeleteDirectory(string remoteDirectory)
        {
            throw new NotImplementedException();
        }

        public void MoveDirectory(string currentPath, string targetPath)
        {
            throw new NotImplementedException();
        }

        private bool IsTrustedConnection()
        {
            //byte[] expectedFingerprintHex =
                //HexByteConverter.ConvertHexStringToByteArray(_credentials.ServerFingerprintHex);


            return true;
        }
    }
}
