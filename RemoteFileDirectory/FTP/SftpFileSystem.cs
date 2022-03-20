using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RemoteFileDirectory.Utils;
using Renci.SshNet;

namespace RemoteFileDirectory.FTP
{
    public class SftpFileSystem : IFileSystem
    {
        private readonly ISftpCredentials _credentials;
        private readonly SftpClient? _client;

        public SftpFileSystem(ISftpCredentials credentials)
        {
            _credentials = credentials;

            var connectionInfo = new ConnectionInfo(_credentials.RemoteUrl,
                _credentials.UserName,
                new PasswordAuthenticationMethod(_credentials.UserName, _credentials.Password),
                new PrivateKeyAuthenticationMethod(_credentials.AuthenticationMethod));

            _client = new SftpClient(connectionInfo);
        }

        public bool Connect()
        {
            if (!IsTrustedConnection()) return false;
            if (_client == null) return false;
            bool isConnected = _client.IsConnected;

            if (!isConnected)
            {
                _client.Connect();
            }

            return _client.IsConnected;
        }

        public void UploadFiles(string remoteDirectory, FileInfo fileInfo)
        {
            UploadFiles(remoteDirectory, new FileInfo[] {fileInfo});
        }

        public void UploadFiles(string remoteDirectory, FileInfo[] fileInfos)
        {
            if (!_client!.Exists(remoteDirectory))
            {
                _client.CreateDirectory(remoteDirectory);
            }

            foreach (FileInfo fileInfo in fileInfos)
            {
                using FileStream fs = fileInfo.OpenRead();
                using StreamReader sr = new StreamReader(fs);

                string remoteFilePath = $"/{remoteDirectory}/{fileInfo.Name}";

                _client.UploadFile(sr.BaseStream, remoteFilePath, true);
            }
        }

        public List<string> ListFilesAndFoldersInDirectory(string remoteDirectory)
        {
            var files = _client!.ListDirectory(remoteDirectory);

            return files.Select(file => file.Name).ToList();
        }

        public void DeleteFilesAndFoldersInDirectory(string remoteDirectory)
        {
            var files = _client!.ListDirectory(remoteDirectory);

            foreach (var file in files)
            {
                _client.DeleteFile($"{remoteDirectory}/{file.Name}");
            }
        }

        public void DeleteDirectory(string remoteDirectory)
        {
            _client!.DeleteDirectory(remoteDirectory);
        }

        public void MoveDirectory(string currentPath, string targetPath)
        {
            _client!.RenameFile(currentPath, targetPath);
        }

        private bool IsTrustedConnection()
        {
            byte[] expectedFingerPrint =
              new HexByteConverter().ConvertHexStringToByteArray(_credentials.ServerFingerprintHex);

            bool isTrusted = true;

            _client!.HostKeyReceived += (sender, e) =>
            {
                if (expectedFingerPrint.Length == e.FingerPrint.Length)
                {
                    if (expectedFingerPrint.Where((t, i) => t != e.FingerPrint[i]).Any())
                    {
                        isTrusted = false;
                    }
                }
                else
                {
                    isTrusted = false;
                }
            };
            

            return isTrusted;
        }
    }
}
