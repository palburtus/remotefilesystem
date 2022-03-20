namespace RemoteFileDirectory
{
    public interface IFileSystem
    {
        bool Connect();
        void UploadFiles(string remoteDirectory, FileInfo fileInfo);
        void UploadFiles(string remoteDirectory, FileInfo[] fileInfos);
        List<string> ListFilesAndFoldersInDirectory(string remoteDirectory);
        void DeleteFilesAndFoldersInDirectory(string remoteDirectory);
        void DeleteDirectory(string remoteDirectory);
        void MoveDirectory(string currentPath, string targetPath);
    }
}