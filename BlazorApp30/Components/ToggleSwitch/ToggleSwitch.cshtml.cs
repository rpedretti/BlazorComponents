using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor;
using System;

namespace BlazorApp30.Components.ToggleSwitch
{
    public class ToggleSwitchBase : BaseComponent
    {
        [Parameter] protected string Label { get; set; }
        [Parameter] protected bool Checked { get; set; }
        [Parameter] protected bool Inline { get; set; }
        [Parameter] protected SwitchSize Size { get; set; } = SwitchSize.MEDIUM;
        [Parameter] protected Action<bool> CheckedChanged { get; set; }
        [Parameter] protected bool Round { get; set; }
        [Parameter] protected bool Fill { get; set; }

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
    }

    public enum SwitchSize
    {
        SMALL,
        MEDIUM,
        LARGE
    }
}
