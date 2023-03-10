using System.ComponentModel.DataAnnotations;

namespace Data.Entitites
{
    public abstract class BaseEntity
    {
        [Key]
        public long Id { get; set; }

        public long CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; } = DateTime.UtcNow;

        public long UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
