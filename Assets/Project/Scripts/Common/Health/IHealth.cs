using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointNSheep.Common.Health
{
    public delegate void HealthChange(IHealth health, int change);
    public interface IHealth
    {
        int CurrentHealth { get; }
        int MaxHealth { get; }

        event HealthChange HealthChanged;
    }
}
