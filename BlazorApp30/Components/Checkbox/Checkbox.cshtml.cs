using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp30.Components.Checkbox
{
    public class CheckboxBase : BaseComponent
    {
        [Parameter] protected bool Inline { get; set; }
        [Parameter] protected bool Checked { get; set; }
        [Parameter] protected Action<bool> CheckedChanged { get; set; }
        [Parameter] protected string Label { get; set; }
        [Parameter] protected CheckboxSize Size { get; set; } = CheckboxSize.REGULAR;

        protected string CheckboxId = Guid.NewGuid().ToString().Replace("-", "");

        protected void HandleChanged(UIChangeEventArgs a)
        {
            Checked = (bool)a.Value;
            CheckedChanged?.Invoke(Checked);
        }
    }
}
