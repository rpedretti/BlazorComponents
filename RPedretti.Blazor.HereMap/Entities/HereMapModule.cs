using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPedretti.Blazor.HereMap.Entities
{
    public class HereMapModule
    {
        private readonly string moduleName;
        public static HereMapModule Ui = new HereMapModule("ui");
        public static HereMapModule Behaviours = new HereMapModule("mapevents");

        private HereMapModule() { }

        private HereMapModule(string moduleName)
        {
            this.moduleName = moduleName;
        }

        public override string ToString()
        {
            return moduleName;
        }
    }
}
