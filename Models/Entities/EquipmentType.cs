using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestEquipment.Models.Entities
{
    public class EquipmentType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletionDate { get; set; }
    }
}
