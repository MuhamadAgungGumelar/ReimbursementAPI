using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReimbursementAPI.Models
{
    public abstract class BaseEntity
    {
        [Key, Column("guid")] //penamaan column pada orm
        public Guid Guid { get; set; } //property pada model
        [Column("created_date")] //penamaan column pada orm
        public DateTime CreatedDate { get; set; } //property pada model
        [Column("modified_date")] //penamaan column pada orm
        public DateTime ModifiedDate { get; set; } //property pada model
    }
}
