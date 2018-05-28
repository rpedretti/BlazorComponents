namespace BlazorApp30.Pages
{
    public class CounterPageBase : BaseBlazorPage
    {
        protected int IncrementAmount = 1;
        protected string CounterTitle { get; set; }
        protected string CounterIncrementDescription { get; set; }

        private int _currentCount;
        public int CurrentCount
        {
            get { return _currentCount; }
            set { SetParameter(ref _currentCount, value); }
        }

        public CounterPageBase()
        {
            CounterTitle = "My counter";
            CounterIncrementDescription = $"Add {IncrementAmount}";
        }
    }
}
