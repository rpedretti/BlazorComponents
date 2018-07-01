using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Browser.Interop;
using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPedretti.Blazor.Components.SuggestBox
{
    public class SuggestBoxBase : BaseComponent
    {
        #region Fields

        private bool init = true;
        private bool _loading;
        private string originalQuery;
        protected readonly string directions = "Keyboard users, use up and down arrows to review and enter to select. Touch device users, explore by touch or with swipe gestures.";
        protected List<SuggestionItem> _suggestionItems = new List<SuggestionItem>();
        protected List<string> _suggestions;
        protected string internalQuery;
        protected bool AnnounceA11Y = false;

        #endregion Fields

        #region Properties

        private string _a11ylabel;
        protected string A11yLabel {
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
            set => SetParameter(ref _loading, value, () => UpdateLoadingA11yLabel(value));
        }
        [Parameter] protected int MaxSuggestions { get; set; }
        protected bool OpenSuggestion { get; set; }

        [Parameter]
        protected string Query
        {
            get => internalQuery;
            set
            {
                SetParameter(ref internalQuery, value, () => QueryChanged?.Invoke(value));
                originalQuery = internalQuery;
            }
        }

        [Parameter] protected Func<string, Task> QueryChanged { get; set; }
        protected string SuggestBoxId { get; set; }

        [Parameter]
        protected List<string> Suggestions
        {
            get => _suggestions;
            set
            {
                if (_suggestions != value)
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

                    Console.WriteLine("suggestion");
                    AnnounceA11Y = true;
                    A11yLabel = _suggestionItems?.Count > 0 ? $"{ _suggestionItems.Count } results. { directions }" : "no results";
                }
            }
        }

        [Parameter] protected Action<string> SuggestionSelected { get; set; }

        #endregion Properties

        #region Methods

        private void UpdateLoadingA11yLabel(bool loading)
        {
            if (loading)
            {
                Console.WriteLine("update");
                AnnounceA11Y = true;
                A11yLabel = "Loading";
            }
        }

        protected void OnFocusIn()
        {
            
        }

        protected void HandleKeyDown(UIKeyboardEventArgs args)
        {
            if (_suggestionItems.Any())
            {
                Console.WriteLine($"olar: {args.Key}");
                SuggestionItem newSelected;
                switch (args.Code)
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

                            newSelected.Selected = true;
                            internalQuery = newSelected.Value;
                        }

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
                        }

                        break;
                    case "Tab":
                    case "Escape":
                        _suggestionItems.ForEach(s => s.Selected = false);
                        internalQuery = originalQuery;
                        OpenSuggestion = false;
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
            Console.WriteLine("selected");
            AnnounceA11Y = true;
            A11yLabel = null;
            RegisteredFunction.Invoke<int>("focus", SuggestBoxId);
        }

        protected override void OnInit()
        {
            SuggestBoxId = $"sugestbox-{Guid.NewGuid()}";
            ListId = Guid.NewGuid().ToString();
        }

        protected override void OnAfterRender()
        {
            base.OnAfterRender();
            AnnounceA11Y = false;
        }

        #endregion Methods
    }

    public sealed class SuggestionItem
    {
        #region Properties

        public bool Selected { get; set; }
        public string Value { get; set; }

        #endregion Properties
    }
}
