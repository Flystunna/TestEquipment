using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestEquipment.Models.Enums;

namespace TestEquipment.Models.Entities
{
    public class Equipment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public Status Status { get; set; }
        public int? EquipmentTypeId { get; set; }
        public virtual EquipmentType EquipmentType { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? DeletionDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
