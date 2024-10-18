using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LFS_Tracker.Data;
using LFS_Tracker.Models;

namespace LFS_Tracker.Controllers
{
    public class PackageController : Controller
    {
        private readonly DBContext _context;

        public PackageController(DBContext context)
        {
            _context = context;
        }

        // GET: Package
        public async Task<IActionResult> Index()
        {
              return View(await _context.Package.ToListAsync());
        }

        // GET: Package/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Package == null)
            {
                return NotFound();
            }

            var package = await _context.Package.Include(p => p.InstalledInstances)
                .FirstOrDefaultAsync(m => m.PackageId == id);
            if (package == null)
            {
                return NotFound();
            }

            return View(package);
        }

        // GET: Package/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Package/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PackageId,PackageName,Version,LfsVersion,IsCoreLfsPackage")] Package package)
        {
            if (ModelState.IsValid)
            {
                _context.Add(package);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(package);
        }

        // GET: Package/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Package == null)
            {
                return NotFound();
            }

            var package = await _context.Package.Include(p => p.InstalledInstances)
                .FirstOrDefaultAsync(p => p.PackageId == id);
            if (package == null)
            {
                return NotFound();
            }
            return View(package);
        }

        // POST: Package/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PackageId,PackageName,Version,LfsVersion,IsCoreLfsPackage")] Package package)
        {
            if (id != package.PackageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(package);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PackageExists(package.PackageId))
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
            return View(package);
        }

        // GET: Package/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Package == null)
            {
                return NotFound();
            }

            var package = await _context.Package
                .FirstOrDefaultAsync(m => m.PackageId == id);
            if (package == null)
            {
                return NotFound();
            }

            return View(package);
        }

        // POST: Package/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Package == null)
            {
                return Problem("Entity set 'DBContext.Package'  is null.");
            }
            var package = await _context.Package.FindAsync(id);
            if (package != null)
            {
                _context.Package.Remove(package);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PackageExists(int id)
        {
          return _context.Package.Any(e => e.PackageId == id);
        }
    }
}
