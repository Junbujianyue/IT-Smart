using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NBD3.Data;
using NBD3.Models;

namespace NBD3.Controllers
{
    public class ClientsController : Controller
    {
        private readonly NBDContext _context;

        public ClientsController(NBDContext context)
        {
            _context = context;
        }

        // GET: Clients
        public async Task<IActionResult> Index(string companyFilter, string lastNameSearch, string sortField, string sortDirection)
        {
            var clients = await _context.Clients.ToListAsync();

            // Apply filters and search
            if (!string.IsNullOrEmpty(companyFilter))
            {
                clients = clients.Where(c => c.ClientCommpanyName.ToLower().Contains(companyFilter.ToLower())).ToList();
            }

            if (!string.IsNullOrEmpty(lastNameSearch))
            {
                clients = clients.Where(c => c.ClientLastName.ToLower().Contains(lastNameSearch.ToLower())).ToList();
            }

            // Apply sorting
            if (!string.IsNullOrEmpty(sortField))
            {
                switch (sortField)
                {
                    case "ClientCommpanyName":
                        clients = (sortDirection == "asc")
                            ? clients.OrderBy(c => c.ClientCommpanyName).ToList()
                            : clients.OrderByDescending(c => c.ClientCommpanyName).ToList();
                        break;

                    case "ContactFullName":
                        clients = (sortDirection == "asc")
                            ? clients.OrderBy(c => c.ContactFullName).ToList()
                            : clients.OrderByDescending(c => c.ContactFullName).ToList();
                        break;

                    // Add cases for other fields as needed

                    default:
                        // Default sorting logic, if none of the specified cases match
                        clients = clients.OrderBy(c => c.ClientCommpanyName).ToList();
                        break;
                }
            }

            // Populate ViewData for the filter and sort values
            ViewData["companyFilter"] = companyFilter;
            ViewData["lastNameSearch"] = lastNameSearch;
            ViewData["sortField"] = sortField;
            ViewData["sortDirection"] = sortDirection;

            return View(clients);
        }



        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.ClientId == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClientId,ClientCommpanyName,ClientFirstName,ClientMiddleName,ClientLastName,ClientPhone,ClientEmail,ClientStreetAddress,ClientPostalCode,ClientCityAddress,ClientCountryAddress")] Client client)
        {
            if (ModelState.IsValid)
            {
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClientId,ClientCommpanyName,ClientFirstName,ClientMiddleName,ClientLastName,ClientPhone,ClientEmail,ClientStreetAddress,ClientPostalCode,ClientCityAddress,ClientCountryAddress")] Client client)
        {
            if (id != client.ClientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.ClientId))
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
            return View(client);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.ClientId == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Clients == null)
            {
                return Problem("Entity set 'NBDContext.Clients'  is null.");
            }
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
          return _context.Clients.Any(e => e.ClientId == id);
        }
        public ActionResult ClientSection()
        {
            return View();
        }
       

    }
}
