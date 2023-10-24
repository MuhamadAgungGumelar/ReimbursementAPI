using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace ReimbursementAPI.Models
{
    [Table("tb_m_account_roles")]
    public class AccountRoles : BaseEntity
    {
        [Column("account_guid")]
        public Guid AccountGuid { get; set; }
        [Column("role_guid")]
        public Guid RoleGuid { get; set; }

        //cardinality
        public Roles? Role { get; set; }
        public Accounts? Accounts { get; set; }
    }
}
