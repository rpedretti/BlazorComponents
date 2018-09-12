export type DotNetEntityTypes
    = 'bingmappushpin'
    | 'bingmappolyline'
    | 'bingmappolygon';

export interface DotNetEntity {
    id: string;
    type: DotNetEntityTypes;
}

export interface DotNetPushpin extends DotNetEntity {
    options: Microsoft.Maps.IPushpinOptions;
    center: Microsoft.Maps.Location;
}

export interface DotNetPolygon extends DotNetEntity {
    options: Microsoft.Maps.IPolygonOptions;
    coordinates: { latitude: number, longitude: number }[];
    rings: { latitude: number, longitude: number }[][];

}

export interface DotNetPolyline extends DotNetEntity {
    options: Microsoft.Maps.IPolylineOptions;
    coordinates: { latitude: number, longitude: number }[];
}
