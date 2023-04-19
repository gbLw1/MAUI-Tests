using MauiTodoList.Models;
using SQLite;

namespace MauiTodoList.Services;

public static class TodoService
{
    static SQLiteAsyncConnection db;

    static async Task Init()
    {
        if (db != null)
        {
            return;
        }

        var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Todos.db3");

        db = new SQLiteAsyncConnection(databasePath);

        await db.CreateTableAsync<Todo>();
    }

    public static async Task AddTodo(Todo todo)
    {
        await Init();

        var todoEntity = new Todo
        {
            Description = todo.Description
        };

        await db.InsertAsync(todoEntity);
    }

    public static async Task DeleteTodo(int id)
    {
        await Init();

        await db.DeleteAsync<Todo>(id);
    }

    public static async Task<IEnumerable<Todo>> GetTodo()
    {
        await Init();

        var todos = await db.Table<Todo>().ToListAsync();

        return todos;
    }

    public static async Task<Todo> GetTodoById(int id)
    {
        await Init();

        var todo = await db.Table<Todo>()
            .Where(t => t.Id == id)
            .FirstOrDefaultAsync();

        return todo;
    }
}