using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Modules.Dishes.DTOs
{
    public class DeleteDishResponse
    {
        public int Id { get; set; }
        public bool IsDeleted{ get; set; }
    }
}