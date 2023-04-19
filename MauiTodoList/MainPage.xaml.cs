using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
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

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var toast = Toast.Make(
            message: "Dica: Arraste para o lado para excluir uma tarefa",
            duration: ToastDuration.Long);
        await toast.Show();

        _ViewModel.GetTodoListCommand.Execute(null);
    }
}