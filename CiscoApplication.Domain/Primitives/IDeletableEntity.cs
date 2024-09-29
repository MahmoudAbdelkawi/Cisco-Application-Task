namespace CiscoApplication.Domain.Primitives
{
    internal interface IDeletableEntity
    {
        public bool IsDeleted { get; set; }
    }
}
