using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBox.Domain.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string? IconUrl { get; set; }
        public string? Description { get; set; }   // Kategori açıklaması
        public bool IsActive { get; set; }          // Kategoriyi pasife alabilmek için
        public ICollection<Event> Events { get; set; }
    }
}
