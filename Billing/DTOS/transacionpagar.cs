namespace Billing.DTOS
{
    public class transacionpagar
    {
        public Guid Id{get; set }
        public Status StatusC {get; set }
        public List<string > err { get; set;  }

    }
}
