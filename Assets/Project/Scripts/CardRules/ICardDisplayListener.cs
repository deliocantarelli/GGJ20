using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGJ20.CardRules
{
    interface ICardDisplayListener
    {
        void OnSelected(CardDisplay cardDisplay);
        bool OnConfirmed(CardDisplay cardDisplay);
    }
}
