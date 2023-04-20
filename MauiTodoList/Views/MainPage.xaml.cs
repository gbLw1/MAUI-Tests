using MauiTodoList.ViewModel;

namespace MauiTodoList;

public partial class MainPage : ContentPage
{
    #region [+ No DI example]

    //public MainPage()
    //{
    //    InitializeComponent();
    //    BindingContext = new MainViewModel();
    //}

    #endregion

    private readonly MainViewModel _ViewModel;

    public MainPage(MainViewModel vm)
    {
        InitializeComponent();
        _ViewModel = vm;
        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _ViewModel.GetTodoListCommand.Execute(null);
    }
}