using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using System;

namespace RPedretti.Blazor.Components.ToggleSwitch
{
    public enum SwitchSize
    {
        SMALL,
        MEDIUM,
        LARGE
    }

    public class ToggleSwitchBase : BaseComponent
    {
        #region Properties

        [Parameter] protected bool Checked { get; set; }
        [Parameter] protected Action<bool> CheckedChanged { get; set; }
        [Parameter] protected bool Fill { get; set; }
        [Parameter] protected bool Inline { get; set; }
        [Parameter] protected string Label { get; set; }
        [Parameter] protected bool Round { get; set; }
        [Parameter] protected SwitchSize Size { get; set; } = SwitchSize.MEDIUM;

        #endregion Properties

        #region Methods

        protected void HandleChanged(UIChangeEventArgs a)
        {
            Checked = (bool)a.Value;
            CheckedChanged?.Invoke(Checked);
        }

        protected void ToggleChecked()
        {
            Checked = !Checked;
            CheckedChanged?.Invoke(Checked);
        }

        #endregion Methods
    }
}
