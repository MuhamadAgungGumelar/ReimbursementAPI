using ReimbursementAPI.Utilities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReimbursementAPI.Models
{
    [Table("tb_m_employees")] //penamaan tabel pada orm
    public class Employees : BaseEntity
    {
        [Column("first_name", TypeName = "nvarchar(100)")] //penamaan column pada orm
        public string FirstName { get; set; } //property pada model
        [Column("last_name", TypeName = "nvarchar(100)")] //penamaan column pada orm
        public string? LastName { get; set; } //property pada model
        [Column("birth_date", TypeName = "datetime2")] //penamaan column pada orm
        public DateTime BirthDate { get; set; } //property pada model
        [Column("gender", TypeName = "int")] //penamaan column pada orm
        public GenderLevel Gender { get; set; } //property pada model
        [Column("hiring_date", TypeName = "datetime2")] //penamaan column pada orm
        public DateTime HiringDate { get; set; } //property pada model
        [Column("email", TypeName = "nvarchar(100)")] //penamaan column pada orm
        public string Email { get; set; } //property pada model
        [Column("phone_number", TypeName = "nvarchar(20)")] //penamaan column pada orm
        public string PhoneNumber { get; set; } //property pada model
        [Column("manager_guid")]
        public Guid ManagerGuid { get; set; }

        //Cardinality
        public ICollection<Reimbursements>? Reimbursements { get; set; }
        public ICollection<Finances>? Finances { get; set; }
        public Accounts? Accounts { get; set; }

    }
}
