using SQLite;

namespace MauiTodoList.Models;

public class Todo
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Description { get; set; }
}