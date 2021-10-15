using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using MongoDB.Driver.Core.Events;

namespace CleanArchWeb.Infrastructure.Persistence.Management
{
    internal class MongoLogEvents : IEventSubscriber
    {
        private readonly ConcurrentDictionary<int, string> queriesBuffer = new();
        private readonly ReflectionEventSubscriber subscriber;

        private readonly ImmutableHashSet<string> notTrackedCommands
            = new[] { "isMaster", "buildInfo", "getLastError", "saslStart", "saslContinue" }
                .Select(v => v.ToLower())
                .ToImmutableHashSet();

        public MongoLogEvents()
        {
            this.subscriber = new ReflectionEventSubscriber(this);
        }

        public MongoLogEvents(List<string> ignoreCommands) : this()
        {
            this.notTrackedCommands = ignoreCommands.Select(v => v.ToLower()).ToImmutableHashSet();
        }

        public bool TryGetEventHandler<TEvent>(out Action<TEvent> handler)
            => this.subscriber.TryGetEventHandler(out handler);

        public void Handle(CommandStartedEvent e)
        {
            try
            {
                if (e.Command != null && !this.notTrackedCommands.Contains(e.CommandName.ToLower()))
                {
                    this.queriesBuffer.TryAdd(e.RequestId, e.Command.ToString());
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        public void Handle(CommandSucceededEvent e)
        {
            if (this.notTrackedCommands.Contains(e.CommandName.ToLower()))
            {
                return;
            }

            try
            {
                if (this.queriesBuffer.TryRemove(e.RequestId, out var query))
                {
                    OnCommandCompleted(new MongoCommandCompletedEventArgs(e.CommandName, query, true, e.Duration));
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        public void Handle(CommandFailedEvent e)
        {
            if (this.notTrackedCommands.Contains(e.CommandName.ToLower()))
            {
                return;
            }
            try
            {
                if (this.queriesBuffer.TryRemove(e.RequestId, out var query))
                {
                    OnCommandCompleted(new MongoCommandCompletedEventArgs(e.CommandName, query, false, e.Duration));
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private static void OnCommandCompleted(MongoCommandCompletedEventArgs args)
        {
            Console.WriteLine(args.ToString());
        }

        private class MongoCommandCompletedEventArgs : EventArgs
        {
            public MongoCommandCompletedEventArgs(string commandName, string query, bool success, TimeSpan duration)
            {
                this.CommandName = commandName;
                this.Query = query;
                this.Success = success;
                this.Duration = duration;
            }

            public string CommandName { get; }
            public string Query { get; }
            public bool Success { get; }
            public TimeSpan Duration { get; }

            public override string ToString()
            {
                var builder = new StringBuilder();

                builder.Append($"CommandName: {this.CommandName}, ");
                builder.Append($"Query: {this.Query}, ");
                builder.Append($"Success: {this.Success}, ");
                builder.Append($"Duration: {this.Duration}");

                return builder.ToString();
            }
        }
    }
}
