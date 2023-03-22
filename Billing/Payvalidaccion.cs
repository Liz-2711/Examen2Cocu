using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using Billing.DTOS.PayDataTransferObject;
using Billing.DTOS;
//namespace Billing;

public class Payvalidaccion
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly EventingBasicConsumer _consumer;

    public Payvalidaccion()
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            Port = 5672
        };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare("tarjeta-queu", false, false, false, null);
        _consumer = new EventingBasicConsumer(_channel);
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        _consumer.Received += async (model, content) =>
        {
            var body = content.Body.ToArray();
            





            var json = Encoding.UTF8.GetString(body);
            var PayInfo = JsonConvert.DeserializeObject<PayInfo>(json);
            var Result = await ProcessPayment(payInfo, cancellationToken);
            var message = $"La tarjeta se esta procesando {PayInfo.Id} El estado de la tarjeta:  {Result.transaccionpager.Status}";
            Console.WriteLine(message);
            NotifyPaymentResult(Result.transaccionpagar);
        };
        _channel.BasicConsume("payment-queue", true, _consumer);
        return Task.CompletedTask;
    }
    

   private void NotifyPaymentResult(transacionpagar transacionpagar)
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            Port = 5672
        };

        using (var connection = factory.CreateConnection())
        {
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare("tarjeta-queu", false, false, false, null);
                var json = JsonConvert.SerializeObject(transacionpagar);
                var body = Encoding.UTF8.GetBytes(json);
                channel.BasicPublish(string.Empty, "tarjeta-queu", null, body);
            }
        }
    }

   /* Crear un proyecto Billing: este servicio valida que el número de la tarjeta no este vacío, que
el cvv no este vacío, que el nombre en la tarjeta no este vació y que la fecha en la tarjeta sea
mayor al día actual.Si todas estas validaciones resultan exitosas cambia el estado de la
transacción a “Cobrado” en el Gateway y envía un mensaje(utilizando RabbitMq) al servicio
de Shipping.*/
    private async Task<PayDataTransferObject> ProcessPayment(PayInfo PayInfom,
        CancellationToken token)
    {
        var errors = new List<string>();
        if (PayInfo.NuemroTarjeta == Guid.Empty)
        {
            errors.Add("Necesita  un numero valido.");
        }
        await Task.Delay(2000, token);

        if (PayInfo.Cvv <= 0)
        {
            errors.Add("La tarjeta debe tener un cvv valido.");
        }

        await Task.Delay(2000, token);

        
        if (PayInfo.mes <= 0 || PayInfo.PayInfom.Year < 0)
        {
            errors.Add("La tarjeta debe tener una fecha de expiracion valida.");
        }

        if (PayInfom.NumeroCvv == Guid.Empty)
        {
            errors.Add("El pago debe ser procesado para un basket valido.");
        }


        PayInfo.Status = errors.Any() ? Status.Errored : Status.Done;
        PayInfo.err = errors;
        return PayInfo;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            Console.WriteLine($"Cobrado! {DateTimeOffset.Now}");
            await Task.Delay(1000, stoppingToken);
        }
    }

}
