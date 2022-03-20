# Remote File System
Simple Wrapper for SFTP File System Operations Using [Renci.SSH](https://github.com/sshnet/SSH.NET)

**App Configuration**
You will need the following values configured in your _appsettings.json_ file 
or else any other means of populating the _IConfiguration_ from the _Microsoft.Extensions.Configuration_ package

Example _appsettings.json_ configuration: 

```
{
  "App": {
    "RemoteUrl":  "yourservername.com",
    "FtpUserName": "yourftpusername",
    "FtpPassword": "yourftppassword",
    "FtpAuthMethod": "rsa.key",
    "FtpServerFingerpringHex": "61a02bc07ddaa3c58db756bc5e43e75f"
  }
}

```

**Running Integration Tests**

In order to run unit/integration tests included in this project you'll need an FTP server with SFTP support.

Your FTP account will have to have read/write/delete permissions

You will also have to change the values in TestFtpCredtianls in the _RemoteFileDirectory.FTP.Tests_ package of the _RemoteFileDirectoryTests_ project.  
It is the same paramaters you configured under _App_ in _application.json_ 

Example _TestSftpCredentials_:

```
public class TestSftpCredentials : ISftpCredentials
{
    public string RemoteUrlAuthenticationMethod => "yourservername.com";

    public string UserName => "ftp_username";

    public string Password => "ftp_password";
    
    public string ServerFingerprintHex => "61a02bc07ddaa3c58db756bc5e43e75f";

    public string AuthenticationMethod => "rsa.key";
}
```
