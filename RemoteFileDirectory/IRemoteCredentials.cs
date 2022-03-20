using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteFileDirectory
{
    public interface IRemoteCredentials
    { 
        string RemoteUrl { get; }
        string UserName { get; }
        string Password { get; }
    }
}
