using MauiTodoList.ViewModel;

namespace MauiTodoList;

public partial class DetailPage : ContentPage
{
    private DetailViewModel _ViewModel;

    public DetailPage(DetailViewModel vm)
    {
        InitializeComponent();
        _ViewModel = vm;
        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _ViewModel.ButtonVisible = _ViewModel.Id != 0;

        _ViewModel.GetTodoCommand.Execute(null);
    }
}