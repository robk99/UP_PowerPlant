namespace Domain.Common
{
    public abstract class AuditDetails
    {
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
