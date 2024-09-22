using Homework_8.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_8.ViewModels
{
    public class ItemView : IShow<int>
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
}
