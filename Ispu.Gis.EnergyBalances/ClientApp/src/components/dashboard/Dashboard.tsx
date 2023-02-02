import React, {useCallback, useEffect, useState} from 'react';
import {LayersControl, MapContainer, Marker, Popup, TileLayer} from 'react-leaflet';
import {useGeolocated} from "react-geolocated";
import "./style.css"
import {Building, City, GeoDataService, IGeoDataService} from "../../app/@shared/g";
import {TabPanel, TabView} from "primereact/tabview";
import {LatLngBoundsExpression} from "leaflet";
import MapController from "../map/MapController";
import {Dropdown} from "primereact/dropdown";

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
    const [cities, setCities] = useState<Array<City>>([]);
    const [selectedCity, setSelectedCity] = useState<City | null>(null);
    const [selectedBuilding, setSelectedBuilding] = useState<Building | null>(null);
    const [currentZoom, setCurrentZoom] = useState<number>(0);

    const onCityChange = (e: { value: City }) => {
        setSelectedCity(e.value);
    }

    const loadData = useCallback(async () => {
        const data = await geoService.getHouses(selectedCity?.id!);
        setBuildings(data ?? []);
    }, [geoService, coords, selectedCity]);

    const getCities = useCallback(async () => {
        const cities = await geoService.getCities();
        setCities(cities ?? []);
    }, [])

    useEffect(() => {
        getCities();
    }, [loadData]);
    
    useEffect(()=>{
        if(!selectedCity){
            return;
        }
        
        loadData();
    }, [selectedCity])

    return (
        <>
            <div className="card" style={{height: "70vh"}}>
                {!isGeolocationAvailable ? (
                    <div>Браузер не поддерживает геолокацию</div>
                ) : !isGeolocationEnabled ? (
                    <div>Геолокация не включена</div>
                ) : coords ? (
                    <MapContainer
                        center={[coords?.latitude!, coords?.longitude!]}
                        zoom={11}
                        minZoom={selectedCity?.minZoom === undefined || !selectedCity?.minZoom ? 1 : selectedCity.minZoom}
                        scrollWheelZoom={true}>
                        <TileLayer
                            url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                        />
                        <MapController city={selectedCity} setCurrentZoom={setCurrentZoom}/>
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
            <div className="card" style={{height: "30vh"}}>
                <TabView>
                    <TabPanel header="Настройки">
                        <h5>Город</h5>
                        <Dropdown value={selectedCity} options={cities} onChange={onCityChange}
                                  optionLabel="nameRussian" placeholder="Выберите город"/>
                    </TabPanel>
                    <TabPanel header="Информация о здании">
                        <p>{currentZoom}</p>
                    </TabPanel>
                </TabView>
            </div>
        </>
    );
}

export default Dashboard;
