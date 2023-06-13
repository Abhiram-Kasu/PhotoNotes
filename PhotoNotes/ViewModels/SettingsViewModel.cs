using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PhotoNotes.Services;

namespace PhotoNotes.ViewModels
{
    public partial class SettingsViewModel : ObservableObject
    {
        private readonly IPhotoManagement photoManagement;
        private readonly IPreferences preferences;
        private bool _fuzzyStringMatch;

        private double _fuzzyStringMatchThreshold;

        private bool _saveToFolder;

        public SettingsViewModel(IPhotoManagement photoManagement)
        {
            preferences = Preferences.Default;

            FuzzyStringMatch = preferences.Get<bool>(PreferencesService.FuzzyStringMatchKey, true);
            FuzzyStringMatchThreshold = double.Parse(preferences.Get<string>(PreferencesService.FuzzyStringMatchThresholdKey, "90.0"));
            SaveToFolder = preferences.Get<bool>(PreferencesService.SaveToFolderKey, false);
            this.photoManagement = photoManagement;
        }

        public bool CanClearTMPFolder => photoManagement.TMPFolderSize > 0;

        public Color ClearTMPButtonBackgroundColor => CanClearTMPFolder ? Colors.Red : Colors.Gray;

        public string ClearTMPFolderText => $"Clear {Math.Round(photoManagement.TMPFolderSize / 1e+6, 2)} mb from TMP folder";

        public bool FuzzyStringMatch
        {
            get => _fuzzyStringMatch;
            set
            {
                SetProperty(ref _fuzzyStringMatch, value);
                preferences.Set(PreferencesService.FuzzyStringMatchKey, value);
            }
        }

        public double FuzzyStringMatchThreshold
        {
            get => _fuzzyStringMatchThreshold;
            set
            {
                SetProperty(ref _fuzzyStringMatchThreshold, value);
                preferences.Set(PreferencesService.FuzzyStringMatchThresholdKey, value.ToString());
            }
        }

        public bool SaveToFolder
        {
            get => _saveToFolder;
            set
            {
                SetProperty(ref _saveToFolder, value);
                preferences.Set(PreferencesService.SaveToFolderKey, value);
            }
        }

        [RelayCommand]
        public void ClearTMP()
        {
            photoManagement.ClearTMPFolder();
            OnPropertyChanged(nameof(ClearTMPFolderText));
            OnPropertyChanged(nameof(CanClearTMPFolder));
            OnPropertyChanged(nameof(ClearTMPButtonBackgroundColor));
        }
    }
}