using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBLL.Models
{
    public class Author
    {
        public Guid ID { get; set; }
        public string Name { get; set; }

        public Author(Guid id, string name)
        {
            ID = id; 
            Name = name;
        }
    }
}
