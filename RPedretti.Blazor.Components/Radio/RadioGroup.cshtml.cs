using Microsoft.AspNetCore.Blazor.Components;
using System;

namespace RPedretti.Blazor.Components.Radio
{
    public class RadioGroupBase : BaseComponent
    {

        [Parameter] protected RadioOrientation Orientation { get; set; } = RadioOrientation.VERTICAL;
        [Parameter] protected RadioButton[] Buttons { get; set; }
        [Parameter] protected RadioButton Selected { get; set; }
        [Parameter] protected Action<RadioButton> SelectedChanged { get; set; }
        [Parameter] protected bool CanDeselect { get; set; }

        protected void SelectButton(RadioButton button)
        {
            if (Selected == button && CanDeselect)
            {
                Selected = null;
            }
            else if (Selected != button)
            {
                Selected = button;
                StateHasChanged();
                SelectedChanged?.Invoke(button);
            }
        }
    }
}
