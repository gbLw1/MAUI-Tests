using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MauiTodoList.ViewModel;

public partial class ConnectionTestViewModel : ObservableObject
{
    IConnectivity connectivity;

    public ConnectionTestViewModel(IConnectivity connectivity)
    {
        this.connectivity = connectivity;
    }

    [RelayCommand]
    async Task TestarConexao()
    {
        if (connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            await Shell.Current.DisplayAlert("Teste de conexão", "Você está desconectado! :(", "OK");
        }
        else
        {
            await Shell.Current.DisplayAlert("Teste de conexão", "Você está conectado! :)", "OK");
        }
    }
}