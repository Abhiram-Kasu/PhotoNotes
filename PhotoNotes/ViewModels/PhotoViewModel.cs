namespace PhotoNotes.ViewModels
{
    [QueryProperty(nameof(PhotoSource), nameof(PhotoSource))]
    public class PhotoViewModel
    {
        public string PhotoSource { get; set; }
    }
}
