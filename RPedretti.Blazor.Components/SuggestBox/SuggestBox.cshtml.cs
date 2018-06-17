using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPedretti.Blazor.Components.SuggestBox
{
    public class SuggestBoxBase : BaseComponent
    {
        private string _query;
        [Parameter] protected string Query
        {
            get => _query;
            set => SetParameter(ref _query, value, () => QueryChanged?.Invoke(value));
        }
        [Parameter] protected Func<string, Task> QueryChanged { get; set; }
        [Parameter] protected Action<string> SuggestionSelected { get; set; }
        [Parameter] protected bool LoadingSuggestion { get; set; }
        [Parameter] protected int MaxSuggestions { get; set; }

        protected List<string> internalSuggestions;
        [Parameter] protected List<string> Suggestions
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
            SuggestionSelected?.Invoke(query);
        }

        protected bool HasFocus { get; set; }

        protected bool OpenSuggestion => (internalSuggestions != null || LoadingSuggestion) && HasFocus;
    }
}
