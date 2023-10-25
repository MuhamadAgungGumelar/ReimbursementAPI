using ReimbursementAPI.Models;

namespace ReimbursementAPI.DTO.AccountRole
{
    public class NewAccountRolesDto
    {
        public Guid AccountGuid { get; set; } //deklarasi property
        public Guid RoleGuid { get; set; } //deklarasi property

        public static implicit operator AccountRoles(NewAccountRolesDto newAccountRolesDto) //implementasi implicit Operator
        {
            return new AccountRoles
            {
                AccountGuid = newAccountRolesDto.AccountGuid,
                RoleGuid = newAccountRolesDto.RoleGuid,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };
        }
    }
}
