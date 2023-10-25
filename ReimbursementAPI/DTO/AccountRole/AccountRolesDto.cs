using ReimbursementAPI.Models;

namespace ReimbursementAPI.DTO.AccountRole
{
    public class AccountRolesDto : GeneralDto
    {
        public Guid AccountGuid { get; set; } //deklarasi property
        public Guid RoleGuid { get; set; } //deklarasi property

        public static explicit operator AccountRolesDto(AccountRoles accountRole) //implementasi explicit Operator
        {
            return new AccountRolesDto
            {
                Guid = accountRole.Guid,
                AccountGuid = accountRole.AccountGuid,
                RoleGuid = accountRole.RoleGuid
            };
        }

        public static implicit operator AccountRoles(AccountRolesDto accountRolesDto) //implementasi implicit Operator
        {
            return new AccountRoles
            {
                Guid = accountRolesDto.Guid,
                AccountGuid = accountRolesDto.AccountGuid,
                RoleGuid = accountRolesDto.RoleGuid,
                ModifiedDate = DateTime.Now,
            };
        }
    }
}
