using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_8.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //Связи с другими классами
        public virtual ICollection<Book> Books { get; set; }

        public override string ToString()
        {
            return String.Format("Name - {0}",Name);
        }
    }
}
