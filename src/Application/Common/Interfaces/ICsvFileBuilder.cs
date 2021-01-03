using CleanArchWeb.Application.TodoLists.Queries.ExportTodos;
using System.Collections.Generic;

namespace CleanArchWeb.Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
    }
}
