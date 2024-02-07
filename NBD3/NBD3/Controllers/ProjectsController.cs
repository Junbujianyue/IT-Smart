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
    public class ProjectsController : Controller
    {
        private readonly NBDContext _context;

        public ProjectsController(NBDContext context)
        {
            _context = context;
        }

        // GET: Projects
        public async Task<IActionResult> Index(string searchString, int? clientId, string sortOrder, DateTime? startDateFilter, DateTime? endDateFilter)
        {
            ViewBag.NameSortParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.ClientSortParam = sortOrder == "client" ? "client_desc" : "client";
            ViewBag.LocationSortParam = sortOrder == "location" ? "location_desc" : "location";
            ViewBag.StartDateSortParam = sortOrder == "start_date" ? "start_date_desc" : "start_date";
            ViewBag.EndDateSortParam = sortOrder == "end_date" ? "end_date_desc" : "end_date";

            var projectsQuery = _context.Projects
                .Include(p => p.Client)
                .Include(p => p.Location)
                .Where(project =>
                    (string.IsNullOrEmpty(searchString) || project.ProjectName.ToLower().Contains(searchString.ToLower())) &&
                    (!clientId.HasValue || project.ClientId == clientId.Value));

            
            if (startDateFilter.HasValue)
            {
                var startDate = startDateFilter.Value.Date;
                projectsQuery = projectsQuery.Where(project =>
                    project.ProjectStartDate.Year >= startDate.Year &&
                    project.ProjectStartDate.Month >= startDate.Month &&
                    project.ProjectStartDate.Day >= startDate.Day);
            }

            if (endDateFilter.HasValue)
            {
                var endDate = endDateFilter.Value.Date;
                projectsQuery = projectsQuery.Where(project =>
                    project.ProjectEndDate.HasValue &&
                    (project.ProjectEndDate.Value.Year <= endDate.Year ||
                    (project.ProjectEndDate.Value.Year == endDate.Year &&
                    (project.ProjectEndDate.Value.Month <= endDate.Month ||
                    (project.ProjectEndDate.Value.Month == endDate.Month &&
                    project.ProjectEndDate.Value.Day <= endDate.Day)))));
            }

            projectsQuery = sortOrder switch
            {
                "name_desc" => projectsQuery.OrderByDescending(p => p.ProjectName),
                "client" => projectsQuery.OrderBy(p => p.Client.ClientFirstName),
                "client_desc" => projectsQuery.OrderByDescending(p => p.Client.ClientFirstName),
                "location" => projectsQuery.OrderBy(p => p.Location.LocationName),
                "location_desc" => projectsQuery.OrderByDescending(p => p.Location.LocationName),
                "start_date" => projectsQuery.OrderBy(p => p.ProjectStartDate),
                "start_date_desc" => projectsQuery.OrderByDescending(p => p.ProjectStartDate),
                "end_date" => projectsQuery.OrderBy(p => p.ProjectEndDate),
                "end_date_desc" => projectsQuery.OrderByDescending(p => p.ProjectEndDate),
                _ => projectsQuery.OrderBy(p => p.ProjectName),
            };

            var projects = await projectsQuery.ToListAsync();

            ViewBag.Clients = await _context.Clients.ToListAsync();
            ViewBag.SearchString = searchString;
            ViewBag.StartDateFilter = startDateFilter;
            ViewBag.EndDateFilter = endDateFilter;

            return View(projects);
        }


        // GET: Projects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.Client)
                .Include(p => p.Location)
                .FirstOrDefaultAsync(m => m.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ContactFullName");
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationName");
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjectId,ProjectName,ProjectDescription,ProjectLocation,ProjectStartDate,ProjectEndDate,ClientId,LocationId")] Project project)
        {
            if (ModelState.IsValid)
            {
                _context.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ContactFullName", project.ClientId);
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationName", project.LocationId);
            return View(project);
        }

        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ContactFullName", project.ClientId);
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationName", project.LocationId);
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjectId,ProjectName,ProjectDescription,ProjectLocation,ProjectStartDate,ProjectEndDate,ClientId,LocationId")] Project project)
        {
            if (id != project.ProjectId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.ProjectId))
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
            else
            {
                

                ViewBag.ErrorMessage = "Start date cannot be greater than end date.";
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ContactFullName", project.ClientId);
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationName", project.LocationId);
            return View(project);
        }

        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.Client)
                .Include(p => p.Location)
                .FirstOrDefaultAsync(m => m.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Projects == null)
            {
                return Problem("Entity set 'NBDContext.Projects'  is null.");
            }
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id)
        {
          return _context.Projects.Any(e => e.ProjectId == id);
        }
		public ActionResult ProjectsAndMaintenance()
		{
			return View();
		}
	}
}
