using Cloudcrate.AspNetCore.Blazor.Browser.Storage;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using RPedretti.Blazor.Components.Debug;
using System;
using System.Collections.Generic;

namespace RPedretti.Blazor.Components
{
    public abstract class BaseComponent : BlazorComponent, IDisposable
    {
        private string renderCounterId;
        [Inject] private SessionStorage SessionStorage { get; set; }

        public BaseComponent()
        {
            renderCounterId = Guid.NewGuid().ToString();
        }

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

        protected int RenderCount { get; set; }

        protected override void OnAfterRender()
        {
            RenderCount++;
#if DEBUG
            var currentCountList = SessionStorage.GetItem<List<ComponentRenderCount>>("render_count") ?? new List<ComponentRenderCount>();
            var dict = new Dictionary<string, int>();
            currentCountList.ForEach(c => dict[c.key] = c.value);

            dict[$"{GetType().Name}_{renderCounterId}"] = RenderCount;
            SessionStorage.SetItem("render_count", dict);
#endif
            base.OnAfterRender();
        }

        public void Dispose()
        {
#if DEBUG
            var currentCountList = SessionStorage.GetItem<List<ComponentRenderCount>>("render_count") ?? new List<ComponentRenderCount>();
            var dict = new Dictionary<string, int>();
            currentCountList.ForEach(c => dict[c.key] = c.value);

            dict.Remove(GetType().Name);
            SessionStorage.SetItem("render_count", dict);
#endif
        }

        #endregion Methods
    }
}
