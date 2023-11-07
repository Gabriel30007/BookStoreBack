using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DAL.Entities
{
    public class Author
    {
        [Required]
        public Guid Id { get; set; }

        [Required] 
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
