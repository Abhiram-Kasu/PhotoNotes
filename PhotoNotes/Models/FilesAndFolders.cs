using System.Collections.ObjectModel;

namespace PhotoNotes.Models
{

    public class FolderItem
    {
        public string CurrPath { get; set; }
        public string Name { get; set; }
        public int NumFiles => Files.Count;
        public string ShortName => Path.GetFileName(Name);
        public ObservableCollection<FolderItem> Folders { get; set; } = new();

        public ObservableCollection<FileItem> Files { get; set; } = new();
    }

    public class FileItem
    {

        private string _currPath;
        public string CurrPath { get; set; }
        public string Name { get; set; }

        public string ShortName => Path.GetFileNameWithoutExtension(Name);


    }
}
