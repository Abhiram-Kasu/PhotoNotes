

using PhotoNotes.Extensions;
using PhotoNotes.Models;

namespace PhotoNotes.Services
{
    public class PhotoManagement : IPhotoManagement
    {



        public bool IsLoading { get; set; }

        public FolderItem MainFolder { get; set; } = new FolderItem
        {
            CurrPath = IPhotoManagement.HomePath,
            Name = "Folder",

        };

        


        /* Unmerged change from project 'PhotoNotes (net7.0-ios)'
        Before:
                public PhotoManagement() => InitFolders();


                private void InitFolders()
        After:
                public PhotoManagement() => InitFolders();


                private void InitFolders()
        */

        /* Unmerged change from project 'PhotoNotes (net7.0-maccatalyst)'
        Before:
                public PhotoManagement() => InitFolders();


                private void InitFolders()
        After:
                public PhotoManagement() => InitFolders();


                private void InitFolders()
        */

        /* Unmerged change from project 'PhotoNotes (net7.0-windows10.0.19041.0)'
        Before:
                public PhotoManagement() => InitFolders();


                private void InitFolders()
        After:
                public PhotoManagement() => InitFolders();


                private void InitFolders()
        */
        public PhotoManagement() => InitFolders();


        private void InitFolders()
        {
            IsLoading = true;

            var files = Directory.GetFiles(IPhotoManagement.HomePath, "*.png");
            MainFolder.Files.AddRange(files.Select(x => new FileItem
            {
                Name = x,
                CurrPath = Path.Combine(IPhotoManagement.HomePath, x)
            }));

            MainFolder.Folders.AddRange(Directory.GetDirectories(IPhotoManagement.HomePath).Select(x => new FolderItem
            {
                Name = x,
                CurrPath = Path.Combine(IPhotoManagement.HomePath, x)
            }));

            MainFolder.Folders.ForEach(x =>
            {
                x.Files.AddRange(Directory.GetFiles(x.CurrPath).Select(x => new FileItem
                {
                    Name = x,
                    CurrPath = Path.Combine(IPhotoManagement.HomePath, x)
                }));
            });

            IsLoading = false;



        }

        public (bool successfull, string? errMessage) CreateNewFolder(string name)
        {
            if (MainFolder.Folders.Any(x => x.Name == name))
            {
                return (false, "Folder already exists!");
            }
            MainFolder.Folders.Add(new FolderItem
            {
                Name = name,
                CurrPath = Path.Combine(IPhotoManagement.HomePath, name)
            });
            Directory.CreateDirectory(Path.Combine(IPhotoManagement.HomePath, name));

            MainFolder.Folders = new(MainFolder.Folders.OrderBy(x => x.Name));
            return (true, null);
        }

        public bool DeleteFolder(string folder)
        {
            var f = MainFolder.Folders.FirstOrDefault(x => x.Name == folder, null);

            if (f is null)
            {
                return false;
            }

            MainFolder.Folders.Remove(f);
            Directory.Delete(f.CurrPath, true);
            return true;
        }

        public bool DeleteFile(string? folder, string file)
        {
            if (folder is null)
            {
                File.Delete(Path.Combine(IPhotoManagement.HomePath, file));
                MainFolder.Files.Remove(MainFolder.Files.FirstOrDefault(x => x.CurrPath == file));
                return true;
            }
            var f = MainFolder.Folders.FirstOrDefault(x => x.ShortName == folder, null);
            if (f is null) { return false; }
            f.Files.Remove(f.Files.FirstOrDefault(x => x.CurrPath == file));
            File.Delete(file);
            return true;


        }
        public (bool successfull, string? errMessage) CreateNewFile(string name, string fromPath, string? folder = null)
        {
            if (folder is null)
            {
                if (MainFolder.Files.Any(x => x.Name == name)) { return (false, "File name already exists!"); };
                var topPath = Path.Combine(IPhotoManagement.HomePath, name + ".png");
                MainFolder.Files.Add(new FileItem
                {
                    Name = name,
                    CurrPath = topPath,
                });
                File.Move(fromPath, topPath);

                return (true, null);
            }
            var f = MainFolder.Folders.FirstOrDefault(x => x.Name == folder, null);
            if (f.Files.Any(x => x.Name == name))
            {
                return (false, "File name already exists!");
            }
            var path = Path.Combine(f.CurrPath, name);
            f.Files.Add(new FileItem
            {
                Name = name,
                CurrPath = path,
            });
            File.Move(fromPath, path);
            return (true, null);
        }
        public double TMPFolderSize => Directory.GetFiles(IPhotoManagement.TempPath).Sum(x => new FileInfo(x).Length);
        public void ClearTMPFolder()
        {
            Directory.GetFiles(IPhotoManagement.TempPath).ForEach(x => File.Delete(x));
            
        }
    }
    public interface IPhotoManagement
    {
        public static readonly string HomePath = Directory.CreateDirectory(Path.Combine(FileSystem.Current.AppDataDirectory, "Folders")).FullName;
        public static readonly string TempPath = Directory.CreateDirectory(Path.Combine(FileSystem.Current.AppDataDirectory, "TMP")).FullName;

        public abstract double TMPFolderSize { get; }
        public bool IsLoading { get; set; }
        public FolderItem MainFolder { get; set; }

        (bool successfull, string? errMessage) CreateNewFolder(string folderName);
        bool DeleteFolder(string folder);
        bool DeleteFile(string folder, string file);
        (bool successfull, string? errMessage) CreateNewFile(string name, string fromPath, string? folder = null);

        void ClearTMPFolder();

    }
}
