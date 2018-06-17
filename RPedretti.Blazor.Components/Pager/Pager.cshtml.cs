using Microsoft.AspNetCore.Blazor.Components;
using System;

namespace RPedretti.Blazor.Components.Pager
{
    public class PagerBase : BaseAccessibleComponent
    {
        [Parameter] protected Action<int> OnRequestPage { get; set; }

        private int _maxIndicators = 5;
        [Parameter] protected int MaxIndicators
        {
            get => _maxIndicators;
            set => SetParameter(ref _maxIndicators, value, UpdatePagerCount);
        }

        private int _pageCount;
        [Parameter] protected int PageCount
        {
            get => _pageCount;
            set => SetParameter(ref _pageCount, value, UpdatePagerCount);
        }

        private int _currentPage;
        [Parameter] protected int CurrentPage
        {
            get => _currentPage;
            set => SetParameter(ref _currentPage, value, UpdatePagerCount);
        }

        [Parameter] protected bool Small { get; set; }

        protected Indicator[] Indicators { get; set; }
        protected int IndicatorCount { get; set; }
        protected bool ShowFirst { get; set; } = false;
        protected bool ShowLast { get; set; } = false;
        private int TotalPaginationPages { get; set; }

        private bool _initialized;

        protected override void OnInit()
        {
            IndicatorCount = Math.Min(PageCount, MaxIndicators);
            Indicators = new Indicator[IndicatorCount];
            for (int i = 0; i < IndicatorCount; i++)
            {
                var page = i + 1;
                Indicators[i] = new Indicator
                {
                    Active = CurrentPage == page,
                    Content = page.ToString(),
                    Page = page
                };
            }

            TotalPaginationPages = (int)Math.Ceiling(PageCount / (double)MaxIndicators);
            _initialized = true;
            UpdatePagerCount();
        }

        protected void PreviousPagination()
        {
            var ammount = (CurrentPage - 1) % MaxIndicators;
            OnRequestPage(CurrentPage - MaxIndicators - ammount);
        }

        protected void NextPagination()
        {
            var ammount = (CurrentPage - 1) % MaxIndicators;
            OnRequestPage(CurrentPage + MaxIndicators - ammount);
        }

        protected void PreviousPage()
        {
            OnRequestPage?.Invoke(CurrentPage - 1);
        }

        protected void NextPage()
        {
            OnRequestPage?.Invoke(CurrentPage + 1);
        }

        protected void FirstPage()
        {
            OnRequestPage?.Invoke(1);
        }

        protected void LastPage()
        {
            OnRequestPage?.Invoke(PageCount);
        }

        private void UpdatePagerCount()
        {
            if (_initialized)
            {
                if (PageCount > MaxIndicators)
                {
                    var start = MaxIndicators * ((CurrentPage - 1) / MaxIndicators) + 1;
                    var limit = Math.Min(PageCount - start + 1, MaxIndicators);

                    ShowFirst = CurrentPage > MaxIndicators;
                    ShowLast = Math.Ceiling(CurrentPage / (double)MaxIndicators) < TotalPaginationPages;

                    for (int i = 0; i < limit; i++)
                    {
                        var page = i + start;
                        Indicators[i].Visible = true;
                        Indicators[i].Active = CurrentPage == page;
                        Indicators[i].Content = page.ToString();
                        Indicators[i].Page = page;
                    }

                    if (limit < MaxIndicators)
                    {
                        for (int i = limit; i < MaxIndicators; i++)
                        {
                            Indicators[i].Visible = false;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < IndicatorCount; i++)
                    {
                        Indicators[i].Active = CurrentPage == i + 1;
                    }
                }
            }
        }

        protected class Indicator
        {
            public string Content { get; set; }
            public int Page { get; set; }
            public bool Active { get; set; }
            public bool Visible { get; set; }
        }
    }
}
