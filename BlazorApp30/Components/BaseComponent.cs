using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp30.Components
{
    public abstract class BaseComponent : BlazorComponent
    {
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
    }
}
