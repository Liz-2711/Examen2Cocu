using Microsoft.VisualBasic;

namespace Billing.DTOS
{
    public class PayInfo
    {

    public int NumeroCvv { get; set; }
        public string NombreProp { get; set; }
        public Guid NumeroTrajeta { get; set; }
public int Year { get; set; }
        public int dia { get; set; }
        public int mes { get; set; }


    }
}
