using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using SellBooksNow.Books.Dtos;
using System.Text;
using System.Threading.Channels;
using Newtonsoft.Json;

namespace SellBooksNow.Books.Controllers
{


    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        // Queremos un endpoint en esta parte 
        //definir el tipo de retorno ActionResult, para confirmar el estatus

        public ActionResult<IEnumerable<BookDataTransferObject>> Getbook()
        {

            return Ok(Database.Books);
        }
        //"id" sirve como parametro
        [HttpGet("{id:int}", Name = "GetBook")]
        //Buscar por Id 
        [ProducesResponseType(200)]
        // 200 es okay , 404 es notFound 
        [ProducesResponseType(404)]

        //buscar an un book por su id 
        public ActionResult<BookDataTransferObject> GetVilla(int id)
        {

            if (id== 0)
            {
                return BadRequest();
            }
            var bk = Database.Books.FirstOrDefault(u => u.Id==id);
            if (bk==null)
            {
                return NotFound();

            }

            return Ok(bk);
        }
        //"id" sirve como parametro
        [HttpGet("{id:int}", Name = "GetBook")]
        //Buscar por Id 
        [ProducesResponseType(200)]
        // 200 es okay , 404 es notFound 
        [ProducesResponseType(404)]

        //buscar an un book por su id 
        public ActionResult<BookDataTransferObject> GetBookCopy(int copy)
        {

            if (copy< 0)
            {
                return BadRequest();
            }
            var bk = Database.Books.FirstOrDefault(u => u.NumberOfCopies==copy);
            if (bk==null)
            {
                return NotFound();

            }

            return Ok(bk);
        }


        private static void SendDataBillingtoBill(List<BookDataTransferObject> bOOKlIST)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "validation_queue",
                                durable: false,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

                string message = JsonConvert.SerializeObject(dataSalesList);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                    routingKey: "validation_queue",
                                    basicProperties: null,
                                    body: body);

                Console.WriteLine(" [x] Sent {0}", message);
            }
        }


    }
}