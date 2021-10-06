using CleanArchWeb.Application.Common.Exceptions;
using CleanArchWeb.Application.TodoItems.Commands.CreateTodoItem;
using CleanArchWeb.Application.TodoItems.Commands.UpdateTodoItem;
using CleanArchWeb.Application.TodoLists.Commands.CreateTodoList;
using CleanArchWeb.Domain.Entities;
using FluentAssertions;
using System.Threading.Tasks;
using NUnit.Framework;
using System;

namespace CleanArchWeb.Application.IntegrationTests.TodoItems.Commands
{
    using static Testing;

    public class UpdateTodoItemTests : TestBase
    {
        [Test]
        public void ShouldRequireValidTodoItemId()
        {
            var command = new UpdateTodoItemCommand
            {
                Id = Guid.Parse("fe5f3ea8-d470-4d80-9fdf-4895ea41d503"),
                ListId = Guid.Parse("c4c59f7d-3a5b-4982-84a0-75046712a179"),
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

            var command = new UpdateTodoItemCommand
            {
                Id = itemId,
                ListId = listId,
                Title = "Updated Item Title"
            };

            await SendAsync(command);

            var item = await FindAsync<TodoItem>(itemId);

            item.Title.Should().Be(command.Title);
            item.LastModifiedBy.Should().NotBeNull();
            item.LastModifiedBy.Should().Be(userId.ToString());
            item.LastModified.Should().NotBeNull();
            item.LastModified.Should().BeCloseTo(DateTime.Now, 1000);
        }
    }
}