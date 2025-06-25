using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using ToDoList.Models;

namespace ToDoList.Services
{
    public class DBHelper : IDBHelper
    {
        private string m_errorMessage;
        public string errorMessage {  get { return m_errorMessage; } }
        public async Task<List<ToDoItem>> GetItems()
        {
            List<ToDoItem> lst = null;
            using (ApplicationContext db = new ApplicationContext())
            {
                lst = await db.ToDoItems.ToListAsync();
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
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.ToDoItems.Add(item);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex) 
            {
                m_errorMessage = ex.Message;
                return false;
            }

        }
        public bool DeleteItem(string id)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    ToDoItem item = db.ToDoItems.Where(el => id == el.id).FirstOrDefault();
                    if (item != null)
                    {
                        db.ToDoItems.Remove(item);
                        db.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                m_errorMessage = ex.Message;
                return false;
            }
        }
        public bool UpdateItem(string id, string title, bool completed)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    ToDoItem _item = db.ToDoItems.Where(el => id == el.id).FirstOrDefault();
                    if (_item != null)
                    {
                        _item.IsCompleted = completed;
                        _item.Title = title;
                        db.ToDoItems.Update(_item);
                        db.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                m_errorMessage = ex.Message;
                return false;
            }

        }

    }
}
