using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMobility.WebApi
{
    public class LogManager
    {
        private readonly EvChargerContext Context;

        public LogManager(EvChargerContext context)
        {
            this.Context = context;
        }
         
        internal void LogMessage(string user, decimal percentagePriceChange)
        {
            // TODO
        }
    }
}
