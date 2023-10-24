using ReimbursementAPI.Utilities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReimbursementAPI.Models
{
    [Table("tb_m_reimbursement")]
    public class Reimbursements : BaseEntity
    {
        [Column("employee_guid")]
        public Guid EmployeeGuid { get; set; }
        [Column("name", TypeName = "nvarchar(100)")]
        public string Name { get; set; }
        [Column("description", TypeName = "nvarchar(100)")]
        public string Description { get; set; }
        [Column("value", TypeName = "int")]
        public int Value { get; set; }
        [Column("image_type", TypeName = "nvarchar(20)")]
        public string ImageType { get; set; }
        [Column("image", TypeName = "varbinary(max)")]
        public byte[] Image { get; set; }
        [Column("status", TypeName = "int")]
        public StatusLevel Status { get; set; }
    }
}
