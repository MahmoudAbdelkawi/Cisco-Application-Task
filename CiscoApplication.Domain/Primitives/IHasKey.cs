namespace CiscoApplication.Domain.Primitives
{
    public interface IHasKey<TKey>
    {
        public TKey Id { get; set; }
    }
}
