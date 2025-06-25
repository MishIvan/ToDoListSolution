using ToDoList.Models;

namespace ToDoList.Services
{
    public interface IDBHelper
    {
        string errorMessage { get; }
        Task<List<ToDoItem>> GetItems();
        ToDoItem GetItem(string id);
        bool CreateItem(ToDoItem item);
        bool DeleteItem(string id);
        bool UpdateItem(string id, string title, bool completed);

    }
}
