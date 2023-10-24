using System.ComponentModel.DataAnnotations.Schema;

namespace ReimbursementAPI.Models
{
    [Table("tb_m_accounts")]
    public class Accounts : BaseEntity
    {
        [Column("password", TypeName = "nvarchar(100)")]
        public string Password { get; set; }
        [Column("otp", TypeName = "int")]
        public int Otp { get; set; }
        [Column("is_used", TypeName = "int")]
        public bool IsUsed { get; set; }
        [Column("expired_time", TypeName = "datetime2")]
        public DateTime ExpiredTime { get; set; }

        //Cardinality
        public ICollection<AccountRoles>? AccountRoles { get; set; }
        public Employees? Employees { get; set; }
    }
}
