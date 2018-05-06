using BlazorApp.Domain;
using FluentMigrator;

namespace BlazorApp.NHContext.Migrations
{
    [Migration(0)]
    public class Migration_V0 : Migration
    {
        public override void Down()
        {
            Create.Table("todo_item")
                .WithColumn(nameof(TodoItem.Id).ToLower()).AsInt64().PrimaryKey()
                .WithColumn(nameof(TodoItem.IsDone).ToLower()).AsBoolean().WithDefaultValue(false)
                .WithColumn(nameof(TodoItem.Title).ToLower()).AsString().NotNullable();


            Create.Table("weather_forecast")
                .WithColumn(nameof(WeatherForecast.Id).ToLower()).AsInt64().PrimaryKey()
                .WithColumn(nameof(WeatherForecast.Date).ToLower()).AsDateTime()
                .WithColumn(nameof(WeatherForecast.Summary).ToLower()).AsString().NotNullable()
                .WithColumn(nameof(WeatherForecast.TemperatureC).ToLower()).AsInt32().NotNullable()
                .WithColumn(nameof(WeatherForecast.TemperatureF).ToLower()).AsInt32().NotNullable();
        }

        public override void Up()
        {
            Delete.Table("todo_item");
            Delete.Table("weather_forecast");
        }
    }
}
