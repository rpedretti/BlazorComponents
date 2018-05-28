using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp30.Components.ToggleSwitch
{
    public class ToggleSwitchBase : BlazorComponent
    {
        [Parameter] protected string Label { get; set; }
        [Parameter] protected bool Checked { get; set; }
        [Parameter] protected bool Inline { get; set; }
        [Parameter] protected Action<bool> CheckedChanged { get; set; }
        [Parameter] protected bool Round { get; set; }

        protected void HandleChanged(UIChangeEventArgs a)
        {
            CheckedChanged?.Invoke((bool)a.Value);
        }
    }
}
