import {useMapEvents} from "react-leaflet";
import React, {Dispatch, SetStateAction, useEffect} from "react";
import {City} from "../../app/@shared/g";
import {LatLngExpression} from "leaflet";

interface MapControllerProps {
    setCurrentZoom: Dispatch<SetStateAction<number>>,
    city: City | null,
    mapCenter: LatLngExpression | undefined
}

const MapController = ({setCurrentZoom, city, mapCenter}: MapControllerProps) => {
    const map = useMapEvents({
        click() {
            map.locate()
        },
        zoomend() {
            setCurrentZoom(map.getZoom());
        }
    })

    useEffect(() => {
        if (!city) {
            map.setMaxBounds([[-90, -180], [90, 180]])
            return;
        }

        if(mapCenter !== undefined){
            map.flyTo(mapCenter);
            setCurrentZoom(20);
        }

        map.setMinZoom(city.minZoom);
        map.fitBounds([[city?.northWestPoint.x!, city?.northWestPoint.y!], [city?.southEastPoint.x!, city?.southEastPoint.y!]]);
        map.setMaxBounds([[city?.northWestPoint.x!, city?.northWestPoint.y!], [city?.southEastPoint.x!, city?.southEastPoint.y!]]);
    }, [city, map, mapCenter])

    return <></>;
};

export default MapController;