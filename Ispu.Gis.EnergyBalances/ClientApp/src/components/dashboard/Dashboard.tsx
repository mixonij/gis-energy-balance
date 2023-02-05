import React, {useCallback, useEffect, useState} from 'react';
import {
    Circle, FeatureGroup,
    LayerGroup,
    LayersControl,
    MapContainer,
    Marker,
    Polygon,
    Polyline,
    Popup, Rectangle,
    TileLayer
} from 'react-leaflet';
import {useGeolocated} from "react-geolocated";
import "./style.css"
import {Area, Building, City, GeoDataService, IGeoDataService} from "../../app/@shared/g";
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
    const [areas, setAreas] = useState<Array<Area>>([]);
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

        const area = await geoService.getAreas(selectedCity?.id!)
        setAreas(area ?? []);
    }, [geoService, coords, selectedCity]);

    const getCities = useCallback(async () => {
        const cities = await geoService.getCities();
        setCities(cities ?? []);
    }, [])

    useEffect(() => {
        getCities();
    }, [loadData]);

    useEffect(() => {
        if (!selectedCity) {
            return;
        }

        loadData();
    }, [selectedCity])

    const fillBlueOptions = {fillColor: 'blue'}
    const fillOrangeOptions = {fillColor: 'orange', color: 'red'}
    const redOptions = {color: 'red'}


    return (
        <>
            <div className="card" style={{height: "70vh"}}>
                {!isGeolocationAvailable ? (
                    <div>Браузер не поддерживает геолокацию</div>
                ) : !isGeolocationEnabled ? (
                    <div>Геолокация не включена</div>
                ) : coords ? (
                    <MapContainer
                        attributionControl={false}
                        center={[coords?.latitude!, coords?.longitude!]}
                        zoom={11}
                        minZoom={selectedCity?.minZoom === undefined || !selectedCity?.minZoom ? 1 : selectedCity.minZoom}
                        scrollWheelZoom={true}>
                        <TileLayer
                            url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                        />
                        <LayersControl position="topright">
                            <LayersControl.Overlay checked name="Жилые районы">
                                <LayerGroup>
                                    {areas.map(area =>
                                        <Polygon pathOptions={fillOrangeOptions} key={area.id}
                                                 positions={area.polygonCoordinates.map(x => [x.x, x.y])}/>)
                                    }
                                </LayerGroup>
                            </LayersControl.Overlay>
                            <LayersControl.Overlay checked name="Здания">
                                <LayerGroup>
                                    {buildings.map(building =>
                                        <Polygon pathOptions={fillBlueOptions} key={building.id}
                                                 positions={building.polygonCoordinates.map(x => [x.x, x.y])}
                                                 eventHandlers={{
                                                     click: () => {
                                                         setSelectedBuilding(building);
                                                     }
                                                 }}/>)
                                    }
                                </LayerGroup>
                            </LayersControl.Overlay>
                        </LayersControl>
                        <MapController city={selectedCity} setCurrentZoom={setCurrentZoom}/>
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
                        <p>{selectedBuilding?.polygonCoordinates.toString()}</p>
                    </TabPanel>
                    <TabPanel header="Расчет способа подключения здания">
                        <div className="energyBalanceTable">
                            <table>
                                <tr>
                                    <th>Вид топлива</th>
                                    <th>Вид расхода</th>
                                    <th>Количество натурального топлива м3 или кВт·ч</th>
                                    <th>Потери, у.т</th>
                                    <th>Количество условного топлива, у.т</th>
                                    <th>S1</th>
                                    <th>S2</th>
                                    <th>S3</th>
                                    <th>S4</th>
                                    <th>5</th>
                                    <th>S6</th>
                                    <th>S7</th>
                                    <th>S8</th>
                                    <th>S9</th>
                                </tr>
                            </table>
                        </div>
                    </TabPanel>
                </TabView>
            </div>
        </>
    );
}

export default Dashboard;
