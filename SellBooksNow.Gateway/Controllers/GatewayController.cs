using Microsoft.AspNetCore.Mvc;
using SellBooksNow.Gateway.Models;

namespace SellBooksNow.Gateway.Controllers
{
    [ApiController]
    public class GatewayController : ControllerBase
    {
        private static readonly List<Transaction> _transactions = new List<Transaction>();

    }
}