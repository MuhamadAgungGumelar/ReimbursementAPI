using ReimbursementAPI.Models;

namespace ReimbursementAPI.DTO.Account
{
    public class AccountsDto : GeneralDto
    {
        public int Otp { get; set; } //deklarasi property
        public string Password { get; set; }
        public bool IsUsed { get; set; } //deklarasi property
        public DateTime ExpiredTime { get; set; }

        public static explicit operator AccountsDto(Accounts accounts) //implementasi explicit Operator
        {
            return new AccountsDto
            {
                Guid = accounts.Guid,
                Password = accounts.Password,
                Otp = accounts.Otp,
                IsUsed = accounts.IsUsed,
                ExpiredTime = accounts.ExpiredTime,
            };
        }

        public static implicit operator Accounts(AccountsDto accountsDto) //implementasi implicit Operator
        {
            return new Accounts
            {
                Guid = accountsDto.Guid,
                Password = accountsDto.Password,
                Otp = accountsDto.Otp,
                IsUsed = accountsDto.IsUsed,
                ExpiredTime = accountsDto.ExpiredTime,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
