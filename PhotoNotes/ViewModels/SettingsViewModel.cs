using CommunityToolkit.Mvvm.ComponentModel;
using PhotoNotes.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoNotes.ViewModels
{
    public class SettingsViewModel : ObservableObject
    {
        
        private readonly IPreferences preferences;
        public SettingsViewModel()
        {
            preferences = Preferences.Default;
            
            FuzzyStringMatch = preferences.Get<bool>(PreferencesService.FuzzyStringMatchKey, true);
            FuzzyStringMatchThreshold = double.Parse(preferences.Get<string>(PreferencesService.FuzzyStringMatchThresholdKey, "90.0"));
            SaveToFolder = preferences.Get<bool>(PreferencesService.SaveToFolderKey, false);
        }

        private bool _saveToFolder;
        public bool SaveToFolder
        {
            get => _saveToFolder;
            set 
            {
                SetProperty(ref _saveToFolder, value);
                preferences.Set(PreferencesService.SaveToFolderKey, value);
            }
        }

        private bool _fuzzyStringMatch;
        public bool FuzzyStringMatch
        {
            get => _fuzzyStringMatch;
            set
            {
                SetProperty(ref _fuzzyStringMatch, value);
                preferences.Set(PreferencesService.FuzzyStringMatchKey, value);
            }
        }
        private double _fuzzyStringMatchThreshold;
        public double FuzzyStringMatchThreshold
        {
            get => _fuzzyStringMatchThreshold;
            set
            {
                SetProperty(ref _fuzzyStringMatchThreshold, value);
                preferences.Set(PreferencesService.FuzzyStringMatchThresholdKey, value.ToString());
            }
        }


    }
}
