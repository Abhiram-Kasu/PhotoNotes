﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PhotoNotes.Extensions;
using PhotoNotes.Models;
using PhotoNotes.Services;
using PhotoNotes.Views;
using System.Collections.ObjectModel;

namespace PhotoNotes.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {

        private readonly IPhotoManagement photoManagement;
        [ObservableProperty]
        private bool _isBusy;

        public bool HasFiles => Files.Any();

        public bool HasFolders => Folders.Any();

        public ObservableCollection<FileItem> Files { get; set; }
        public ObservableCollection<FolderItem> Folders { get; set; }
        private static readonly ObservableCollection<FolderItem> EmptyFolderCollection = new (Enumerable.Empty<FolderItem>());
        private static readonly ObservableCollection<FileItem> EmptyFileCollection = new(Enumerable.Empty<FileItem>());
        public MainPageViewModel(IPhotoManagement photoManagement)
        {

            this.photoManagement = photoManagement;
            (Folders, Files) = (photoManagement.MainFolder.Folders, photoManagement.MainFolder.Files);


            Files.CollectionChanged += (_, e) => OnPropertyChanged(nameof(HasFiles));
            Folders.CollectionChanged += (_, e) => OnPropertyChanged(nameof(HasFolders));




        }
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CurrTitle))]
        [NotifyPropertyChangedFor(nameof(ShowHomeButton))]
        private string? currFolder = null;

        public bool ShowHomeButton => CurrFolder is not null;

        public string CurrTitle => CurrFolder ?? "Files";

        [RelayCommand]
        public void BackToHome()
        {
            Folders = photoManagement.MainFolder.Folders;
            Files = photoManagement.MainFolder.Files;
            CurrFolder = null;
            OnPropertyChanged(nameof(Files));
            OnPropertyChanged(nameof(HasFolders));
            OnPropertyChanged(nameof(HasFiles));
        }

        [RelayCommand]
        public async Task DeleteFolderItem(string name)
        {
            var folder = Folders.First(x => x.CurrPath == name);
            if(folder.NumFiles > 0)
            {
                if(!await Shell.Current.DisplayAlert("Warning", $"Are you sure you want to delete the nested {folder.NumFiles} inside this folder?", "Yes", "No"))
                {
                    return;
                }
            }
            
            photoManagement.DeleteFolder(name);
            
        }
        [RelayCommand]
        public void DeleteFileItem(string name)
        {
            if (CurrFolder is null)
            {
                photoManagement.DeleteFile(null, name);

            }
            else
            {
                photoManagement.DeleteFile(CurrFolder, name);
            }
            //Files.RemoveAll(x => x.CurrPath == name);


        }

        [RelayCommand]
        public void SelectFolder(string name)
        {
            
            var folder = Folders.Single(x => x.CurrPath == name);
            CurrFolder = folder.ShortName;

            Files = folder.Files;
            Folders = EmptyFolderCollection;

            OnPropertyChanged(nameof(HasFolders));
            OnPropertyChanged(nameof(Files));

        }
        [RelayCommand]
        public async Task SelectFile(string name)
        {
            //TODO 
            await Shell.Current.GoToAsync($"secret/{nameof(PhotoView)}?{nameof(PhotoViewModel.PhotoSource)}={name}");
        }
        [RelayCommand]
        public void BackToMain()
        {
            CurrFolder = null;
            Folders = photoManagement.MainFolder.Folders;
            OnPropertyChanged(nameof(Folders));
            Files = photoManagement.MainFolder.Files;
            OnPropertyChanged(nameof(Files));

        }


    }
}
