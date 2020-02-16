using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointNSheep.Common.Interactables
{
    public interface IProxyFor<T>
    {
        T Owner { get; }
    }
}
