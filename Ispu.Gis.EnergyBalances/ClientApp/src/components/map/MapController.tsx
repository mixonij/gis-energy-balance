import {useMapEvents} from "react-leaflet";
import React, {Dispatch, SetStateAction, useEffect} from "react";
import {City} from "../../app/@shared/g";

interface MapControllerProps {
    setCurrentZoom: Dispatch<SetStateAction<number>>,
    city: City | null
}

const MapController = ({setCurrentZoom, city}: MapControllerProps) => {
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

        map.setMinZoom(city.minZoom);
        map.fitBounds([[city?.northWestBound.x!, city?.northWestBound.y!], [city?.southEastBound.x!, city?.southEastBound.y!]]);
        map.setMaxBounds([[city?.northWestBound.x!, city?.northWestBound.y!], [city?.southEastBound.x!, city?.southEastBound.y!]]);
    }, [city])

    return <></>;
};

export default MapController;