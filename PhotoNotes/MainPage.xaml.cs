using PhotoNotes.ViewModels;

namespace PhotoNotes;

public partial class MainPage : ContentPage
{
    int initial = 0;
    private readonly MainPageViewModel viewModel;
	public MainPage(MainPageViewModel m)
	{
        this.BindingContext = m;
        viewModel = m;
        InitializeComponent();
		
		
		
	}


}

