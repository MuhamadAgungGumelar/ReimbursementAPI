using ReimbursementAPI.Models;
using ReimbursementAPI.Utilities.Enums;

namespace ReimbursementAPI.DTO.Reimbursement
{
    public class NewReimbursementsDto
    {
        public Guid EmployeeGuid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Value { get; set; }
        public string ImageType { get; set; }
        public byte[] Image { get; set; }
        public StatusLevel Status { get; set; }

        public static implicit operator Reimbursements(NewReimbursementsDto dto)
        {
            return new Reimbursements
            {
                EmployeeGuid = dto.EmployeeGuid,
                Name = dto.Name,
                Description = dto.Description,
                Value = dto.Value,
                ImageType = dto.ImageType,
                Image = dto.Image,
                Status = dto.Status,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };
        }
    }
}
