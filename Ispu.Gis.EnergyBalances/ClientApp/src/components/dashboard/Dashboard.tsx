import React, {useCallback, useEffect, useState} from 'react';
import {LayersControl, MapContainer, Marker, Popup, TileLayer} from 'react-leaflet';
import {useGeolocated} from "react-geolocated";
import "./style.css"
import {Building, GeoDataService, IGeoDataService} from "../../app/@shared/g";
import {TabPanel, TabView} from "primereact/tabview";

const Dashboard = (props: any) => {
    const {coords, isGeolocationAvailable, isGeolocationEnabled} =
        useGeolocated({
            positionOptions: {
                enableHighAccuracy: true,
            },
            userDecisionTimeout: 5000,
        });

    const [geoService] = useState<IGeoDataService>(new GeoDataService())
    const [buildings, setBuildings] = useState<Array<Building>>([]);
    const [selectedBuilding, setSelectedBuilding] = useState<Building | null>(null);

    const loadData = useCallback(async () => {
        const data = await geoService.getHouses();
        console.log(data);
        setBuildings(data ?? []);

        console.log(coords);
    }, [geoService, coords]);

    useEffect(() => {
        loadData();
    }, [loadData]);

    return (
        <>
            <div className="card" style={{height: "60vh"}}>
                {!isGeolocationAvailable ? (
                    <div>Your browser does not support Geolocation</div>
                ) : !isGeolocationEnabled ? (
                    <div>Geolocation is not enabled</div>
                ) : coords ? (
                    <MapContainer center={[coords?.latitude!, coords?.longitude!]} zoom={20} scrollWheelZoom={true}>
                        <TileLayer
                            // attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
                            url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                        />
                        {/*<LayersControl position="topright">*/}
                        {/*    <LayersControl.Overlay key="test" checked={true} name="fgfdgdfg">*/}
                        {/*        */}
                        {/*    </LayersControl.Overlay>*/}
                        {/*</LayersControl>*/}
                        {buildings.map(building => <Marker key={building.id}
                                                           position={[building.coordinates.y, building.coordinates.x]}
                                                           eventHandlers={{
                                                               click: () => {
                                                                   setSelectedBuilding(building);
                                                               }
                                                           }
                                                           }/>)
                        }
                    </MapContainer>) : <div></div>}
            </div>
            <div className="card" style={{height: "40vh"}}>
                <TabView>
                    <TabPanel header="Информация о здании">
                        {selectedBuilding?.id}
                    </TabPanel>
                </TabView></div>
        </>
    );
}

export default Dashboard;
