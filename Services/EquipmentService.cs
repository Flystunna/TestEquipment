using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestEquipment.Data;
using TestEquipment.Models.DataTransferObjects;
using TestEquipment.Models.Entities;

namespace TestEquipment.Services
{
    public interface IEquipmentService
    {
        Task Add(EquipmentDTO equip);
        Task EditEquipment(int Id, EquipmentDTO equip);
    }
    public class EquipmentService : IEquipmentService
    {
        private readonly ApplicationDbContext _context;

        public EquipmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(EquipmentDTO equip)
        {
            _context.Equipment.Add(new Equipment
            {
                Name = equip.Name,
                CreationTime = DateTime.Now,
                Quantity = equip.Quantity,
                Status = equip.Status,
                EquipmentTypeId = equip.EquipmentTypeId
            });
            
            await _context.SaveChangesAsync();
        }

        public async Task EditEquipment(int Id, EquipmentDTO equip)
        {
            //get equipment by ID
            var update = _context.Equipment.Find(Id);
            if (update == null)
            {
                return;
            }
            else
            {
                update.Name = equip.Name;
                update.Quantity = equip.Quantity;
                update.Status = equip.Status;
                update.EquipmentTypeId = equip.EquipmentTypeId;
                await _context.SaveChangesAsync();
            }
        }
    }
}
