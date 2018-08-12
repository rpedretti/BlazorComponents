listeners = [];

function registerListener(listener) {
    listeners.push(listener);
}

function removeListener(listener) {
    listeners.remove(listener);
}

if ('AmbientLightSensor' in window) {
    const sensor = new AmbientLightSensor();
    sensor.onreading = () => {
        listeners.foreach(l => l.InvokeMethodAsync('CheckLight', sensor.illuminance));
    };
    sensor.onerror = (event) => {
        console.log(event.error.name, event.error.message);
    };
    sensor.start();
} else {
    console.debug('no api');
}

window.addEventListener('devicelight', function (event) {
    console.log(event.value);
});