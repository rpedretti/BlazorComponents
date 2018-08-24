window.rpedrettiBlazorSensors = window.rpedrettiBlazorSensors || {};
window.rpedrettiBlazorSensors.geolocation = (function () {
    var clonePosition = function (position) {
        return {
            coords: {
                accuracy: position.coords.accuracy,
                altitude: position.coords.altitude,
                altitudeAccuracy: position.coords.altitudeAccuracy,
                heading: position.coords.heading,
                latitude: position.coords.latitude,
                longitude: position.coords.longitude,
                speed: position.coords.speed
            },
            timestamp: position.timestamp
        };
    };

    var clonePositionError = function (positionError) {
        return {
            code: positionError.code,
            message: positionError.message
        };
    };

    return {
        watchPosition: function (objectRef) {
            if (navigator.geolocation) {
                const id = navigator.geolocation.watchPosition(
                    p => objectRef.invokeMethodAsync('WatchPositionResponse', clonePosition(p)),
                    e => objectRef.invokeMethodAsync('WatchPositionError', clonePositionError(e))
                );

                return id;
            }
        },

        stopWatchPosition: function (watchId) {
            if (navigator.geolocation) {
                navigator.geolocation.clearWatch(watchId);
            }
        }
    };
})();