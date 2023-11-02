namespace TenmoServer.Models
{
    public class TransferRequest
    {

        public int ToId { get; set; }
        public int FromId { get; set; }
        public decimal Amount { get; set; }

    }
}
