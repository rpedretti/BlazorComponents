using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPedretti.Blazor.BingMaps.Entities.InfoBox
{
    public partial class InfoBox
    {

        private event EventHandler<InfoboxEventArgs> _onClick;
        private event EventHandler<InfoboxEventArgs> _onInfoboxChanged;
        private event EventHandler<InfoboxEventArgs> _onMouseOver;
        private event EventHandler<InfoboxEventArgs> _onMouseUp;
    }
}
