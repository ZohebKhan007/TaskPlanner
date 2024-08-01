using TaskPlanner.MVVM.ViewModels;

namespace TaskPlanner.MVVM.Views;

public partial class MainView : ContentPage
{
	private MainViewModels mainViewModels = new MainViewModels();
	public MainView()
	{
		InitializeComponent();
		BindingContext = mainViewModels;
	}

    private void checkBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
		mainViewModels.UpdateData();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
		var taskView = new NewTaskView
		{
			BindingContext = new NewTaskViewModel
			{
				Tasks = mainViewModels.Tasks,
				Categories = mainViewModels.Categories,
			}
		};

		Navigation.PushAsync(taskView);
    }
}