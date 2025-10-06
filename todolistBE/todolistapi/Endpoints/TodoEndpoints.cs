using todolistapi.Models;
using todolistapi.Services;

namespace todolistapi.Endpoints
{
    public static class TodoEndpoints
    {
        public static WebApplication MapTodoEndpoints(this WebApplication app)
        {
            // GET All
            app.MapGet("/api/todos", async (TodoItemService service) =>
            {
                var todos = await service.GetAllTodoItemsAsync();
                return Results.Ok(todos);
            });


            // POST
            app.MapPost("/api/todos", async (TodoItemService service, TodoItem todo) =>
            {
                if (string.IsNullOrEmpty(todo.Title))
                {
                    return Results.BadRequest("Title is required.");
                }

                await service.AddTodoListItemAsync(todo);
                return Results.Created($"/api/todos/{todo.Id}", todo);
            });


            // DELETE
            app.MapDelete("/api/todos/{id}", async (TodoItemService service, int id) =>
            {
                var todo = await service.GetTodoItemByIdAsync(id);

                if (todo == null)
                {
                    return Results.NotFound();
                }

                await service.DeleteTodoItemAsync(todo.Id);
                return Results.Ok(todo.Title + " deleted");
            });

            // PATCH: ändra om todo är klar eller inte
            app.MapPatch("/api/todos/{id:int}/done", async (TodoItemService service, int id, bool isDone) =>
            {
                var updated = await service.UpdateIsDoneAsync(id, isDone);

                if (updated is null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(updated);
            });

            return app;
        }  
    }
}
