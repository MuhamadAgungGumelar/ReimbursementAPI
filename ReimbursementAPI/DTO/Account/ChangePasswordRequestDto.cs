namespace ReimbursementAPI.DTO.Account
{
    public class ChangePasswordRequestDto
    {
        public string Email { get; set; }
        public int Otp { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
