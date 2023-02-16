import React, {useCallback, useEffect, useState} from 'react';
import {
    LayerGroup,
    LayersControl,
    MapContainer,
    Polygon,
    TileLayer
} from 'react-leaflet';
import {useGeolocated} from "react-geolocated";
import "./style.css"
import {
    Area,
    Building,
    BuildingsInfo,
    City,
    BuildingPowerConnections,
    GeoDataService,
    IGeoDataService
} from "../../app/@shared/g";
import {TabPanel, TabView} from "primereact/tabview";
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
    const [selectedEnergyBalance, setSelectedEnergyBalance] = useState<BuildingPowerConnections | null>(null);
    const [selectedBuildingInfo, setSelectedBuildingInfo] = useState<BuildingsInfo | null>(null);
    const [, setCurrentZoom] = useState<number>(0);

    const onCityChange = (e: { value: City }) => {
        setSelectedCity(e.value);
    }

    const loadData = useCallback(async () => {
        const data = await geoService.getHouses(selectedCity?.id!);
        setBuildings(data ?? []);

        const area = await geoService.getAreas(selectedCity?.id!)
        setAreas(area ?? []);
    }, [geoService, coords, selectedCity]);

    const loadBuildingInfo = useCallback(async () => {
        if (!selectedBuilding) {
            setSelectedBuildingInfo(null);
        }

        const info = await geoService.getBuildingInfo(selectedBuilding?.id!)
        setSelectedBuildingInfo(info);

        const energyBalance = await geoService.calculateEnergyBalance(selectedBuilding?.id!);
        setSelectedEnergyBalance(energyBalance);
    }, [geoService, selectedBuilding]);

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

    useEffect(() => {
        loadBuildingInfo();
    }, [selectedBuilding])

    const fillBlueOptions = {fillColor: 'blue'}
    const fillOrangeOptions = {fillColor: 'orange', color: 'red'}
    const purpleOptions = {fillColor: 'red'}


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
                            <LayersControl.Overlay checked name="Здания">
                                <LayerGroup>
                                    {buildings.map(building =>
                                        <div style={{zIndex: 1002}}>
                                            <Polygon
                                                pathOptions={selectedBuilding?.id == building.id ? purpleOptions : fillBlueOptions}
                                                key={building.id}
                                                positions={building.polygonCoordinates.map(x => [x.x, x.y])}
                                                eventHandlers={{
                                                    click: () => {
                                                        setSelectedBuilding(building);
                                                    }
                                                }}/></div>)
                                    }
                                </LayerGroup>
                            </LayersControl.Overlay>
                            <LayersControl.Overlay name="Жилые районы">
                                <LayerGroup>
                                    {areas.map(area =>
                                        <div style={{zIndex: 1001}}>
                                            <Polygon pathOptions={fillOrangeOptions}
                                                     key={area.id}
                                                     positions={area.polygonCoordinates.map(x => [x.x, x.y])}/>
                                        </div>
                                    )
                                    }
                                </LayerGroup>
                            </LayersControl.Overlay>
                        </LayersControl>
                        <MapController city={selectedCity} setCurrentZoom={setCurrentZoom}/>
                    </MapContainer>) : <div></div>}
            </div>
            <div className="card" style={{height: "40vh"}}>
                <TabView>
                    <TabPanel header="Настройки">
                        <h5>Город</h5>
                        <Dropdown value={selectedCity} options={cities} onChange={onCityChange}
                                  optionLabel="nameRussian" placeholder="Выберите город"/>
                    </TabPanel>
                    <TabPanel header="Информация о здании">
                        <p>Год постройки здания - {selectedBuildingInfo?.builtYear}</p>
                        <p>Число жителей здания - {selectedBuildingInfo?.residentsCount}</p>
                        <p>Общая жилая площадь здания - {selectedBuildingInfo?.area}</p>
                    </TabPanel>
                    <TabPanel header="Расчет способа подключения здания">
                        <div className="center">
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
                                    <th>S5</th>
                                    <th>S6</th>
                                    <th>S7</th>
                                    <th>S8</th>
                                    <th>S9</th>
                                </tr>
                                <tr>
                                    <td rowSpan={3}>
                                        Газ
                                    </td>
                                    <td>
                                        Пищеприготовление
                                    </td>
                                    <td>
                                        {selectedEnergyBalance?.gasCookingConsumption}
                                    </td>
                                    <td>
                                        -
                                    </td>
                                    <td>
                                        {selectedEnergyBalance?.gasCookingConsumptionFuel}
                                    </td>
                                    <td>
                                        X
                                    </td>
                                    <td>

                                    </td>
                                    <td>
                                        X
                                    </td>
                                    <td>
                                        X
                                    </td>
                                    <td>

                                    </td>
                                    <td>

                                    </td>
                                    <td>

                                    </td>
                                    <td>

                                    </td>
                                    <td>
                                        X
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Горячее водоснабжение
                                    </td>
                                    <td>
                                        {selectedEnergyBalance?.gasWaterHeatingConsumptionWithoutBoiler}
                                    </td>
                                    <td>
                                        -
                                    </td>
                                    <td>
                                        {selectedEnergyBalance?.gasWaterHeatingConsumptionWithoutBoilerFuel}
                                    </td>
                                    <td>

                                    </td>
                                    <td>
                                        X
                                    </td>
                                    <td>
                                        X
                                    </td>
                                    <td>
                                        X
                                    </td>
                                    <td>
                                        X
                                    </td>
                                    <td>
                                        X
                                    </td>
                                    <td>

                                    </td>
                                    <td>

                                    </td>
                                    <td>

                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Отопление
                                    </td>
                                    <td>
                                        {selectedEnergyBalance?.gasHeatingConsumption}
                                    </td>
                                    <td>
                                        -
                                    </td>
                                    <td>
                                        {selectedEnergyBalance?.gasHeatingConsumptionFuel}
                                    </td>
                                    <td>

                                    </td>
                                    <td>

                                    </td>
                                    <td>

                                    </td>
                                    <td>

                                    </td>
                                    <td>
                                        X
                                    </td>
                                    <td>
                                        X
                                    </td>
                                    <td>

                                    </td>
                                    <td>
                                        X
                                    </td>
                                    <td>
                                        X
                                    </td>
                                </tr>
                                <tr>
                                    <td rowSpan={2}>
                                        Электрическая энергия
                                    </td>
                                    <td>
                                        Пищеприготовление
                                    </td>
                                    <td>
                                        {selectedEnergyBalance?.electricityCooking}
                                    </td>
                                    <td>
                                        -
                                    </td>
                                    <td>
                                        {selectedEnergyBalance?.electricityCookingFuel}
                                    </td>
                                    <td>

                                    </td>
                                    <td>
                                        X
                                    </td>
                                    <td>

                                    </td>
                                    <td>

                                    </td>
                                    <td>
                                        X
                                    </td>
                                    <td>
                                        X
                                    </td>
                                    <td>
                                        X
                                    </td>
                                    <td>
                                        X
                                    </td>
                                    <td>

                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Горячее водоснабжение
                                    </td>
                                    <td>
                                        {selectedEnergyBalance?.electricityWaterHeating}
                                    </td>
                                    <td>
                                        -
                                    </td>
                                    <td>
                                        {selectedEnergyBalance?.electricityWaterHeatingFuel}
                                    </td>
                                    <td>

                                    </td>
                                    <td>

                                    </td>
                                    <td>

                                    </td>
                                    <td>

                                    </td>
                                    <td>

                                    </td>
                                    <td>

                                    </td>
                                    <td>
                                        X
                                    </td>
                                    <td>
                                        X
                                    </td>
                                    <td>
                                        X
                                    </td>
                                </tr>
                                <tr>
                                    <td rowSpan={2}>
                                        Центральное отопление и ГВС
                                    </td>
                                    <td>
                                        Горячее водоснабжение
                                    </td>
                                    <td>
                                        {selectedEnergyBalance?.centralWater}
                                    </td>
                                    <td>
                                        -
                                    </td>
                                    <td>
                                        {selectedEnergyBalance?.centralWaterFuel}
                                    </td>
                                    <td>
                                        X
                                    </td>
                                    <td>
                                        X
                                    </td>
                                    <td>

                                    </td>
                                    <td>

                                    </td>
                                    <td>

                                    </td>
                                    <td>

                                    </td>
                                    <td>

                                    </td>
                                    <td>

                                    </td>
                                    <td>

                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Отопление
                                    </td>
                                    <td>
                                        {selectedEnergyBalance?.centralHeating}
                                    </td>
                                    <td>
                                        -
                                    </td>
                                    <td>
                                        {selectedEnergyBalance?.centralHeatingFuel}
                                    </td>
                                    <td>
                                        X
                                    </td>
                                    <td>
                                        X
                                    </td>
                                    <td>
                                        X
                                    </td>
                                    <td>

                                    </td>
                                    <td>

                                    </td>
                                    <td>
                                        X
                                    </td>
                                    <td>
                                        X
                                    </td>
                                    <td>

                                    </td>
                                    <td>

                                    </td>
                                </tr>
                                <tr>
                                    <td colSpan={5}>
                                        Общее потребление
                                    </td>
                                    <td>
                                        {selectedEnergyBalance?.s1}
                                    </td>
                                    <td>
                                        {selectedEnergyBalance?.s2}
                                    </td>
                                    <td>
                                        {selectedEnergyBalance?.s3}
                                    </td>
                                    <td>
                                        {selectedEnergyBalance?.s4}
                                    </td>
                                    <td>
                                        {selectedEnergyBalance?.s5}
                                    </td>
                                    <td>
                                        {selectedEnergyBalance?.s6}
                                    </td>
                                    <td>
                                        {selectedEnergyBalance?.s7}
                                    </td>
                                    <td>
                                        {selectedEnergyBalance?.s8}
                                    </td>
                                    <td>
                                        {selectedEnergyBalance?.s9}
                                    </td>
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