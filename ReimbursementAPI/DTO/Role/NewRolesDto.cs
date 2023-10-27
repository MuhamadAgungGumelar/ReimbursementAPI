using ReimbursementAPI.Models;

namespace ReimbursementAPI.DTO.Role
{
    public class NewRolesDto
    {
        public string Name { get; set; } //deklarasi property

        public static implicit operator Roles(NewRolesDto newRoleDto) //implementasi implicit Operator
        {
            return new Roles
            {
                Name = newRoleDto.Name,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };
        }
    }
}
