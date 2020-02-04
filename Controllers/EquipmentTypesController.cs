using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestEquipment.Data;
using TestEquipment.Models.DataTransferObjects;
using TestEquipment.Models.Entities;
using TestEquipment.Services;

namespace TestEquipment.Controllers
{
    [Authorize]
    public class EquipmentTypesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEquipmentService _equipSvc;
        private readonly IEquipmentTypeService _equipmentTypeSvc;


        public EquipmentTypesController(ApplicationDbContext context,
            IEquipmentService equipSvc,
            IEquipmentTypeService equipTypeSvc)
        {
            _context = context;
            _equipmentTypeSvc = equipTypeSvc;
            _equipSvc = equipSvc;
        }

        // GET: EquipmentTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.EquipmentType.ToListAsync());
        }
        public IActionResult GetEquips()
        {
            var equiptypes = _context.EquipmentType.ToList();
            var list = from equip in equiptypes
                      where equip.IsDeleted == false
                       select new EquipmentTypeDTO
                       {
                           Id = equip.Id,
                           Name = equip.Name,
                           CreationTime = equip.CreationTime,
                           IsDeleted = equip.IsDeleted,
                           DeletionDate = equip.DeletionDate,
                           Description = equip.Description
                       };
            return Json(new
            {
                data = list
            });
        }

        // GET: EquipmentTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipmentType = await _context.EquipmentType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (equipmentType == null)
            {
                return NotFound();
            }

            return View(equipmentType);
        }

        // GET: EquipmentTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EquipmentTypeDTO equipment)
        {
            if (ModelState.IsValid)
            {
                await _equipmentTypeSvc.Add(equipment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(equipment);
        }

        // GET: EquipmentTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipmentType = await _context.EquipmentType.FindAsync(id);
            if (equipmentType == null)
            {
                return NotFound();
            }
            return View(equipmentType);
        }

        // POST: EquipmentTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] EquipmentType equipmentType)
        {
            if (id != equipmentType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(equipmentType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipmentTypeExists(equipmentType.Id))
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
            return View(equipmentType);
        }

        // GET: EquipmentTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipmentType = await _context.EquipmentType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (equipmentType == null)
            {
                return NotFound();
            }

            return View(equipmentType);
        }

        // POST: EquipmentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var equipmentType = await _context.EquipmentType.FindAsync(id);
            equipmentType.IsDeleted = true;
            equipmentType.DeletionDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EquipmentTypeExists(int id)
        {
            return _context.EquipmentType.Any(e => e.Id == id);
        }
    }
}
