namespace ReimbursementAPI.Contracts
{
    public interface IEmailHandler
    {
        void Send(string subject, string body, string toEmail);
    }
}
