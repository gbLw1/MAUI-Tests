using MauiTodoList.ViewModel;

namespace MauiTodoList;

public partial class ConnectionTestPage : ContentPage
{
    public ConnectionTestPage(ConnectionTestViewModel vm)
    {
        InitializeComponent();

        BindingContext = vm;
    }
}