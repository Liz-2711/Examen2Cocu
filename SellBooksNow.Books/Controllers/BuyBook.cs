using Microsoft.AspNetCore.Mvc;

namespace SellBooksNow.Books.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BuyBook
    {
         if (Database.Numberocopies.Any(x => x.Isbn == book.Isbn))
        {
            return BadRequest();
    }
    var ShopDto = new BooktDataTransferObject
    {
        Isbn =Books.Isbn,

     Title = Books.Tittle,

   Pages = Books.Pages

   Price = Book.Price

     NumberOfCopies =0
}
    };
    Database.Bookd(BookDto);

        return Ok(BookDto);
}
}
