using Microsoft.AspNetCore.Blazor.Components;

namespace BlazorApp40.Components.SurveyPrompt
{
    public class SurveyPromptBase : BlazorComponent
    {
        #region Properties

        [Parameter] protected string Title { get; set; }

        #endregion Properties
    }
}
