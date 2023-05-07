using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoNotes.Models
{
    
    public class FolderItem
    {
        public string CurrPath { get; set; }
        public string Name { get; set; }
        public List<FolderItem> Folders { get; set;} = new List<FolderItem>();

        public List<FileItem> Files { get; set; } = new List<FileItem>();
    }

    public class FileItem
    {

        private string _currPath;
        public string CurrPath { get; set; }
        public string Name { get; set; }


    }
}
