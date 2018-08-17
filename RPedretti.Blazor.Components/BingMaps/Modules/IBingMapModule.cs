using System.Threading.Tasks;

namespace RPedretti.Blazor.Components.BingMaps.Modules
{
    public interface IBingMapModule
    {
        Task InitAsync(string mapId);
    }
}
