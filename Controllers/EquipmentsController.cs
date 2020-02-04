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
    public class EquipmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEquipmentService _equipSvc;
        private readonly IEquipmentTypeService _equipmentTypeSvc;

        public EquipmentsController(ApplicationDbContext context, 
            IEquipmentService equipSvc,
            IEquipmentTypeService equipTypeSvc)
        {
            _context = context;
            _equipSvc = equipSvc;
            _equipmentTypeSvc = equipTypeSvc;
        }

        // GET: Equipments
        public async Task<IActionResult> Index()
        {
            return View(await _context.Equipment.ToListAsync());
        }
        public IActionResult GetEquips()
        {
            var equips = _context.Equipment.ToList();
            var equiptypes = _context.EquipmentType.ToList();
            var list = from equip in equips
                       join equiptype in equiptypes on equip.EquipmentTypeId equals equiptype.Id
                       into types
                       from equiptype in types.DefaultIfEmpty()
                       where equip.IsDeleted == false
                       select new EquipmentDTO
                       {
                           Id = equip.Id,
                           Name = equip.Name,
                           CreationTime = equip.CreationTime,
                           IsDeleted = equip.IsDeleted,
                           EquipmentTypeName = equiptype.Name ?? "No Type Defined",
                           EquipmentTypeDescription = equiptype.Description ?? "No Type Defined",
                           Quantity = equip.Quantity,
                           Status = equip.Status,
                           Statuses = equip.Status.ToString()
                       };
            return Json(new
            {
                data = list
            });
        }

        // GET: Equipments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var model = (from equip in _context.Equipment
                         join type in _context.EquipmentType on equip.EquipmentTypeId equals type.Id
                         into types from type in types.DefaultIfEmpty()
                         select new EquipmentDTO
                         {
                             Name = equip.Name,
                             CreationTime = equip.CreationTime,
                             Status = equip.Status,
                             Statuses = equip.Status.ToString(),
                             EquipmentTypeDescription = type.Description,
                             EquipmentTypeName = type.Name,
                             Quantity = equip.Quantity,
                             IsDeleted = equip.IsDeleted
                         }).ToList().FirstOrDefault();

            return View(model);
        }
        //create new equipment
        public IActionResult Create()
        {
            List<EquipmentType> equipmentTypes = _equipmentTypeSvc.GetAll();

            ViewBag.EquipmentType = new SelectList(equipmentTypes, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EquipmentDTO equip)
        {
            if (ModelState.IsValid)
            {
               await _equipSvc.Add(equip);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Equipments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = await _context.Equipment.FindAsync(id);
            if (equipment == null)
            {
                return NotFound();
            }
            return View(equipment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EquipmentDTO equipment)
        {
            if (id != equipment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _equipSvc.EditEquipment(id, equipment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipmentExists(equipment.Id))
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
            return View(equipment);
        }

        // GET: Equipments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = await _context.Equipment
                .FirstOrDefaultAsync(m => m.Id == id);
            if (equipment == null)
            {
                return NotFound();
            }

            return View(equipment);
        }

        // POST: Equipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var equipment = await _context.Equipment.FindAsync(id);
            equipment.IsDeleted = true;
            equipment.DeletionDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EquipmentExists(int id)
        {
            return _context.Equipment.Any(e => e.Id == id);
        }
    }
}
