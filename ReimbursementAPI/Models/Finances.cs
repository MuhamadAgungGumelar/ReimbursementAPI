using System.ComponentModel.DataAnnotations.Schema;

namespace ReimbursementAPI.Models
{
    [Table("tb_m_finances")]
    public class Finances : BaseEntity
    {
        [Column("employee_guid")]
        public Guid EmployeeGuid { get; set; }
        [Column("name", TypeName = "nvarchar(100)")]
        public string Name { get; set; }
        [Column("approved_date", TypeName = "datetime2")]
        public DateTime ApprovedDate { get; set; }
        [Column("approved_amount", TypeName = "int")]
        public int ApprovedAmount { get; set; }
    }
}
