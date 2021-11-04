using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using CleanArchWeb.Application.Common.Models;
using CleanArchWeb.Application.TodoItems.Commands.CreateTodoItem;
using CleanArchWeb.Application.TodoItems.Commands.DeleteTodoItem;
using CleanArchWeb.Application.TodoItems.Commands.UpdateTodoItem;
using CleanArchWeb.Application.TodoItems.Commands.UpdateTodoItemDetail;
using CleanArchWeb.Application.TodoItems.Queries.GetTodoItemsWithPagination;
using CleanArchWeb.Application.TodoLists.Queries.GetTodos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchWeb.WebUI.Controllers
{
    [Authorize]
    public class TodoItemsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedList<TodoItemDto>>> GetTodoItemsWithPagination([FromQuery] GetTodoItemsWithPaginationQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(CreateTodoItemCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, UpdateTodoItemCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut("[action]/{listId}/{id}")]
        public async Task<ActionResult> UpdateItemDetails([NotNull] string listId, Guid id, UpdateTodoItemDetailCommand command)
        {
            if (id != command.Id || listId != command.ListId)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{listId}/{id}")]
        public async Task<ActionResult> Delete([NotNull] string listId, Guid id)
        {
            await Mediator.Send(new DeleteTodoItemCommand { Id = id, ListId = listId });

            return NoContent();
        }
    }
}