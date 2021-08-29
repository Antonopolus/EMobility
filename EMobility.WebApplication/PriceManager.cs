using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMobility.WebApi
{
    public class PriceManager
    {
        private readonly EvChargerContext Context;

        public PriceManager(EvChargerContext context)
        {
            this.Context = context;
        }

        public void ChangePrices(decimal percPriceChange)
        {
            foreach (var p in Context.Prices)
            {
                p.ProductPrice *= percPriceChange;
            }
        }

    }
}
