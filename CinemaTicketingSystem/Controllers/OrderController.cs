using CinemaTicketingSystem.Domain;
using CinemaTicketingSystem.Service.Interface;
using CinemaTicketingSystem.Web.Controllers;
using GemBox.Document;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTicketingSystem.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<TicketsController> _logger;

        public OrderController(IOrderService orderService, ILogger<TicketsController> logger)
        {
            _orderService = orderService;
            _logger = logger;
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
        }

        public IActionResult Index()
        {
            _logger.LogInformation("User Request -> Get All Orders!");
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return View(this._orderService.GetAllOrders(userId));
        }

        public IActionResult Details(BaseEntity model)
        {
            _logger.LogInformation("User Request -> Get Details For Order");
            if (model == null)
            {
                return NotFound();
            }

            var order = this._orderService.GetOrderDetails(model);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        public IActionResult CreateInvoice(BaseEntity model)
        {
            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Invoice.docx");

            var document = DocumentModel.Load(templatePath);

            var order = this._orderService.GetOrderDetails(model);

            document.Content.Replace("{{OrderNumber}}", order.Id.ToString());
            document.Content.Replace("{{CustomerEmail}}", order.User.Email);
            //document.Content.Replace("{{CustomerInfo}}", (order.User.FirstName + " " + order.User.LastName));

            StringBuilder sb = new StringBuilder();

            var total = 0.0;

            foreach (var item in order.TicketsInOrder)
            {
                total += item.Quantity * item.Ticket.TicketPrice;
                sb.AppendLine(item.Ticket.TicketName + " with quantity of: " + item.Quantity + " and price of: $" + item.Ticket.TicketPrice);
            }

            document.Content.Replace("{{AllProducts}}", sb.ToString());
            document.Content.Replace("{{TotalPrice}}", "$" + total.ToString());

            var stream = new MemoryStream();

            document.Save(stream, new PdfSaveOptions());


            return File(stream.ToArray(), new PdfSaveOptions().ContentType, "ExportInvoice.pdf");
        }
    }
}
