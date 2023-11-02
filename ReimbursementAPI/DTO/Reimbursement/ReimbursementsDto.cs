using ReimbursementAPI.Models;
using ReimbursementAPI.Utilities.Enums;

namespace ReimbursementAPI.DTO.Reimbursement
{
    public class ReimbursementsDto : GeneralDto
    {
        public Guid EmployeeGuid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Value { get; set; }
        public string ImageType { get; set; }
        public byte[] Image { get; set; }
        public StatusLevel Status { get; set; }
        public DateTime CreatedDate { get; set; }

        public static explicit operator ReimbursementsDto(Reimbursements reimbursements)
        {
            return new ReimbursementsDto
            {
                Guid = reimbursements.Guid,
                EmployeeGuid = reimbursements.EmployeeGuid,
                Name = reimbursements.Name,
                Description = reimbursements.Description,
                Value = reimbursements.Value,
                ImageType = reimbursements.ImageType,
                Image = reimbursements.Image,
                Status = reimbursements.Status,
                CreatedDate = reimbursements.CreatedDate,
            };
        }

        public static implicit operator Reimbursements(ReimbursementsDto reimbursementDto)
        {
            return new Reimbursements
            {
                Guid = reimbursementDto.Guid,
                Name = reimbursementDto.Name,
                Description = reimbursementDto.Description,
                Value = reimbursementDto.Value,
                ImageType = reimbursementDto.ImageType,
                Image = reimbursementDto.Image,
                Status = reimbursementDto.Status,
                ModifiedDate = DateTime.Now,
            };
        }
    }
}
