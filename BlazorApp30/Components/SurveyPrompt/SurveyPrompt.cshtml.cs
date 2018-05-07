using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp30.Components.SurveyPrompt
{
    public class SurveyPromptBase : BlazorComponent
    {
        [Parameter]
        protected string Title { get; set; }
    }
}
