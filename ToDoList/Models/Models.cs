namespace ToDoList.Models
{
    public class ToDoItem 
    {
        public string id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
