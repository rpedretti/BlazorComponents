using Microsoft.AspNetCore.Blazor.Components;
using System;

namespace RPedretti.Blazor.Components.Checkbox
{
    public class CheckboxBase : BaseAccessibleComponent
    {
        #region Fields

        protected string CheckboxId = Guid.NewGuid().ToString().Replace("-", "");

        #endregion Fields

        #region Properties

        [Parameter] protected bool Checked { get; set; }
        [Parameter] protected Action<bool> CheckedChanged { get; set; }
        [Parameter] protected bool Disabled { get; set; }
        [Parameter] protected bool Inline { get; set; }
        [Parameter] protected string Label { get; set; }
        [Parameter] protected CheckboxSize Size { get; set; } = CheckboxSize.REGULAR;

        #endregion Properties

        #region Methods

        protected void ToggleCheck()
        {
            if (!Disabled)
            {
                Checked = !Checked;
                CheckedChanged?.Invoke(Checked);
            }
        }

        #endregion Methods
    }
}
