using System.Threading.Tasks;

namespace RPedretti.Blazor.BingMaps.Modules
{
    public interface IBingMapModule
    {
        #region Methods

        Task InitAsync(string mapId);

        #endregion Methods
    }
}
