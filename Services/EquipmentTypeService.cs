using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestEquipment.Data;
using TestEquipment.Models.DataTransferObjects;
using TestEquipment.Models.Entities;

namespace TestEquipment.Services
{
    public interface IEquipmentTypeService
    {
        List<EquipmentType> GetAll();
        Task Add(EquipmentTypeDTO equip);
    }
    public class EquipmentTypeService : IEquipmentTypeService
    {
        private readonly ApplicationDbContext _context;

        public EquipmentTypeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<EquipmentType> GetAll()
        {
            return _context.EquipmentType.ToList();
        }
        public async Task Add(EquipmentTypeDTO equip)
        {
            _context.EquipmentType.Add(new EquipmentType
            {
                Name = equip.Name,
                CreationTime = DateTime.Now,
                IsDeleted = false,
                Description = equip.Description
            });

            await _context.SaveChangesAsync();
        }
    } 
}
