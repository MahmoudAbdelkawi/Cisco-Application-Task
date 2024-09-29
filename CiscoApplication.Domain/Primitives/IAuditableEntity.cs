namespace CiscoApplication.Domain.Primitives
{
    public interface IAuditableEntity
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedOnUtc { get; set; }
    }
}
