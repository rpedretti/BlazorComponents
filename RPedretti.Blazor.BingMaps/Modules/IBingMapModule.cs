using System.Threading.Tasks;

namespace RPedretti.Blazor.BingMaps.Modules
{
    public interface IBingMapModule
    {
        Task InitAsync(string mapId);
    }
}
