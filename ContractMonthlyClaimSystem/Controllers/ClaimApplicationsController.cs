using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContractMonthlyClaimSystem.Data;
using ContractMonthlyClaimSystem.Models;

namespace ContractMonthlyClaimSystem.Controllers
{
    public class ClaimApplicationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClaimApplicationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ClaimApplications
        public async Task<IActionResult> Index()
        {
            var awaitingstatus = _context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "ClaimApprovalStatus" && y.Code == "Awaiting Approval").FirstOrDefault();

            var applicationDbContext = _context.ClaimApplications
                .Include(c => c.ClaimType)
                .Include(c => c.Duration)
                .Include(c => c.Lecturer)
                .Include(c => c.Status)
                .Where(c => c.StatusId == awaitingstatus!.Id);
            ;
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> ApprovedClaimApplication()
        {
            var approvedstatus = _context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "ClaimApprovalStatus" && y.Code == "Approved").FirstOrDefault();

            var applicationDbContext = _context.ClaimApplications
                .Include(c => c.ClaimType)
                .Include(c => c.Duration)
                .Include(c => c.Lecturer)
                .Include(c => c.Status)
                .Where(c => c.StatusId== approvedstatus!.Id);
            
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> RejectedClaimApplication()
        {
            var rejectedstatus = _context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "ClaimApprovalStatus" && y.Code == "Rejected").FirstOrDefault();

            var applicationDbContext = _context.ClaimApplications
                .Include(c => c.ClaimType)
                .Include(c => c.Duration)
                .Include(c => c.Lecturer)
                .Include(c => c.Status)
                .Where(c => c.StatusId == rejectedstatus!.Id);

