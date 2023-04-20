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
            Todos.Add(todo);
        }

    }

    [RelayCommand]
    async Task Add()
    {
        if (string.IsNullOrWhiteSpace(Text))
        {
            await Shell.Current.DisplayAlert("Ops", "Você precisa digitar algo para adicionar", "Ok");
            return;
        }

        await TodoService.AddTodo(new Todo
        {
            Description = Text
        });

        await GetTodoList();

        Text = string.Empty;
    }

    [RelayCommand]
    async Task Delete(Todo todoModel)
    {
        var result = await Shell.Current.DisplayAlert("Delete", $"Você tem certeza que deseja deletar \"{todoModel.Description}\"?", "Sim", "Não");

        if (!result)
        {
            return;
        }

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