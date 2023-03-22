namespace Billing.DTOS
{
    public class PayDataTransferObject
    {

        PayDataTransferObject methadopay { get; set; }
        public Guid IdBookComp { get; set; }

        public decimal Total { get; set; }
        public transacionpagar transaccionpagar { get; set; }
    }
}
