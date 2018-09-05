using System.Threading.Tasks;

namespace RPedretti.Blazor.BingMap.Modules
{
    public interface IBingMapModule
    {
        #region Methods

        Task InitAsync(string mapId);

        #endregion Methods
    }
}
