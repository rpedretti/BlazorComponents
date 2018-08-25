using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace RPedretti.Blazor.BingMaps.Modules
{
    public abstract class BaseBingMapModule : IBingMapModule
    {
        #region Methods

        protected async Task InitModuleAsync(string mapId, string moduleName, string initFuncName = null, object initFuncParams = null)
        {
            await JSRuntime.Current.InvokeAsync<object>("rpedrettiBlazorComponents.bingMaps.loadModule",
                mapId, moduleName, initFuncName, initFuncParams ?? new { });
        }

        public abstract Task InitAsync(string mapId);

        #endregion Methods
    }
}
