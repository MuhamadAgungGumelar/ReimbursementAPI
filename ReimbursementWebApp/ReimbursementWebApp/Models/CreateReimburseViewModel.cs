using ReimbursementAPI.Utilities.Enums;

namespace ReimbursementWebApp.Models
{
    public class CreateReimburseViewModel
    {
        public Guid EmployeeGuid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Value { get; set; }
        public StatusLevel Status { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
