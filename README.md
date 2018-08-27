# BlazorComponents

This project aims to provide reusable components foa Blazor application.

As for now to be able to use these components the CI build nuget repository for Blazor must be configured. Directions [here](https://github.com/aspnet/Blazor#using-ci-builds-of-blazor)

Live samples at

https://blazorcomponents.azurewebsites.net/ for inputs, layouts, spinners, dynamic grid, dynamic (grouped) table and SignlR

https://blazorbingmaps.azurewebsites.net/ for the bing maps component

## Packages

- Inputs: https://www.nuget.org/packages/RPedretti.Blazor.Components
- Layouts: https://www.nuget.org/packages/RPedretti.Blazor.Components.Layout
- Sensors: https://www.nuget.org/packages/RPedretti.Blazor.Sensors
- BingMaps: https://www.nuget.org/packages/RPedretti.Blazor.BingMaps

## Inputs
There are four custom inputs, with acessibility
- Checkbox: [sample](https://github.com/rpedretti/BlazorComponents/blob/ecaa7db9411fbe053adfceea27b46c7b36e7e0e6/Samples/RPedretti.Blazor.Components.Sample/Pages/Inputs/Inputs.cshtml#L22-L36)
- RadioGroup: [sample](https://github.com/rpedretti/BlazorComponents/blob/ecaa7db9411fbe053adfceea27b46c7b36e7e0e6/Samples/RPedretti.Blazor.Components.Sample/Pages/Inputs/Inputs.cshtml#L72-L87)
- SuggestBox: [sample](https://github.com/rpedretti/BlazorComponents/blob/ecaa7db9411fbe053adfceea27b46c7b36e7e0e6/Samples/RPedretti.Blazor.Components.Sample/Pages/Inputs/Inputs.cshtml#L10-L16)
- ToggleSwitch: [sample](https://github.com/rpedretti/BlazorComponents/blob/ecaa7db9411fbe053adfceea27b46c7b36e7e0e6/Samples/RPedretti.Blazor.Components.Sample/Pages/Inputs/Inputs.cshtml#L41-L68)

## Progress
- ProgressBar: [sample](https://github.com/rpedretti/BlazorComponents/blob/ecaa7db9411fbe053adfceea27b46c7b36e7e0e6/Samples/RPedretti.Blazor.Components.Sample/Pages/Loaders/Loaders.cshtml#L8-L13)
- Spinner: [sample](https://github.com/rpedretti/BlazorComponents/blob/ecaa7db9411fbe053adfceea27b46c7b36e7e0e6/Samples/RPedretti.Blazor.Components.Sample/Pages/Loaders/Loaders.cshtml#L14-L31)

## Layout
- Accordeon: There are many in [sample Input](https://github.com/rpedretti/BlazorComponents/blob/master/Samples/RPedretti.Blazor.Components.Sample/Pages/Inputs/Inputs.cshtml) page
- DynamicGroupedTable: [sample](https://github.com/rpedretti/BlazorComponents/blob/ecaa7db9411fbe053adfceea27b46c7b36e7e0e6/Samples/RPedretti.Blazor.Components.Sample/Pages/Forecast/Forecast.cshtml#L18-L20)
- DynamicTable: [sample](https://github.com/rpedretti/BlazorComponents/blob/ecaa7db9411fbe053adfceea27b46c7b36e7e0e6/Samples/RPedretti.Blazor.Components.Sample/Pages/Forecast/Forecast.cshtml#L24-L28)
- PagedGrid: [sample](https://github.com/rpedretti/BlazorComponents/blob/ecaa7db9411fbe053adfceea27b46c7b36e7e0e6/Samples/RPedretti.Blazor.Components.Sample/Pages/Movies/Movies.cshtml#L24-L43)
- Pager: [sample]()

## BingMaps
To use the maps you only have to load the maps script in yoyr app.
There is a extension method that do that. Just have to call it ath the `Startup.Configure` method:

```
public void Configure(IBlazorApplicationBuilder app)
{
    app.AddComponent<App>("app");
    app.UseBingMaps("<your_maps_key>");
}
```

To acquire one key follow the instructions [here](https://msdn.microsoft.com/en-us/library/ff428642.aspx)

The component supports dynamic modue loading. The [sample](https://github.com/rpedretti/BlazorComponents/tree/master/Samples/RPedretti.Blazor.BingMaps.Sample)
cover its features.

## Sensors
### Ambient Light Sensor (experimental)
api docs: https://developer.mozilla.org/en-US/docs/Web/API/AmbientLightSensor.

Only available in Chrome: must enable [chrome://flags/#enable-generic-sensor](chrome://flags/#enable-generic-sensor) and [chrome://flags/#enable-generic-sensor-extra-classes](chrome://flags/#enable-generic-sensor-extra-classes)

To use this sensor just call the `AddAmbientLightSensor()` at the `Startup.ConfigureServices` and
`InitAmbientLightSensor()` at `Startup.Configure`

```
public void ConfigureServices(IServiceCollection services)
{
    ...
    services.AddAmbientLightSensor();
    ...
}

public void Configure(IBlazorApplicationBuilder app)
{
    ...
    app.InitAmbientLightSensor();
    ...
}
```

Then the service will be registered and can be injected anywhere in the application with the `AmbientLightSensor` class.
To get reading subscribe to the event `AmbientLightSensor.OnReading` and for error `AmbientLightSensor.OnError`

### Geolocation

To use this sensor just call the `AddGeolocationSensor()` at the `Startup.ConfigureServices`:

```
public void ConfigureServices(IServiceCollection services)
{
    ...
    services.AddGeolocationSensor();
    ...
}
```

Then the service will be registered and can be injected anywhere in the application with the `GeolocationSensor` class.
To watch position change subscribe to `OnPositionUpdate` and for error `OnPositionError`

## Network

### SignalR
**TBD**

[sample](https://github.com/rpedretti/BlazorComponents/blob/master/Samples/RPedretti.Blazor.Components.Sample/Pages/SignalR/SignalR.cshtml.cs)
