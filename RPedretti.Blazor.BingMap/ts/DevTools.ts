import { DotNetPushpin, DotNetPolyline, DotNetPolygon } from "./Entities/DotNetEntityTypes";
import { v4 as uuid } from 'uuid';

export class DevTools {
    getPolygons = (
        ammount: number,
        bounds: Microsoft.Maps.LocationRect,
        size: number,
        scaleFactor: number,
        options: Microsoft.Maps.IPolylineOptions,
        addHole: boolean): DotNetPolygon[] => {
        let polygons: Microsoft.Maps.Polygon[];

        const generatedPolygons = Microsoft.Maps.TestDataGenerator.getPolygons(
            ammount,
            new Microsoft.Maps.LocationRect(bounds.center, bounds.width, bounds.height),
            size,
            scaleFactor,
            options,
            addHole);

        if (ammount === 1) {
            polygons = [generatedPolygons as Microsoft.Maps.Polygon];
        } else {
            polygons = generatedPolygons as Microsoft.Maps.Polygon[];
        }

        const serializeablePushpins: DotNetPolygon[] = polygons.map(p => {
            return {
                id: uuid(),
                coordinates: p.getLocations(),
                rings: p.getRings(),
                type: 'bingmappushpin',
                options
            } as DotNetPolygon
        });

        return serializeablePushpins;
    }

    getPolylines = (
        ammount: number,
        bounds: Microsoft.Maps.LocationRect,
        size: number = 1,
        scaleFactor: number = 1,
        options: Microsoft.Maps.IPolylineOptions = {}): DotNetPolyline[] => {
        let polylines: Microsoft.Maps.Polyline[];

        const generatedPolylines = Microsoft.Maps.TestDataGenerator.getPolylines(
            ammount,
            !!bounds ? new Microsoft.Maps.LocationRect(bounds.center, bounds.width, bounds.height) : undefined,
            size,
            scaleFactor,
            options)

        if (ammount === 1) {
            polylines = [generatedPolylines as Microsoft.Maps.Polyline];
        } else {
            polylines = generatedPolylines as Microsoft.Maps.Polyline[];
        }

        const serializeablePushpins: DotNetPolyline[] = polylines.map(p => {
            return {
                id: uuid(),
                coordinates: p.getLocations(),
                type: 'bingmappushpin',
                options
            } as DotNetPolyline
        });

        return serializeablePushpins;
    }

    getPushpins = (ammount: number, bounds: Microsoft.Maps.LocationRect, options: Microsoft.Maps.IPushpinOptions): DotNetPushpin[] => {
        let pushpins: Microsoft.Maps.Pushpin[];
        const generatedPushpins = Microsoft.Maps.TestDataGenerator.getPushpins(ammount, new Microsoft.Maps.LocationRect(bounds.center, bounds.width, bounds.height), options)
        if (ammount === 1) {
            pushpins = [generatedPushpins as Microsoft.Maps.Pushpin];
        } else {
            pushpins = generatedPushpins as Microsoft.Maps.Pushpin[];
        }

        const serializeablePushpins: DotNetPushpin[] = pushpins.map(p => {
            return {
                id: uuid(),
                type: 'bingmappushpin',
                center: p.getLocation(),
                options
            } as DotNetPushpin
        });

        return serializeablePushpins;
    }
}