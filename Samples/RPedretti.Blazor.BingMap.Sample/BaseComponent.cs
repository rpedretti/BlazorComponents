using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Collections.Generic;

namespace RPedretti.Blazor.BingMap.Sample
{
    public abstract class BaseComponent : BlazorComponent
    {
        #region Methods

        protected void HandleKeyPress(UIKeyboardEventArgs args, Action action)
        {
            if (args.Key == " " || args.Key == "Enter")
            {
                action?.Invoke();
            }
        }

        protected bool SetParameter<T>(ref T prop, T value, Action onChange = null)
        {
            if (EqualityComparer<T>.Default.Equals(prop, value))
            {
                return false;
            }

            prop = value;
            onChange?.Invoke();

            return true;
        }

        #endregion Methods
    }
}
