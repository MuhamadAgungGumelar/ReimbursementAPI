using ReimbursementAPI.Models;

namespace ReimbursementAPI.DTO.Finance
{
    public class FinancesDto : GeneralDto
    {
        public Guid EmployeeGuid { get; set; }
        public string Name { get; set; }
        public DateTime ApprovedDate { get; set; }
        public int ApprovedAmount { get; set; }

        public static explicit operator FinancesDto(Finances finances)
        {
            return new FinancesDto
            {
                Guid = finances.Guid,
                EmployeeGuid = finances.EmployeeGuid,
                Name = finances.Name,
                ApprovedDate = finances.ApprovedDate,
                ApprovedAmount = finances.ApprovedAmount,
            };
        }

        public static implicit operator Finances(FinancesDto financesDto)
        {
            return new Finances
            {
                Guid = financesDto.Guid,
                EmployeeGuid = financesDto.EmployeeGuid,
                Name = financesDto.Name,
                ApprovedDate = financesDto.ApprovedDate,
                ApprovedAmount = financesDto.ApprovedAmount,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
