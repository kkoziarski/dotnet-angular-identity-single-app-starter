namespace CleanArchWeb.Domain.Entities
{
    public interface IDocument : IDocument<string>
    {
    }

    public interface IDocument<TKey>
    {
        TKey Id { get; set; }
    }
}
