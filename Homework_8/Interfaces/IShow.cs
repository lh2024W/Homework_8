using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Homework_8.Interfaces
{
    public interface IShow<T> where T : INumber<T>
    {
        T Id { get; set; }
        string Value { get; set; }
    }
}
