using System;

namespace CleanArchWeb.Domain.Entities
{
    public interface IDocument : IDocument<Guid>
    {
    }

    public interface IDocument<TKey>
    {
        TKey Id { get; set; }
    }
}
