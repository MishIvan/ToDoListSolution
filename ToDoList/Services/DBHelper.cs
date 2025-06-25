using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using ToDoList.Models;

namespace ToDoList.Services
{
    public class DBHelper : IDBHelper
    {
        public List<ToDoItem> GetItems()
        {
            List<ToDoItem> lst = null;
            using (ApplicationContext db = new ApplicationContext())
            {
                lst = db.ToDoItems.ToList();
            }
            return lst; 
        }
        public ToDoItem GetItem(string id)
        {
            ToDoItem item = null;
            using (ApplicationContext db = new ApplicationContext())
            {
                item = db.ToDoItems.Where(el => el.id == id).FirstOrDefault();
            }

            return item;
        }
        public bool CreateItem(ToDoItem item)
        {
            EntityEntry<ToDoItem> result = null;
            using (ApplicationContext db = new ApplicationContext())
            {
                result = db.ToDoItems.Add(item);
                if(result != null) 
                    db.SaveChanges();
            }
            return result != null;

        }
        public bool DeleteItem(string id)
        {
            EntityEntry<ToDoItem> result = null;
            using (ApplicationContext db = new ApplicationContext())
            {
                ToDoItem item = db.ToDoItems.Where(el => id == el.id).FirstOrDefault();
                if (item != null)
                {
                    result = db.ToDoItems.Remove(item);
                    if (result != null)
                        db.SaveChanges();
                }
            }
            return result != null;
        }
        public bool UpdateItem(string id, string title, bool completed)
        {
            EntityEntry<ToDoItem> result = null;
            using (ApplicationContext db = new ApplicationContext())
            {
                ToDoItem _item = db.ToDoItems.Where(el => id == el.id).FirstOrDefault();
                if (_item != null)
                {
                    _item.IsCompleted = completed;
                    _item.Title = title;
                    result = db.ToDoItems.Update(_item);
                    if (result != null)
                        db.SaveChanges();
                }
            }
            return result != null;

        }

    }
}
