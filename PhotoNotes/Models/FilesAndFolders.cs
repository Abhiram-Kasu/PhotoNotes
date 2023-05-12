using PhotoNotes.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoNotes.Models
{
    
    public class FolderItem
    {
        public string CurrPath { get; set; }
        public string Name { get; set; }
        public int NumFiles => Files.Count;
        public string ShortName => Name.Split(@"\").Last();
        public ObservableCollection<FolderItem> Folders { get; set;} = new();

        public ObservableCollection<FileItem> Files { get; set; } = new ();
    }

    public class FileItem
    {

        private string _currPath;
        public string CurrPath { get; set; }
        public string Name { get; set; }

        public string ShortName => Name.Split(@"\").Last();


    }
}
