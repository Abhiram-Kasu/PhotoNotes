

using PhotoNotes.Extensions;
using PhotoNotes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            
            MainFolder.Folders = new (MainFolder.Folders.OrderBy(x => x.Name));
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
            Directory.Delete(f.CurrPath);
            return true;
        }

        public bool DeleteFile(string? folder, string file)
        {
            if (folder is null)
            {
                File.Delete(Path.Combine(IPhotoManagement.HomePath, file));
                MainFolder.Files.RemoveAll(x => x.Name == file);
                return true;
            }
            var f = MainFolder.Folders.FirstOrDefault(x => x.Name == folder, null);
            if (f is null) { return false; }
            f.Files.RemoveAll(x => x.Name == folder);
            Directory.Delete(Path.Combine(f.CurrPath, file));
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

    }
    public interface IPhotoManagement
    {
        public static readonly string HomePath = Directory.CreateDirectory(Path.Combine(FileSystem.Current.AppDataDirectory, "Folders")).FullName;
        public static readonly string TempPath = Directory.CreateDirectory(Path.Combine(FileSystem.Current.AppDataDirectory, "TMP")).FullName;


        public bool IsLoading { get; set; }
        public FolderItem MainFolder { get; set; }

        (bool successfull, string? errMessage) CreateNewFolder(string folderName);
        bool DeleteFolder(string folder);
        bool DeleteFile(string folder, string file);
        (bool successfull, string? errMessage) CreateNewFile(string name, string fromPath, string? folder = null);


    }
}
