using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiTodoList.Models;
using MauiTodoList.Services;
using System.Collections.ObjectModel;

namespace MauiTodoList.ViewModel;

#region [+ Old code]

//public class MainViewModel : INotifyPropertyChanged
//{
//    private string text;
//    public string Text
//    {
//        get => text;
//        set
//        {
//            text = value;
//            OnPropertyChanged(nameof(Text));
//        }
//    }


//    public event PropertyChangedEventHandler PropertyChanged;
//    void OnPropertyChanged(string name)
//        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
//}

#endregion

public partial class MainViewModel : ObservableObject
{
    public MainViewModel()
    {
        Todos = new();
    }

    [ObservableProperty]
    ObservableCollection<Todo> todos;

    [ObservableProperty]
    private string text;

    [RelayCommand]
    public async Task GetTodoList()
    {
        var todos = await TodoService.GetTodo();

        if (!todos.Any())
            return;

        Todos.Clear();

        foreach (var todo in todos)
        {
            Todos.Add(new Todo
            {
                Id = todo.Id,
                Description = todo.Description.Length > 30 ? $"{todo.Description[..30]}..." : todo.Description,
            });
        }

    }

    [RelayCommand]
    async Task Add()
    {
        if (string.IsNullOrWhiteSpace(Text))
        {
            await Shell.Current.DisplayAlert("Ops", "Você precisa digitar algo para adicionar.", "Ok");
            return;
        }

        await TodoService.AddTodo(new Todo
        {
            Description = Text
        });

        await GetTodoList();

        var toast = Toast.Make(
            message: "Dica: arraste para o lado para excluir uma tarefa",
            duration: ToastDuration.Short);
        await toast.Show();

        Text = string.Empty;
    }

    [RelayCommand]
    async Task Delete(Todo todoModel)
    {
        await TodoService.DeleteTodo(todoModel.Id);

        var todo = Todos.FirstOrDefault(t => t.Id == todoModel.Id);

        Todos.Remove(todo);
    }

    [RelayCommand]
    async Task Tap(int id)
    {
        // Query parameters podem ser passados normalmente na rota
        await Shell.Current.GoToAsync($"{nameof(DetailPage)}?id={id}");
    }
}