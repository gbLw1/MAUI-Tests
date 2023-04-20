using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiTodoList.Services;

namespace MauiTodoList.ViewModel;

[QueryProperty(nameof(Id), "id")]
public partial class DetailViewModel : ObservableObject
{
    [ObservableProperty]
    int id;

    [ObservableProperty]
    string description;

    [RelayCommand]
    public async Task GetTodo()
    {
        var todo = await TodoService.GetTodoById(Id);

        if (todo is null)
        {
            await Shell.Current.DisplayAlert("Ops", "Ocorreu um erro e não foi possível encontrar a anotação", "OK");

            Description = "¯\\_(ツ)_/¯";
            return;
        }

        Description = todo.Description;
    }

    [RelayCommand]
    async Task Voltar()
    {
        await Shell.Current.GoToAsync("..");
    }
}