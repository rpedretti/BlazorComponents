using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPedretti.Blazor.Components.SuggestBox
{
    public class SuggestBoxBase : BaseComponent
    {
        private bool _suggestionSelected;

        protected string internalQuery;
        [Parameter] protected string Query
        {
            get => internalQuery;
            set => SetParameter(ref internalQuery, value, () => { QueryChanged?.Invoke(value); _suggestionSelected = false; });
        }
        [Parameter] protected Func<string, Task> QueryChanged { get; set; }
        [Parameter] protected Action<string> SuggestionSelected { get; set; }
        [Parameter] protected bool LoadingSuggestion { get; set; }
        [Parameter] protected int MaxSuggestions { get; set; }

        protected List<string> internalSuggestions;
        [Parameter]
        protected List<string> Suggestions
        {
            get
            {
                if (MaxSuggestions > 0)
                {
                    return internalSuggestions?.Take(MaxSuggestions).ToList();
                }
                else
                {
                    return internalSuggestions;
                }
            }
            set
            {
                internalSuggestions = value;
                StateHasChanged();
            }
        }

        protected void InternalSuggestionSelected(string query)
        {
            _suggestionSelected = true;
            internalQuery = query;
            SuggestionSelected?.Invoke(query);
            StateHasChanged();
        }

        protected bool HasFocus { get; set; }

        protected bool OpenSuggestion => (internalSuggestions != null || LoadingSuggestion) && HasFocus && !_suggestionSelected;
    }
}
