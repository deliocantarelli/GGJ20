using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointNSheep.Mend.Battle
{
    interface ICardDisplayListener
    {
        void OnSelected(CardDisplay cardDisplay);
        bool OnConfirmed(CardDisplay cardDisplay);
    }
}
