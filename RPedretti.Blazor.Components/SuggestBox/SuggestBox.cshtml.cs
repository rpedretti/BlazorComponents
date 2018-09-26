using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.JSInterop;
using RPedretti.Blazor.Shared.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPedretti.Blazor.Components.SuggestBox
{
    public class SuggestBoxBase : BaseComponent, IDisposable
    {
        #region Fields

        private string _a11ylabel;
        private bool _loading;
        private bool _shouldRender;
        private bool init = true;
        private string originalQuery;
        private DebounceDispatcher queryDispatcher = new DebounceDispatcher();

        private DotNetObjectRef thisRef;
        protected ElementRef input;

        #endregion Fields

        #region Methods

        private void UpdateLoadingA11yLabel(bool loading)
        {
            if (loading)
            {
                AnnounceA11Y = true;
                A11yLabel = "Loading";
            }
        }

        #endregion Methods

        protected readonly string directions = "Keyboard users, use up and down arrows to review and enter to select. Touch device users, explore by touch or with swipe gestures.";
        protected List<SuggestionItem> _suggestionItems = new List<SuggestionItem>();
        protected List<string> _suggestions;
        protected bool AnnounceA11Y = false;
        protected string internalQuery;

        protected string A11yLabel
        {
            get => _a11ylabel;
            set => SetParameter(ref _a11ylabel, value, () => AnnounceA11Y = true);
        }

        [Parameter] protected string Description { get; set; }
        protected bool HasFocus { get; set; }
        protected string ListId { get; set; }

        [Parameter]
        protected bool LoadingSuggestion
        {
            get => _loading;
            set
            {
                SetParameter(ref _loading, value, () =>
                {
                    UpdateLoadingA11yLabel(value);
                    _shouldRender = true;
                });
            }
        }

        [Parameter] protected int MaxSuggestions { get; set; }
        protected bool OpenSuggestion { get; set; }

        [Parameter]
        protected string Query
        {
            get => internalQuery;
            set
            {
                if (SetParameter(ref internalQuery, value))
                {
                    queryDispatcher.Debounce(1000, (v) => QueryChanged?.Invoke(v as string), value);
                }
                originalQuery = internalQuery;
            }
        }

        [Parameter] protected Func<string, Task> QueryChanged { get; set; }

        [Parameter]
        protected List<string> Suggestions
        {
            get => _suggestions;
            set
            {
                SetParameter(ref _suggestions, value, () =>
                {
                    OpenSuggestion = value?.Count > 0;
                    _suggestions = value;
                    if (_suggestions != null)
                    {
                        _suggestionItems = _suggestions.Select(s => new SuggestionItem
                        {
                            Selected = false,
                            Value = s
                        }).ToList();
                    }

                    AnnounceA11Y = true;
                    A11yLabel = _suggestionItems?.Count > 0 ? $"{ _suggestionItems.Count } results. { directions }" : "no results";
                    _shouldRender = true;
                });
            }
        }

        [Parameter] protected Action<string> SuggestionSelected { get; set; }

        protected void HandleKeyDown(UIKeyboardEventArgs args)
        {
            if (_suggestionItems.Any())
            {
                SuggestionItem newSelected;
                switch (args.Key)
                {
                    case "Enter":
                        if (!OpenSuggestion && _suggestionItems.Any())
                        {
                            OpenSuggestion = true;
                        }
                        else
                        {
                            var selected = _suggestionItems.FirstOrDefault(i => i.Selected);
                            if (selected != null)
                            {
                                InternalSuggestionSelected(selected);
                            }
                        }

                        _shouldRender = true;
                        break;

                    case "ArrowUp":
                        if (OpenSuggestion)
                        {
                            var currentSelectedUp = _suggestionItems.FirstOrDefault(i => i.Selected);
                            if (currentSelectedUp != null)
                            {
                                currentSelectedUp.Selected = false;
                                if (_suggestionItems.First() == currentSelectedUp)
                                {
                                    newSelected = _suggestionItems.Last();
                                }
                                else
                                {
                                    newSelected = _suggestionItems[_suggestionItems.IndexOf(currentSelectedUp) - 1];
                                }
                            }
                            else
                            {
                                newSelected = _suggestionItems.Last();
                            }

                            internalQuery = newSelected.Value;
                            newSelected.Selected = true;
                        }
                        _shouldRender = true;
                        break;

                    case "ArrowDown":
                        if (OpenSuggestion)
                        {
                            var currentSelectedDown = _suggestionItems.FirstOrDefault(i => i.Selected);
                            if (currentSelectedDown != null)
                            {
                                currentSelectedDown.Selected = false;
                                if (_suggestionItems.Last() == currentSelectedDown)
                                {
                                    newSelected = _suggestionItems.First();
                                }
                                else
                                {
                                    var currIndex = _suggestionItems.IndexOf(currentSelectedDown);
                                    newSelected = _suggestionItems[currIndex + 1];
                                }
                            }
                            else
                            {
                                newSelected = _suggestionItems.First();
                            }

                            newSelected.Selected = true;
                            internalQuery = newSelected.Value;
                            _shouldRender = true;
                        }

                        _shouldRender = true;
                        break;

                    case "Tab":
                    case "Escape":
                        ClearSelection();
                        _shouldRender = true;
                        break;
                }
            }
        }

        protected void InternalSuggestionSelected(SuggestionItem item)
        {
            internalQuery = item.Value;
            _suggestionItems.Clear();
            OpenSuggestion = false;
            SuggestionSelected?.Invoke(item.Value);
            AnnounceA11Y = true;
            A11yLabel = null;
            JSRuntime.Current.InvokeAsync<int>("suggestbox.focusById", SuggestBoxId);
            _shouldRender = true;
        }

        protected override void OnAfterRender()
        {
            base.OnAfterRender();
            AnnounceA11Y = false;
        }

        protected override Task OnAfterRenderAsync()
        {
            if (init)
            {
                init = false;
                thisRef = new DotNetObjectRef(this);
                JSRuntime.Current.InvokeAsync<object>("rpedrettiBlazorComponents.suggestbox.initSuggestBox", thisRef, SuggestBoxId);
            }

            return base.OnAfterRenderAsync();
        }

        protected override void OnInit()
        {
            SuggestBoxId = $"suggestbox-{Guid.NewGuid()}";
        }

        protected override bool ShouldRender()
        {
            return _shouldRender;
        }

        internal string SuggestBoxId { get; set; }

        [JSInvokable]
        public Task ClearSelection()
        {
            _suggestionItems.ForEach(s => s.Selected = false);
            internalQuery = originalQuery;
            OpenSuggestion = false;
            StateHasChanged();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            JSRuntime.Current.InvokeAsync<object>("rpedrettiBlazorComponents.suggestbox.unregisterSuggestBox", SuggestBoxId);
            JSRuntime.Current.UntrackObjectRef(thisRef);
        }
    }

    public sealed class SuggestionItem
    {
        #region Properties

        public bool Selected { get; set; }
        public string Value { get; set; }

        #endregion Properties
    }
}
