using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LFS_Tracker.Data;
using LFS_Tracker.Models;
using System.Collections.ObjectModel;

namespace LFS_Tracker.Controllers
{
    public class LfsInstanceController : Controller
    {
        private readonly DBContext _context;

        public LfsInstanceController(DBContext context)
        {
            _context = context;
        }

        // GET: LfsInstance
        public async Task<IActionResult> Index()
        {
              return View(await _context.LfsInstance.ToListAsync());
        }

        // GET: LfsInstance/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LfsInstance == null)
            {
                return NotFound();
            }

            var lfsInstance = await _context.LfsInstance.Include(instance => instance.InstalledPackages)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lfsInstance == null)
            {
                return NotFound();
            }

            return View(lfsInstance);
        }

        // GET: LfsInstance/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LfsInstance/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,InstanceName,Version,LfsVersion")] LfsInstance lfsInstance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lfsInstance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lfsInstance);
        }

        // GET: LfsInstance/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LfsInstance == null)
            {
                return NotFound();
            }

            var lfsInstance = await _context.LfsInstance.Include(instance => instance.InstalledPackages).FirstAsync(instance => instance.Id == id);

            //var lfsInstance = await _context.LfsInstance.FindAsync(id);
            if (lfsInstance == null)
            {
                return NotFound();
            }
            return View(lfsInstance);
        }

        // POST: LfsInstance/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,InstanceName,Version,LfsVersion")] LfsInstance lfsInstance)
        {
            if (id != lfsInstance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lfsInstance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LfsInstanceExists(lfsInstance.Id))
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
            return View(lfsInstance);
        }

        // GET: LfsInstance/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LfsInstance == null)
            {
                return NotFound();
            }

            var lfsInstance = await _context.LfsInstance
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lfsInstance == null)
            {
                return NotFound();
            }

            return View(lfsInstance);
        }

        // POST: LfsInstance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LfsInstance == null)
            {
                return Problem("Entity set 'DBContext.LfsInstance'  is null.");
            }
            var lfsInstance = await _context.LfsInstance.FindAsync(id);
            if (lfsInstance != null)
            {
                _context.LfsInstance.Remove(lfsInstance);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LfsInstanceExists(int id)
        {
          return _context.LfsInstance.Any(e => e.Id == id);
        }

        // Get LfsInstance/ManagePackages/5
        public async Task<IActionResult> ManagePackages(int? id)
        {
            if (id == null || _context.LfsInstance == null)
            {
                return NotFound();
            }

            var lfsInstance = await _context.LfsInstance.Include(instance => instance.InstalledPackages).FirstAsync(instance => instance.Id == id);
            if (lfsInstance == null)
            {
                return NotFound();
            }
            ManagePackageModel managePackageModel = new ManagePackageModel(lfsInstance.Id, lfsInstance.InstanceName, lfsInstance.InstalledPackages, await GetPackagesNotInInstance(lfsInstance));
            return View(managePackageModel);
        }

        // Post LfsInstance/ManagePackages/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManagePackages(int id, ManagePackageModel model)
        {
            if(_context.LfsInstance == null)
            {
                return NotFound();
            }

            var lfsInstance = await _context.LfsInstance.Include(instance => instance.InstalledPackages).FirstAsync(instance => instance.Id == id);
            if (lfsInstance == null)
            {
                return NotFound();
            }

            if(model.AddPackages != null)
            {
                foreach(var packageId in model.AddPackages)
                {
                    
                    var package = await _context.Package.FindAsync(int.Parse(packageId));
                    lfsInstance.InstalledPackages.Add(package);
                }
            }

            if(model.RemovePackages != null)
            {
                foreach(var packageId in model.RemovePackages)
                {
                    var package = await _context.Package.FindAsync(int.Parse(packageId));
                    lfsInstance.InstalledPackages.Remove(package);
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public class ManagePackageModel
        {
            public int LfsInstanceId { get; set; }
            public string LfsInstanceName { get; set; }

            public List<Package> InstancePackages { get; set; }
            public List<Package> OtherPackages { get; set; }

            public List<string> AddPackages { get; set; }
            public List<string> RemovePackages { get; set; }

            public ManagePackageModel()
            {

            }

            public ManagePackageModel(int id, string name, List<Package> installedPackages, List<Package> otherPackages)
            {
                LfsInstanceId = id;
                LfsInstanceName = name;
                InstancePackages = installedPackages;
                OtherPackages = otherPackages;
            }
        }

        private async Task<List<Package>> GetPackagesNotInInstance(LfsInstance instance)
        {
            var packages = await _context.Package.Distinct().ToListAsync();
            foreach(var package in instance.InstalledPackages)
            {
                packages.Remove(package);
            }
            return packages;
        }
    }
}
