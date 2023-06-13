namespace PhotoNotes.Services
{
    public static class PreferencesService
    {
        public const string SaveToFolderKey = "SaveToFolder";
        public const string FuzzyStringMatchKey = "FuzzyStringMatch";
        public const string FuzzyStringMatchThresholdKey = "FuzzyStringMatchThreshold";

        public static void Init()
        {
            if (!Preferences.Default.ContainsKey(PreferencesService.SaveToFolderKey))
            {
                Preferences.Default.Set(PreferencesService.SaveToFolderKey, false);
            }
            if (!Preferences.Default.ContainsKey(PreferencesService.FuzzyStringMatchThresholdKey))
            {
                Preferences.Default.Set(PreferencesService.FuzzyStringMatchThresholdKey, "80.0");
            }
            if (!Preferences.Default.ContainsKey(PreferencesService.FuzzyStringMatchKey))
            {
                Preferences.Default.Set(PreferencesService.FuzzyStringMatchKey, true);
            }
        }
    }
}