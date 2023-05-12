﻿using PhotoNotes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoNotes.TemplateSelectors
{
    class FileFolderItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate FileTemplate { get; set; }
        public DataTemplate FolderTemplate { get; set; }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {

            return item switch
            {
                FileItem => FileTemplate,
                FolderItem => FolderTemplate,
                _ => throw new Exception("Wrong item passed in")
            };


            
        }
    }
}
