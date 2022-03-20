# Remote File System
Simple Wrapper for SFTP File System Operations Using [Renci.SSH](https://github.com/sshnet/SSH.NET)

**Running Integration Tests**
In order to run unit/integration tests included in this project you'll need an FTP server with SFTP support.

You FTP Server will have to have certain files (see below) all with read/write/delete permissions for the tests to pass

You will also have to change the values in TestFtpCredtianls in the _RemoteFileDirectory.FTP.Tests_ package of the _RemoteFileDirectoryTests_ project.  
Below is an example:

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

You also have to make sure the folders for the following constants exist and you FTP Server

```
private const string RemoteSftpDirectory = "unit_test_base_directory";
private const string CopyToDirectoryName = "unit_test_copy_to";
```
