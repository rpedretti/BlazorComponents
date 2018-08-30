window.rpedrettiBlazorComponents = window.rpedrettiBlazorComponents || {};
window.rpedrettiBlazorComponents.bingMaps = Object.assign(window.rpedrettiBlazorComponents.bingMaps || {}, (function () {
    const _maps = new Map();
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
            switch (item.type) {
                case 'bingmappushpin':
                    self.pushpin.add(mapId, item);
                    break;

            }
            console.debug(item);

            return 1;
        },
        updateItem: function (mapId, item) {
            switch (item.type) {
                case 'bingmappushpin':
                    self.pushpin.update(mapId, item);
                    break;
            }

            return 1;
        },
        removeItem: function (mapId, item) {
            switch (item.type) {
                case 'bingmappushpin':
                    self.pushpin.remove(mapId, item);
                    break;
            }

            return 1;
        },
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
            const updateOptions = function (mapId, pushpin) {
                _pushpins.get(mapId).get(pushpin.id).setOptions(pushpin.options);
            };
            const updateLocation = function (mapId, pushpin) {
                _pushpins.get(mapId).get(pushpin.id).setLocation(pushpin.center);
            };
            const clearTarget = function (target, id) {
                const clear = {
                    center: target.getLocation(),
                    id: id
                };

                return clear;
            };
            const addEventHandler = function (pushpinInstance, pushpinId, eventName, pushpinRef, refEventName) {
                const handler = Microsoft.Maps.Events.addHandler(pushpinInstance, eventName, (e) => {
                    var evt = Object.assign({}, e);
                    evt.getX = e.getX();
                    evt.getY = e.getY();
                    delete evt.primitive;
                    delete evt.layer;
                    evt.target = clearTarget(evt.target, pushpinId);
                    evt['targetId'] = pushpinId;
                    console.debug(evt);
                    pushpinRef.invokeMethodAsync(refEventName, evt);
                });
                if (!_handlers.has(pushpinId)) {
                    _handlers.set(pushpinId, {});
                }
                _handlers.get(pushpinId)[eventName] = handler;
            };

            return {
                add: function (mapId, pushpin) {
                    normalizePushpin(pushpin);
                    const map = _maps.get(mapId).map;
                    const pushpinInstance = new Microsoft.Maps.Pushpin(pushpin.center, pushpin.options);

                    for (var { pushpinId, eventName, pushpinRef, refEventName } of _initHandler.get(pushpin.id)) {
                        addEventHandler(pushpinInstance, pushpinId, eventName, pushpinRef, refEventName);
                    }

                    _initHandler.delete(pushpin.id);

                    if (!_pushpins.has(mapId)) {
                        _pushpins.set(mapId, new Map());
                    }

                    _pushpins.get(mapId).set(pushpin.id, pushpinInstance);

                    map.entities.push(pushpinInstance);

                    return 1;
                },
                update: function (mapId, pushpin) {
                    normalizePushpin(pushpin);
                    const originalPushpin = _pushpins.get(mapId).get(pushpin.id);
                    if (!_.isEqual(Object.assign({}, pushpin.center), Object.assign({}, originalPushpin.getLocation()))) {
                        updateLocation(mapId, pushpin);
                    }

                    if (pushpin.options) {
                        updateOptions(mapId, pushpin);
                    }

                    return 1;
                },
                remove: function (mapId, pushpin) {
                    const instance = _pushpins.get(mapId).get(pushpin.id);
                    _maps.get(mapId).map.entities.remove(instance);

                    return 1;
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
        })()
    };

    return self;
})());

function getBingMaps() {
    rpedrettiBlazorComponents.bingMaps.initMaps();
}