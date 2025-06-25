using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using ToDoList.Services;
using Microsoft.Extensions.Logging;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDosController : Controller
    {
        private IDBHelper m_dbHelper;
        private ILogger<ToDosController> m_logger;
        public ToDosController(IDBHelper helper, ILogger<ToDosController> logger)
        {
            m_dbHelper = helper;
            m_logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetToDos()
        {
            List<ToDoItem> list = await m_dbHelper.GetItems();
            if (list == null)
            {
                m_logger.LogError("Не удалось получить список TODO");
                return NotFound("Не удалось получить список TODO");
            }
            else
                return Ok(list);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetToToById(string id)
        {
            ToDoItem res = await Task<ToDoItem>.Run(() => { return m_dbHelper.GetItem(id); });
            if (res != null)
                return Ok(res);
            else
            {
                m_logger.LogError($"Не найдена запись TODO с ИД = {id}");
                return NotFound($"Запись TODO с ИД = {id} не найдена");
            }

        }
        [HttpPost]
        public async Task<IActionResult> CreateItem(ToDoItem message)
        {

            bool res = await Task<bool>.Run(() => {
                message.CreatedAt = DateTime.Now;
                message.id = Guid.NewGuid().ToString();
                message.IsCompleted = false;
                return m_dbHelper.CreateItem(message); 
            });
            if (res)
                return Ok($"Добавлена запись с ИД = {message.id}");
            else
            {
                m_logger.LogError($"Не удалось добавить запись TODO с заголовком {message.Title} и ИД = {message.id}: {m_dbHelper.errorMessage}");
                return NotFound($"Запись TODO с заголовком {message.Title} и ИД = {message.id} не добавлена");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(string id, ToDoItem message)
        { 

            bool res = await Task<bool>.Run(() => { return m_dbHelper.UpdateItem(id, message.Title, message.IsCompleted); });
            if (res)
                return Ok($"Изменена запись с ИД = {id}");
            else
            {
                m_logger.LogError($"Не удалось изменить запись TODO с ИД = {id}");
                return NotFound($"Запись TODO с ИД = {id} не изменена: {m_dbHelper.errorMessage}");
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(string id)
        {
            bool res = await Task<bool>.Run(() => { return m_dbHelper.DeleteItem(id); });
            if (res)
                return Ok($"Запись с ИД = {id} удалена");
            else
            {
                m_logger.LogError($"Не удалось запись TODO с ИД = {id}");
                return NotFound($"Запись TODO с ИД = {id} не удалена: {m_dbHelper.errorMessage}");
            }

        }
    }
}
