namespace ReimbursementAPI.Utilities.Handler
{
    public class OtpHandler
    {
        public static int GenerateRandomOtp()
        {
            var random = new Random();
            int otpValue = random.Next(100000, 999999); //mengambil random value dari 100000 - 999999
            return otpValue; //mengembalikan nilai otp
        }
    }
}
