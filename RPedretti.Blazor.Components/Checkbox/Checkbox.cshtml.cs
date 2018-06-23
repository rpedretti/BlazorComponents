using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor;
using System;

namespace RPedretti.Blazor.Components.Checkbox
{
    public class CheckboxBase : BaseAccessibleComponent
    {
        [Parameter] protected string Label { get; set; }
        [Parameter] protected bool Checked { get; set; }
        [Parameter] protected bool Inline { get; set; }
        [Parameter] protected Action<bool> CheckedChanged { get; set; }
        [Parameter] protected CheckboxSize Size { get; set; } = CheckboxSize.REGULAR;
        [Parameter] protected bool Disabled { get; set; }

        protected string CheckboxId = Guid.NewGuid().ToString().Replace("-", "");

        protected void ToggleCheck()
        {
            if (!Disabled)
            {
                Checked = !Checked;
                CheckedChanged?.Invoke(Checked);
            }
        }
    }
}
