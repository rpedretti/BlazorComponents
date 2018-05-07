using BlazorApp30.Models;

namespace BlazorApp30.ViewModel
{
    public class CounterPageViewModel
    {
        public CounterPageModel Model { get; set; }
        public int IncrementAmount = 1;

        public CounterPageViewModel()
        {
            Model = new CounterPageModel
            {
                CounterTitle = "My counter",
                CounterIncrementDescription = $"Add {IncrementAmount}"
            };
        }

        public void IncrementCount()
        {
            Model.CurrentCount += IncrementAmount;
        }
    }
}
