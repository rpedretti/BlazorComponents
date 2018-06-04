using System.Drawing;
using BlazorApp30.Components;

namespace BlazorApp30.Pages.Index
{
    public class IndexBase : BaseComponent
    {

        public Color ParentBgColor { get; set; }
        public Color ChildBgColor { get; set; }
        public string ParagraphStyle { get; set; }
        public bool ExpandLoaderAccordeon { get; set; }

        private bool _someChecked;
        protected bool SomeChecked {
            get { return _someChecked; }
            set { SetParameter(ref _someChecked, value, StateHasChanged); }
        }

        private bool _someChecked2;
        protected bool SomeChecked2
        {
            get { return _someChecked2; }
            set { SetParameter(ref _someChecked2, value, StateHasChanged); }
        }

        private bool _someToggled;
        protected bool SomeToggled
        {
            get { return _someToggled; }
            set { SetParameter(ref _someToggled, value, StateHasChanged); }
        }

        private bool _someToggled2;
        protected bool SomeToggled2
        {
            get { return _someToggled2; }
            set { SetParameter(ref _someToggled2, value, StateHasChanged); }
        }

        public IndexBase()
        {
            ParentBgColor = Color.AliceBlue;
            ChildBgColor = Color.FromArgb(220, 111, 25, 111);
            ParagraphStyle = "background: ";
        }
    }
}
