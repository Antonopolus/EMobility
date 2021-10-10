using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMobilityApp
{
    public class BdeProcessor
    {
        bool errorMode = false;

        public async Task<bool> SendState(string state)
        {
            await Task.Delay(2000);

            return !errorMode;
        }

        internal void ToggleErrorMode()
        {
            errorMode = !errorMode;
        }
    }
}
