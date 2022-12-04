using System.ComponentModel.DataAnnotations;

namespace MRA.Domain.Common
{
    public abstract class BaseEntity<TKey> : AuditableEntity, IHasKey<TKey>
    {
        [Key]
        public TKey Id { get; set; }
    }
}
