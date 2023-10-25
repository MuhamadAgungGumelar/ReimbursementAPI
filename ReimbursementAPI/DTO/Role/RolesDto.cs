using ReimbursementAPI.Models;

namespace ReimbursementAPI.DTO.Role
{
    public class RolesDto : GeneralDto
    {
        public string Name { get; set; } //deklarasi property

        public static explicit operator RolesDto(Roles role) //implementasi explicit Operator
        {
            return new RolesDto
            {
                Guid = role.Guid,
                Name = role.Name
            };
        }

        public static implicit operator Roles(RolesDto roleDto) //implementasi implicit Operator
        {
            return new Roles
            {
                Guid = roleDto.Guid,
                Name = roleDto.Name,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
