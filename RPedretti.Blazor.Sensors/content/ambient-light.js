window.rpedrettiBlazorSensors = window.rpedrettiBlazorSensors || {};
window.rpedrettiBlazorSensors.lightsensor = {
    initSensor: function (sensorRef) {
        if (window.AmbientLightSensor) {
            const sensor = new AmbientLightSensor();
            sensor.onreading = () => {
                sensorRef.invokeMethodAsync('NotifyReading', sensor.illuminance);
            };
            sensor.onerror = (event) => {
                sensorRef.invokeMethodAsync('NotifyError', event.error.message);
            };
            sensor.start();
        } else {
            sensorRef.invokeMethodAsync('NotifyError', 'not supported');
        }
    }
};