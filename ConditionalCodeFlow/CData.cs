using System;
using System.Collections.Generic;
using System.Text;

namespace ConditionalCore { 

    public class CData
    {
        public bool condition = false;
        public bool previousNodeExecutedOk = false;

        public CData(bool isExecutedOk) {
            previousNodeExecutedOk = isExecutedOk;
        }
        public CData()
        {
            previousNodeExecutedOk = false;
        }

        public CData ShallowClone() {
            return (CData)this.MemberwiseClone();
        }
    }
}
