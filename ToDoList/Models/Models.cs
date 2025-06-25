namespace ToDoList.Models
{
    public class ToDoForCreate
    {
        public string Title { get; set; }
    }
    public class ToDoForUpdate : ToDoForCreate
    {
        public bool IsCompleted { get; set; }
    }  
    public class ToDoItem : ToDoForUpdate 
    {
        public string id { get; set; }       
        public DateTime CreatedAt { get; set; }
    }
    public class ToDoForDelete
    {
        public string id { get; set; }
    }
}
