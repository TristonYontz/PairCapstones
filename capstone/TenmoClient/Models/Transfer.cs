namespace TenmoServer.Models
{
    public class Transfer
    {
        public int TransferTypeId { get; set; }
        public int TransferSatusId { get; set; }
        public string AccountFromName { get; set; }
        public string AccountToName { get; set; }
        public decimal Amount { get; set; }
        public int TransferId { get; set; }
    }
}
