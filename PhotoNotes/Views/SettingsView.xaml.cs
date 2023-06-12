using PhotoNotes.ViewModels;

namespace PhotoNotes.Views;

public partial class SettingsView : ContentPage
{
	private readonly SettingsViewModel viewModel;
	public SettingsView(SettingsViewModel vm)
	{
		this.BindingContext = viewModel = vm;
		InitializeComponent();
	}
}