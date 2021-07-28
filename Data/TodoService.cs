using System.Collections.Generic;
using System.Linq;

namespace BlazorApp.Data
{
    public class TodoService : ITodoService
    {
        public event System.Action RefreshRequested;
        
        public void CallRequestRefresh()
        {
            RefreshRequested?.Invoke();
        }
        
        private List<TodoItem> Items { get; set; }

        public TodoService()
        {
            Items = new List<TodoItem>() { new TodoItem() {Id = 0, Description = "first todo", Done = false}};
            
        }
        public List<TodoItem> GetTodos()
        {
            return Items;
        }

        public void SetDone(TodoItem todo, bool done)
        {
            SetDone(todo.Id,done);
        }

        public void SetDone(int id, bool done)
        {
            Items = Items.Select(x =>
            {
                if (x.Id == id)
                {
                    x.Done = done;
                }

                return x;
            }).ToList();
            CallRequestRefresh();
        }

        public void Add(TodoItem todo)
        {
            Items.Add(todo);
            CallRequestRefresh();
        }

        public void Add(string description)
        {
            var id = Items.Select(x => x.Id).Max() + 1;
            var todo = new TodoItem() { Id = id, Description = description, Done = false };
            Add(todo);
        }
    }

    public interface ITodoService
    {
        event System.Action RefreshRequested;
        
       
        
        List<TodoItem> GetTodos();

        void SetDone(TodoItem todo, bool done);

        void SetDone(int id, bool done);

        void Add(TodoItem todo);

        void Add(string description);

    }
}