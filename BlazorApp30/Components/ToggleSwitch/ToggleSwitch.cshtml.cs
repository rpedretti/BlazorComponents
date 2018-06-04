using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor;
using System;

namespace BlazorApp30.Components.ToggleSwitch
{
    public class ToggleSwitchBase : BlazorComponent
    {
        [Parameter] protected string Label { get; set; }
        [Parameter] protected bool Checked { get; set; }
        [Parameter] protected bool Inline { get; set; }
        [Parameter] protected Action<bool> CheckedChanged { get; set; }
        [Parameter] protected bool Round { get; set; }
        [Parameter] protected bool Fill { get; set; }

        protected void HandleChanged(UIChangeEventArgs a)
        {
            Checked = (bool)a.Value;
            CheckedChanged?.Invoke(Checked);
        }

        protected void HandleKeyPress(UIKeyboardEventArgs args)
        {
            if (args.Key == " " || args.Key == "Enter")
            {
                Checked = !Checked;
                CheckedChanged?.Invoke(Checked);
            }
        }
    }
}
