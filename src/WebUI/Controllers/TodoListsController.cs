﻿using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using CleanArchWeb.Application.TodoLists.Commands.CreateTodoList;
using CleanArchWeb.Application.TodoLists.Commands.DeleteTodoList;
using CleanArchWeb.Application.TodoLists.Commands.UpdateTodoList;
using CleanArchWeb.Application.TodoLists.Queries.ExportTodos;
using CleanArchWeb.Application.TodoLists.Queries.GetTodos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchWeb.WebUI.Controllers
{
    [Authorize]
    public class TodoListsController : ApiControllerBase
    {
        [HttpGet("public/list")]
        [AllowAnonymous]
        public async Task<ActionResult<TodosVm>> GetPublic()
        {
            return await Mediator.Send(new GetTodosQuery());
        }

        [HttpGet]
        public async Task<ActionResult<TodosVm>> Get()
        {
            return await Mediator.Send(new GetTodosQuery());
        }



        [HttpGet("{id}")]
        public async Task<FileResult> Get([NotNull] string id)
        {
            var vm = await Mediator.Send(new ExportTodosQuery { ListId = id });

            return File(vm.Content, vm.ContentType, vm.FileName);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Create(CreateTodoListCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([NotNull] string id, UpdateTodoListCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([NotNull] string id)
        {
            await Mediator.Send(new DeleteTodoListCommand { Id = id });

            return NoContent();
        }
    }
}