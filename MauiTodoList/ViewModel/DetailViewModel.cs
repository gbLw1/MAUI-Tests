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

    [ObservableProperty]
    bool buttonVisible;

    [RelayCommand]
    public async Task GetTodo()
    {
        var todo = await TodoService.GetTodoById(Id);

        if (todo is null)
        {
            await Shell.Current.DisplayAlert("Error", "Not found", "Ok");

            Description = "¯\\_(ツ)_/¯";
            return;
        }

        Description = todo.Description;
    }

    [RelayCommand]
    async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }
}