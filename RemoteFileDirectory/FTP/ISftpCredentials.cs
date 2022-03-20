using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteFileDirectory.FTP
{
    public interface ISftpCredentials : IRemoteCredentials
    {
        string AuthenticationMethod { get; }
        string ServerFingerprintHex { get; }
    }
}
