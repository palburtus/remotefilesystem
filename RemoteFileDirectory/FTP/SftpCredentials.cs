using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Renci.SshNet;

namespace RemoteFileDirectory.FTP
{
    public class SftpCredentials : ISftpCredentials
    {
        public SftpCredentials(IConfiguration configuration)
        {
            RemoteUrl = configuration["App:RemoteUrl"];
            UserName = configuration["App:FtpUserName"];
            Password = configuration["App:FtpPassword"];
            AuthenticationMethod = configuration["App:FtpAuthMethod"];
            ServerFingerprintHex = configuration["App:FtpServerFingerprintHex"];
        }

        public string RemoteUrl { get; }

        public string UserName { get; }

        public string Password { get; }

        public string AuthenticationMethod { get; }
        
        public string ServerFingerprintHex { get; }
    }
}
