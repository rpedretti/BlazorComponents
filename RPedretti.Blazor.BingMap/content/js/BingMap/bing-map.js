window.rpedrettiBlazorComponents = window.rpedrettiBlazorComponents || {};
window.rpedrettiBlazorComponents.bingMaps = Object.assign(window.rpedrettiBlazorComponents.bingMaps || {}, (function () {
    const _maps = new Map();
    const _layers = new Map();
    const notInit = [];
    const self = {
        initScript: function (apiKey, mapLanguage) {
            var mapScriptTag = document.createElement('script');
            mapScriptTag.type = 'text/javascript';
            document.head.appendChild(mapScriptTag);
            const language = mapLanguage || window.navigator.userLanguage || window.navigator.language;
            mapScriptTag.src = `https://www.bing.com/api/maps/mapcontrol?callback=getBingMaps&key=${apiKey}&setLang=${language}`;

            return 1;
        },
        initMaps: function () {
            notInit.forEach(({ mapId, mapRef, config }) => {
                const map = Microsoft.Maps.Map(`#${mapId}`, config);
                _maps.set(mapId, { mapRef, map });
                mapRef.invokeMethodAsync('NotifyMapLoaded');

                return 1;
            });
        },
        getMap: function (mapRef, mapId, config) {
            rpedrettiBlazorComponents.helpers.removeEmpty(config);
            try {
                const map = Microsoft.Maps.Map(`#${mapId}`, config);
                _maps.set(mapId, { mapRef, map });
                mapRef.invokeMethodAsync('NotifyMapLoaded');

                return 1;
            } catch (e) {
                notInit.push({ mapId, mapRef, config });

                return 0;
            }
        },
        unloadMap: function (mapID) {
            const map = _maps.get(mapID).map;
            map.entities.clear();
            map.layers.clear();
            map.dispose();
            _maps.delete(mapID);

            return 1;
        },
        updateView: function (mapId, config) {
            rpedrettiBlazorComponents.helpers.removeEmpty(config);
            _maps.get(mapId).map.setView(config);

            return 1;
        },
        loadModule(mapId, moduleId, funcName, funcParams) {
            const map = _maps.get(mapId).map;
            if (funcName !== null) {
                const func = rpedrettiBlazorComponents.helpers.genericFunction(funcName);
                Microsoft.Maps.loadModule(moduleId, func.bind(Object.assign({}, funcParams, { map })));
            } else {
                Microsoft.Maps.loadModule(moduleId, () => { });
            }

            return 1;
        },
        addItem: function (mapId, item) {
            let instance;
            switch (item.type) {
                case 'bingmappushpin':
                    instance = self.pushpin.buildInstance(item);
                    break;
                case 'bingmappolyline':
                    instance = self.polyline.buildInstance(item);
                    break;
            }

            if (instance) {
                _maps.get(mapId).map.entities.push(instance);
            }

            return 1;
        },
        updateItem: function (mapId, item) {
            switch (item.type) {
                case 'bingmappushpin':
                    self.pushpin.update(item);
                    break;
                case 'bingmappolyline':
                    self.polyline.update(item);
                    break;
            }

            return 1;
        },
        removeItem: function (mapId, item) {
            let instance;
            switch (item.type) {
                case 'bingmappushpin':
                    instance = self.pushpin.remove(item.id);
                    break;
                case 'bingmappolyline':
                    instance = self.polyline.remove(item.id);
                    break;
            }

            _maps.get(mapId).map.entities.remove(instance);

            return 1;
        },
        addLayer: function (mapId, id) {
            _maps.get(mapId).map.layers.insert(_layers.get(id).layerInstance);
        },
        removeLayer: function (mapId, id) {
            _maps.get(mapId).map.layers.remove(_layers.get(id).layerInstance);
        },
        polyline: (function () {
            const _polylines = new Map();
            const _handlers = new Map();
            const _initHandler = new Map();
            const normalizePolyline = function (polyline) {
                rpedrettiBlazorComponents.helpers.removeEmpty(polyline);
                const options = polyline.options;
                if (options && options.strokeColor) {
                    const c = options.strokeColor;
                    if (typeof c === 'string') {
                        options.strokeColor = Microsoft.Maps.Color.fromHex(c);
                    } else {
                        options.strokeColor = new Microsoft.Maps.Color(c.a, c.r, c.g, c.b);
                    }
                }
            };
            const updateOptions = function (polyline) {
                _polylines.get(polyline.id).setOptions(polyline.options);
            };
            const updateLocations = function (polyline) {
                const coordinates = polyline.coordinates.map(c => new Microsoft.Maps.Location(c.latitude, c.longitude));
                _polylines.get(polyline.id).setLocations(coordinates);
            };
            const clearEvents = function (polylineId) {
                const handlers = _handlers.get(polylineId) || []
                for (const handler in handlers) {
                    Microsoft.Maps.Events.removeHandler(handlers[handler]);
                }

                return true;
            };
            const addEventHandler = function (polylineInstance, polylineId, eventName, polylineRef, refEventName) {
                const handler = Microsoft.Maps.Events.addHandler(polylineInstance, eventName, (e) => {
                    var evt = Object.assign({}, e);
                    evt.getX = e.getX();
                    evt.getY = e.getY();
                    delete evt.primitive;
                    delete evt.layer;
                    delete evt.target;
                    polylineRef.invokeMethodAsync(refEventName, evt);
                });
                if (!_handlers.has(polylineId)) {
                    _handlers.set(polylineId, {});
                }
                _handlers.get(polylineId)[eventName] = handler;
            };
            return {
                buildInstance: function (polyline) {
                    normalizePolyline(polyline);
                    const coordinates = polyline.coordinates.map(c => new Microsoft.Maps.Location(c.latitude, c.longitude));
                    const polylineInstance = new Microsoft.Maps.Polyline(coordinates, polyline.options);

                    for (var { polylineId, eventName, polylineRef, refEventName } of _initHandler.get(polyline.id) || []) {
                        addEventHandler(polylineInstance, polylineId, eventName, polylineRef, refEventName);
                    }

                    _initHandler.delete(polyline.id);

                    _polylines.set(polyline.id, polylineInstance);

                    return polylineInstance;
                },
                update: function (polyline) {
                    normalizePolyline(polyline);
                    const originalPolyline = _polylines.get(polyline.id);
                    if (!_.isEqual(Object.assign({}, polyline.coordinates), Object.assign({}, originalPolyline.getLocations()))) {
                        updateLocations(polyline);
                    }

                    if (polyline.options) {
                        updateOptions(polyline);
                    }

                    return 1;
                },
                attachEvent: function (polylineId, eventName, polylineRef, refEventName) {
                    const pushpinInstance = _polylines.get(polylineId);
                    if (pushpinInstance) {
                        addEventHandler(pushpinInstance, polylineId, eventName, polylineRef, refEventName);
                    } else {
                        if (!_initHandler.has(polylineId)) {
                            _initHandler.set(polylineId, []);
                        }
                        _initHandler.get(polylineId).push({ polylineId, eventName, polylineRef, refEventName });
                    }

                    return 1;
                },
                remove: function (polylineId) {
                    const instance = _polylines.get(polylineId);
                    clearEvents(polylineId);
                    _polylines.delete(polylineId);
                    return instance;
                }
            };
        })(),
        pushpin: (function () {
            const _pushpins = new Map();
            const _handlers = new Map();
            const _initHandler = new Map();
            const normalizePushpin = function (pushpin) {
                rpedrettiBlazorComponents.helpers.removeEmpty(pushpin);
                const options = pushpin.options;
                if (options && options.color) {
                    const c = options.color;
                    if (typeof c === 'string') {
                        options.color = Microsoft.Maps.Color.fromHex(c);
                    } else {
                        options.color = new Microsoft.Maps.Color(c.a, c.r, c.g, c.b);
                    }
                }
            };
            const updateOptions = function (pushpin) {
                _pushpins.get(pushpin.id).setOptions(pushpin.options);
            };
            const updateLocation = function (pushpin) {
                _pushpins.get(pushpin.id).setLocation(pushpin.center);
            };
            const clearTarget = function (target, id) {
                const clear = {
                    center: target.getLocation(),
                    id: id
                };

                return clear;
            };
            const clearEvents = function (pushpinId) {
                const handlers = _handlers.get(pushpinId) || [];
                for (const handler in handlers) {
                    Microsoft.Maps.Events.removeHandler(handlers[handler]);
                }
            };
            const addEventHandler = function (pushpinInstance, pushpinId, eventName, pushpinRef, refEventName) {
                const handler = Microsoft.Maps.Events.addHandler(pushpinInstance, eventName, (e) => {
                    var evt = Object.assign({}, e);
                    evt.getX = e.getX();
                    evt.getY = e.getY();
                    delete evt.primitive;
                    delete evt.layer;
                    evt.target = clearTarget(evt.target, pushpinId);
                    pushpinRef.invokeMethodAsync(refEventName, evt);
                });
                if (!_handlers.has(pushpinId)) {
                    _handlers.set(pushpinId, {});
                }
                _handlers.get(pushpinId)[eventName] = handler;
            };

            return {
                buildInstance: function (pushpin) {
                    normalizePushpin(pushpin);
                    const pushpinInstance = new Microsoft.Maps.Pushpin(pushpin.center, pushpin.options);

                    for (var { pushpinId, eventName, pushpinRef, refEventName } of _initHandler.get(pushpin.id) || []) {
                        addEventHandler(pushpinInstance, pushpinId, eventName, pushpinRef, refEventName);
                    }

                    _initHandler.delete(pushpin.id);

                    _pushpins.set(pushpin.id, pushpinInstance);

                    return pushpinInstance;
                },
                update: function (pushpin) {
                    normalizePushpin(pushpin);
                    const originalPushpin = _pushpins.get(pushpin.id);
                    if (!_.isEqual(Object.assign({}, pushpin.center), Object.assign({}, originalPushpin.getLocation()))) {
                        updateLocation(pushpin);
                    }

                    if (pushpin.options) {
                        updateOptions(pushpin);
                    }

                    return 1;
                },
                getPropertie: function (id, propName) {
                    return _pushpins.get(id)['get' + propName]();
                },
                getInstance: function (pushpinId) {
                    const instance = _pushpins.get(pushpinId);
                    return instance;
                },
                remove: function (pushpinId) {
                    const instance = _pushpins.get(pushpinId);
                    clearEvents(pushpinId);
                    _pushpins.delete(pushpinId);
                    return instance;
                },
                attachEvent: function (pushpinId, eventName, pushpinRef, refEventName) {
                    const pushpinInstance = _pushpins.get(pushpinId);
                    if (pushpinInstance) {
                        addEventHandler(pushpinInstance, pushpinId, eventName, pushpinRef, refEventName);
                    } else {
                        if (!_initHandler.has(pushpinId)) {
                            _initHandler.set(pushpinId, []);
                        }
                        _initHandler.get(pushpinId).push({ pushpinId, eventName, pushpinRef, refEventName });
                    }

                    return 1;
                },
                detachEvent: function (pushpinId, eventName) {
                    const handler = _handlers.get(pushpinId)[eventName];
                    Microsoft.Maps.Events.removeHandler(handler);

                    return 1;
                }
            };
        })(),
        infobox: (function () {
            const _infoboxes = new Map();
            const normalizeOptions = function (options) {
                rpedrettiBlazorComponents.helpers.removeEmpty(options);
                if (options.location) {
                    const l = options.location;
                    options.location = new Microsoft.Maps.Location(l.latitude, l.longitude);
                }
            };
            return {
                register: function (id, center, options) {
                    normalizeOptions(options);
                    const _infobox = new Microsoft.Maps.Infobox(center, options);
                    _infoboxes.set(id, _infobox);

                    return 1;
                },
                get: function (id, propName) {
                    return _infoboxes.get(id)['get' + propName]();
                },
                setHtmlContent: function (id, content) {
                    _infoboxes.get(id).setHtmlContent(content);

                    return 1;
                },
                setLocation: function (id, location) {
                    const msLocation = new Microsoft.Maps.Location(location.latitude, location.longitude);
                    _infoboxes.get(id).setLocation(msLocation);

                    return 1;
                },
                setMap: function (id, mapId) {
                    const mapInstance = _maps.get(mapId).map;
                    _infoboxes.get(id).setMap(mapInstance);

                    return 1;
                },
                setOptions: function (id, options, ignoreDelay = false) {
                    normalizeOptions(options);
                    _infoboxes.get(id).setOptions(options, ignoreDelay);

                    return 1;
                }
            };
        })(),
        layer: (function () {
            const _items = new Map();
            return {
                init: function (id, layerRef) {
                    const layerInstance = new Microsoft.Maps.Layer(id);
                    _layers.set(id, { layerInstance, layerRef });
                },
                AddToMap: function (id, mapId) {
                    _maps.get(mapId).map.layers.insert(_layers.get(id).layerInstance);
                },
                addItem: function (id, item) {
                    if (!_items.has(id)) {
                        _items.set(id, new Map());
                    }
                    let instance;
                    switch (item.type) {
                        case 'bingmappushpin':
                            instance = self.pushpin.buildInstance(item);
                            _items.get(id).set(item.id, instance);
                            break;
                    }

                    _layers.get(id).layerInstance.add(instance);
                },
                removeItem(id, itemId) {
                    const item = _items.get(id).get(itemId);
                    _items.get(id).delete(itemId);
                    _layers.get(id).layerInstance.remove(item);
                }
            };
        })()
    };

    return self;
})());

function getBingMaps() {
    rpedrettiBlazorComponents.bingMaps.initMaps();
}