using Microsoft.EntityFrameworkCore;
using todolistapi.Data;
using todolistapi.Models;

namespace todolistapi.Services
{
    public class TodoItemService
    {
        private readonly AppDbContext _context;

        public TodoItemService(AppDbContext context)
        {
            _context = context;
        }


        // GET
        public async Task<List<TodoItem>> GetAllTodoItemsAsync()
        {
            return await _context.TodoItems.ToListAsync();
        }


        // GET BY ID
        public async Task<TodoItem?> GetTodoItemByIdAsync(int id)
        {
            return await _context.TodoItems.FirstOrDefaultAsync(x => x.Id == id);
        }


        // POST
        public async Task<TodoItem> AddTodoListItemAsync(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();
            return todoItem;
        }


        // DELETE
        public async Task<bool> DeleteTodoItemAsync(int id)
        {
            var todo = await _context.TodoItems.FindAsync(id);
            if (todo is null)
            {
                return false;
            }
            _context.TodoItems.Remove(todo);
            await _context.SaveChangesAsync();
            return true;
        }


        // PATCH
        public async Task<TodoItem?> UpdateIsDoneAsync(int id, bool isDone)
        {
            var todo = await _context.TodoItems.FindAsync(id);
            if (todo is null)
            {
                return null;
            }

            todo.IsDone = isDone;
            await _context.SaveChangesAsync();
            return todo;
        }
    }
}
