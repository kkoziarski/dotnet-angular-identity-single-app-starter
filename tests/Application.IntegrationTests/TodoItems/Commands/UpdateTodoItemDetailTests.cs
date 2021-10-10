using System;
using System.Threading.Tasks;
using CleanArchWeb.Application.Common.Exceptions;
using CleanArchWeb.Application.TodoItems.Commands.CreateTodoItem;
using CleanArchWeb.Application.TodoItems.Commands.UpdateTodoItem;
using CleanArchWeb.Application.TodoItems.Commands.UpdateTodoItemDetail;
using CleanArchWeb.Application.TodoLists.Commands.CreateTodoList;
using CleanArchWeb.Domain.Entities;
using CleanArchWeb.Domain.Enums;
using FluentAssertions;
using NUnit.Framework;

namespace CleanArchWeb.Application.IntegrationTests.TodoItems.Commands
{
    using static Testing;

    public class UpdateTodoItemDetailTests : TestBase
    {
        [Test]
        public void ShouldRequireValidTodoItemId()
        {
            var command = new UpdateTodoItemCommand
            {
                Id = Guid.NewGuid(),
                Title = "New Title"
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldUpdateTodoItem()
        {
            var userId = await RunAsDefaultUserAsync();

            var listId = await SendAsync(new CreateTodoListCommand
            {
                Title = "New List"
            });

            var itemId = await SendAsync(new CreateTodoItemCommand
            {
                ListId = listId,
                Title = "New Item"
            });

            var command = new UpdateTodoItemDetailCommand
            {
                Id = itemId,
                ListId = listId,
                Note = "This is the note.",
                Priority = PriorityLevel.High
            };

            await SendAsync(command);

            var item = await FindAsync<TodoItem>(itemId);

            item.Note.Should().Be(command.Note);
            item.Priority.Should().Be(command.Priority);
            item.LastModifiedBy.Should().NotBeNull();
            item.LastModifiedBy.Should().Be(userId.ToString());
            item.LastModified.Should().NotBeNull();
            item.LastModified.Should().BeCloseTo(DateTime.Now, 10000);
        }
    }
}
