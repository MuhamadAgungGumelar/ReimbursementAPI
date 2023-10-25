using System.ComponentModel.DataAnnotations.Schema;

namespace ReimbursementAPI.Models
{
    [Table("tb_m_roles")]
    public class Roles : BaseEntity
    {
        [Column("name", TypeName = "varchar(100)")]
        public string Name { get; set; }

        //Cardinality
        public ICollection<AccountRoles>? AccountRoles { get; set; }
    }
}
