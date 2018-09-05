using System;

namespace RPedretti.Blazor.BingMap.Entities.InfoBox
{
    public partial class InfoBox
    {

        private event EventHandler<InfoboxEventArgs> _onClick;
        private event EventHandler<InfoboxEventArgs> _onInfoboxChanged;
        private event EventHandler<InfoboxEventArgs> _onMouseOver;
        private event EventHandler<InfoboxEventArgs> _onMouseUp;
    }
}
