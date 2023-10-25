using ReimbursementAPI.Models;

namespace ReimbursementAPI.DTO.Finance
{
    public class NewFinancesDto
    {
        public Guid EmployeeGuid { get; set; }
        public string Name { get; set; }
        public DateTime ApprovedDate { get; set; }
        public int ApprovedAmount { get; set; }

        public static implicit operator Finances(NewFinancesDto dto)
        {
            return new Finances
            {
                EmployeeGuid = dto.EmployeeGuid,
                Name = dto.Name,
                ApprovedDate = dto.ApprovedDate,
                ApprovedAmount = dto.ApprovedAmount,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };
        }
    }
}
