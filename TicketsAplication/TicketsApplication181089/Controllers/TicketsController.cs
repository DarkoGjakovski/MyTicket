using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using TicketsApplication.Domain.DomainModels;
using TicketsApplication.Domain.DTO;
using TicketsApplication.Domain.Identity;
using TicketsApplication.Services.Interfaces;

namespace TicketsApplication.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ITicketService _ticketService;
        private readonly UserManager<TicketsApplicationUser> _userManager;
        public TicketsController(ITicketService TicketServicet, UserManager<TicketsApplicationUser> userManager)
        {
            _ticketService = TicketServicet;
            _userManager = userManager;
        }

        // GET: Tickets
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var toFilter = new TicketDto
            {
                Tickets = this._ticketService.GetAllTickets(),
                Date = DateTime.Now,
                CurrentUserRole = user.Role
            };
            return View(toFilter);
        }
        [HttpPost]
        public IActionResult Index(TicketDto toFilter)
        {
            var tickets = _ticketService.GetAllTickets()
                .Where(z => z.TicketDate.Date == toFilter.Date.Date).ToList(); //ako e ist datumot so odbraniot
            var filtered = new TicketDto
            {
                Tickets = tickets,
                Date = toFilter.Date
            };
            return View(filtered); //vrati filtirani
        }


        public IActionResult AddTicketToCard(Guid? id)
        {
            var model = this._ticketService.GetShoppingCartInfo(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddTicketToCard([Bind("TicketId", "Quantity")] AddToShoppingCardDto item)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = this._ticketService.AddToShoppingCart(item, userId);

            if (result)
            {
                return RedirectToAction("Index", "Tickets");
            }

            return View(item);
        }

        // GET: Tickets/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Ticket = this._ticketService.GetDetailsForTicket(id);

            if (Ticket == null)
            {
                return NotFound();
            }

            return View(Ticket);
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,TicketName,TicketImage,TicketDescription,TicketDate,TicketPrice,Genre,TicketRating")] Ticket Ticket)
        {
            if (ModelState.IsValid)
            {
                this._ticketService.CreateNewTicket(Ticket);
                return RedirectToAction(nameof(Index));
            }
            return View(Ticket);
        }

        public IActionResult Edit(Guid? p)
        {
            if (p == null)
            {
                return NotFound();
            }

            var Ticket = this._ticketService.GetDetailsForTicket(p);

            if (Ticket == null)
            {
                return NotFound();
            }
            return View(Ticket);
        }

        
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,TicketName,TicketImage,TicketDescription,TicketDate,TicketPrice,Genre,TicketRating")] Ticket Ticket)
        {
            if (id != Ticket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    this._ticketService.UpdeteExistingTicket(Ticket);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(Ticket.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(Ticket);
        }

        
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Ticket = this._ticketService.GetDetailsForTicket(id);

            if (Ticket == null)
            {
                return NotFound();
            }

            return View(Ticket);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            this._ticketService.DeleteTicket(id);
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(Guid id)
        {
            return this._ticketService.GetDetailsForTicket(id) != null;
        }
      
        public async Task<FileContentResult> ExportAllTickets(TicketDto toFilter)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if(user.Role != EnumRoles.Administrator)
            {
                throw new Exception("Only administrators can export tickets");
            }
            string fileName = "Tickets.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add("All Tickets");

                worksheet.Cell(1, 1).Value = "Ticket Id";
                worksheet.Cell(1, 2).Value = "Ticket Name";
                worksheet.Cell(1, 3).Value = "Ticket Genre";
                worksheet.Cell(1, 4).Value = "Ticket Price";
                List<Ticket> result = new List<Ticket>();
              
                if (!toFilter.Genre.HasValue)
                {
                   result = _ticketService.GetAllTickets();
                }

                else
                {
                   result = _ticketService.GetAllTickets().Where(z => z.Genre == toFilter.Genre.Value).ToList();
                }

                for (int i = 1; i <= result.Count(); i++)
                {
                    var item = result[i - 1];

                    worksheet.Cell(i + 1, 1).Value = item.Id.ToString();
                    worksheet.Cell(i + 1, 2).Value = item.TicketName;
                    worksheet.Cell(i + 1, 3).Value = item.Genre;
                    worksheet.Cell(i + 1, 4).Value = item.TicketPrice;

                    //for (int p = 0; p < item.TicketInOrders.Count(); p++)
                    //{
                    //    worksheet.Cell(1, p + 3).Value = "Ticket-" + (p + 1);
                    //    worksheet.Cell(i + 1, p + 3).Value = item.TicketInOrders.ElementAt(p).OrderedTicket.TicketName;  
                    //}
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(content, contentType, fileName);
                }

            }
        }
    }
}
