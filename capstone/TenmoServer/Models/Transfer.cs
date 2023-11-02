namespace TenmoServer.Models
{
    public class Transfer
    {
        public int TransferTypeId { get; set; }
        public int TransferSatusId { get; set; }
        public int AccountFrom { get; set; }
        public int AccountTo { get; set; }
        public decimal Amount { get; set; }
    }
}