            return View(await applicationDbContext.ToListAsync());
        }


        // GET: ClaimApplications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claimApplication = await _context.ClaimApplications
                .Include(c => c.ClaimType)
                .Include(c => c.Duration)
                .Include(c => c.Lecturer)
                .Include(c => c.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (claimApplication == null)
            {
                return NotFound();
            }

            return View(claimApplication);
        }

        // GET: ClaimApplications/Create
        public IActionResult Create()
        {
            ViewData["ClaimTypeId"] = new SelectList(_context.ClaimTypes, "Id", "Name");
            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails.Include(x=> x.SystemCode).Where(y=> y.SystemCode.Code== "ClaimDuration"), "Id", "Description");
            ViewData["LecturerId"] = new SelectList(_context.Lecturers, "Id", "FullName");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> RejectClaim(int? id)
        {
            var claimApplication = await _context.ClaimApplications
                .Include(c => c.ClaimType)
                .Include(c => c.Duration)
                .Include(c => c.Lecturer)
                .Include(c => c.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (claimApplication == null)
            {
                return NotFound();
            }
            ViewData["ClaimTypeId"] = new SelectList(_context.ClaimTypes, "Id", "Name");
            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "ClaimDuration"), "Id", "Description");
            ViewData["LecturerId"] = new SelectList(_context.Lecturers, "Id", "FullName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RejectClaim(ClaimApplication claim)
        {
            var rejectedstatus = _context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "ClaimApprovalStatus" && y.Code == "Rejected").FirstOrDefault();

            var claimApplication = await _context.ClaimApplications
                .Include(c => c.ClaimType)
                .Include(c => c.Duration)
                .Include(c => c.Lecturer)
                .Include(c => c.Status)
                .FirstOrDefaultAsync(m => m.Id == claim.Id);
            if (claimApplication == null)
            {
                return NotFound();
            }

            claimApplication.ApprovedOn = DateTime.Now;
            claimApplication.ApprovedId = "Kamogelo";
            claimApplication.StatusId = rejectedstatus!.Id;
            claimApplication.ApprovalNotes = claim.ApprovalNotes;

            _context.Update(claimApplication);
            await _context.SaveChangesAsync();

            ViewData["ClaimTypeId"] = new SelectList(_context.ClaimTypes, "Id", "Name");
            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "ClaimDuration"), "Id", "Description");
            ViewData["LecturerId"] = new SelectList(_context.Lecturers, "Id", "FullName");
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> ApproveClaim(int? id)
        {
            var claimApplication = await _context.ClaimApplications
                .Include(c => c.ClaimType)
                .Include(c => c.Duration)
                .Include(c => c.Lecturer)
                .Include(c => c.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (claimApplication == null)
            {
                return NotFound();
            }
            ViewData["ClaimTypeId"] = new SelectList(_context.ClaimTypes, "Id", "Name");
            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "ClaimDuration"), "Id", "Description");
            ViewData["LecturerId"] = new SelectList(_context.Lecturers, "Id", "FullName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ApproveClaim(ClaimApplication claim)
        {
            var approvedstatus = _context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "ClaimApprovalStatus" && y.Code=="Approved").FirstOrDefault();

            var claimApplication = await _context.ClaimApplications
                .Include(c => c.ClaimType)
                .Include(c => c.Duration)
                .Include(c => c.Lecturer)
                .Include(c => c.Status)
                .FirstOrDefaultAsync(m => m.Id == claim.Id);
            if (claimApplication == null)
            {
                return NotFound();
            }

            claimApplication.ApprovedOn = DateTime.Now;
            claimApplication.ApprovedId = "Kamogelo";
            claimApplication.StatusId = approvedstatus!.Id;
            claimApplication.ApprovalNotes = claim.ApprovalNotes;

            _context.Update(claimApplication);
            await _context.SaveChangesAsync();

            ViewData["ClaimTypeId"] = new SelectList(_context.ClaimTypes, "Id", "Name");
            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "ClaimDuration"), "Id", "Description");
            ViewData["LecturerId"] = new SelectList(_context.Lecturers, "Id", "FullName");
            return RedirectToAction(nameof(Index));
        }

        // POST: ClaimApplications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClaimApplication claimApplication)
        {
            var pendingStatus = _context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.Code == "                .Where(c => c.StatusId== approvedstatus!.Id);\r\n" && y.SystemCode.Code == "ClaimApprovalStatus").FirstOrDefaultAsync();
           
                claimApplication.CreatedById = "Kamogelo";
                claimApplication.CreatedOn = DateTime.Now;
                claimApplication.StatusId = pendingStatus.Id;
                _context.Add(claimApplication);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            ViewData["ClaimTypeId"] = new SelectList(_context.ClaimTypes, "Id", "Name", claimApplication.ClaimTypeId);
            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "ClaimDuration"), "Id", "Description", claimApplication.DurationId);
            ViewData["LecturerId"] = new SelectList(_context.Lecturers, "Id", "FullName", claimApplication.LecturerId);
            return View(claimApplication);
        }

        // GET: ClaimApplications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claimApplication = await _context.ClaimApplications.FindAsync(id);
            if (claimApplication == null)
            {
                return NotFound();
            }
            ViewData["ClaimTypeId"] = new SelectList(_context.ClaimTypes, "Id", "Name", claimApplication.ClaimTypeId);
            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "ClaimDuration"), "Id", "Description", claimApplication.DurationId);
            ViewData["LecturerId"] = new SelectList(_context.Lecturers, "Id", "FullName", claimApplication.LecturerId);
            return View(claimApplication);
        }

        // POST: ClaimApplications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClaimApplication claimApplication)
        {
            if (id != claimApplication.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var pendingStatus = _context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.Code == "Pending" && y.SystemCode.Code == "ClaimApprovalStatus").FirstOrDefaultAsync();

                try
                {
                    claimApplication.ModifiedById = "Kamogelo";
                    claimApplication.ModifiedOn = DateTime.Now;
                    claimApplication.StatusId = pendingStatus.Id;
                    _context.Update(claimApplication);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClaimApplicationExists(claimApplication.Id))
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
            ViewData["ClaimTypeId"] = new SelectList(_context.ClaimTypes, "Id", "Id", claimApplication.ClaimTypeId);
            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails, "Id", "Id", claimApplication.DurationId);
            ViewData["LecturerId"] = new SelectList(_context.Lecturers, "Id", "Id", claimApplication.LecturerId);
            ViewData["StatusId"] = new SelectList(_context.SystemCodeDetails, "Id", "Id", claimApplication.StatusId);
            return View(claimApplication);
        }

        // GET: ClaimApplications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claimApplication = await _context.ClaimApplications
                .Include(c => c.ClaimType)
                .Include(c => c.Duration)
                .Include(c => c.Lecturer)
                .Include(c => c.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (claimApplication == null)
            {
                return NotFound();
            }

            return View(claimApplication);
        }

        // POST: ClaimApplications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var claimApplication = await _context.ClaimApplications.FindAsync(id);
            if (claimApplication != null)
            {
                _context.ClaimApplications.Remove(claimApplication);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClaimApplicationExists(int id)
        {
            return _context.ClaimApplications.Any(e => e.Id == id);
        }
    }
}
