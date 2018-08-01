using System.Collections.Generic;
using System.Threading.Tasks;

namespace RPedretti.Blazor.Components.SuggestBox
{
    public class SuggestBoxList
    {
        #region Fields

        private static Dictionary<string, SuggestBoxBase> _suggestionsBase = new Dictionary<string, SuggestBoxBase>();

        #endregion Fields

        #region Methods

        public static void Add(SuggestBoxBase suggestBox)
        {
            _suggestionsBase[suggestBox.SuggestBoxId] = suggestBox;
        }

        public static Task ClearSelection(string id)
        {
            if (_suggestionsBase.TryGetValue(id, out SuggestBoxBase suggest))
            {
                suggest.ClearSelection();
            }

            return Task.CompletedTask;
        }

        public static void Remove(SuggestBoxBase suggestBox)
        {
            _suggestionsBase.Remove(suggestBox.SuggestBoxId);
        }

        #endregion Methods
    }
}
