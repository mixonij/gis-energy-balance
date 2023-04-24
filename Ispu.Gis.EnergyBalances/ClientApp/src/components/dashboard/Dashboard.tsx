import React, {MutableRefObject, useCallback, useEffect, useRef, useState} from 'react';
import {FeatureGroup, LayerGroup, LayersControl, MapContainer, Polygon, Polyline, TileLayer} from 'react-leaflet';
import {useGeolocated} from "react-geolocated";
import "./style.css"
import {
    Building,
    City,
    CityDistrict,
    GeoDataService,
    HeatingPipe,
    HeatingStation,
    IGeoDataService
} from "../../app/@shared/g";
import MapController from "../map/MapController";
import {TabPanel, TabView} from "primereact/tabview";
import {Dropdown} from "primereact/dropdown";
import {Toolbar} from 'primereact/toolbar';
import {Toast} from 'primereact/toast';
import {Badge} from "primereact/badge";
import L from "leaflet";


const Dashboard = (props: any) => {
    const {coords, isGeolocationAvailable, isGeolocationEnabled} =
        useGeolocated({
            positionOptions: {
                enableHighAccuracy: true,
            },
            userDecisionTimeout: 5000,
        });

    const toast = useRef() as MutableRefObject<Toast>;


    const [geoService] = useState<IGeoDataService>(new GeoDataService())
    const [buildings, setBuildings] = useState<Array<Building>>([]);
    const [areas, setAreas] = useState<Array<CityDistrict>>([]);
    const [cities, setCities] = useState<Array<City>>([]);
    const [selectedCity, setSelectedCity] = useState<City | null>(null);
    const [selectedEntity, setSelectedEntity] = useState<CityDistrict | Building | null>(null);
    const [heatingPipes, setHeatingPipes] = useState<Array<HeatingPipe>>([]);
    const [heatingStations, setHeatingStations] = useState<Array<HeatingStation>>([]);

    const [, setCurrentZoom] = useState<number>(0);

    const onCityChange = (e: { value: City }) => {
        setSelectedCity(e.value);
    }

    const loadData = useCallback(async () => {
        const area = await geoService.getAreas(selectedCity?.id!)
        setAreas(area ?? []);

        //const data = await geoService.getHouses(selectedCity?.id!);
        let buildings: Building[] = [];
        if (area) {
            for (const district of area) {
                for (const districtBuilding of district.buildings) {
                    buildings.push(districtBuilding);
                }
            }
        }
        setBuildings(buildings);

        const pipes = await geoService.getPipes();
        setHeatingPipes(pipes ?? []);

        const heatingStations = await geoService.getHeatingStations();
        setHeatingStations(heatingStations ?? []);
    }, [geoService, selectedCity]);

    const loadBuildingInfo = useCallback(async () => {
        // if (!selectedBuilding) {
        //     setSelectedBuildingInfo(null);
        // }

        // const info = await geoService.getBuildingInfo(selectedBuilding?.id!)
        // setSelectedBuildingInfo(info);
        //
        // const energyBalance = await geoService.calculateEnergyBalance(selectedBuilding?.id!);
        // setSelectedEnergyBalance(energyBalance);
    }, []);

    const getCities = useCallback(async () => {
        const cities = await geoService.getCities();
        setCities(cities ?? []);
    }, [geoService])

    useEffect(() => {
        getCities().then(() => toast.current.show({
            severity: 'success',
            summary: 'Операция выполнена',
            detail: 'Список городов успешно загружен',

            life: 3000
        }));
    }, [getCities]);

    useEffect(() => {
        if (!selectedCity) {
            setAreas([]);
        }

        loadData();
    }, [loadData, selectedCity])

    useEffect(() => {
        loadBuildingInfo();
    }, [loadBuildingInfo])

    const fillBlueOptions = {fillColor: 'blue'}
    const fillOrangeOptions = {color: 'red', fillColor: 'orange'}
    const purpleOptions = {fillColor: 'red'}
    const blackOptions = {color: 'black'}

    const limeOptions = {color: 'lime'}

    const leftContents = (
        <>
            <Dropdown className="m-2" value={selectedCity} options={cities} showClear onChange={onCityChange}
                      optionLabel="nativeName" placeholder="Выберите город"/>

            <Badge className="m-2" value={selectedEntity && "buildings" in selectedEntity ? 1 : areas.length}
                   size="large"
                   severity="warning"/>
            <Badge className="m-2"
                   value={selectedEntity && "buildings" in selectedEntity ? selectedEntity.buildings.length : buildings.length}
                   size="large" severity="success"/>
        </>
    );

    return (
        <div className="grid">
            <Toast ref={toast} position="bottom-right"/>
            <div className="col-12">
                <div className="card mb-0"><Toolbar left={leftContents}/></div>

            </div>
            <div className="col-9">
                <div className="card mb-0" style={{height: "90vh"}}>
                    {!isGeolocationAvailable ? (
                        <div>Браузер не поддерживает геолокацию</div>
                    ) : !isGeolocationEnabled ? (
                        <div>Геолокация не включена</div>
                    ) : coords ? (
                        <MapContainer
                            renderer={L.canvas()}
                            attributionControl={true}
                            center={[coords?.latitude!, coords?.longitude!]}
                            zoom={11}
                            minZoom={selectedCity?.minZoom === undefined || !selectedCity?.minZoom ? 1 : selectedCity.minZoom}
                            scrollWheelZoom={true}>
                            <TileLayer
                                attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
                                url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                            />
                            <LayersControl position="topright">
                                <LayersControl.Overlay checked name="Кварталы и здания">
                                    <FeatureGroup>
                                        {areas.map(area =>

                                            <Polygon pathOptions={fillOrangeOptions}
                                                     key={area.id}
                                                     positions={area.geometryPoints.map(x => [x.x, x.y])}
                                                     eventHandlers={{
                                                         click: () => {
                                                             setSelectedEntity(area);

                                                             toast.current.show({
                                                                 severity: 'error',
                                                                 summary: 'Операция выполнена',
                                                                 detail: 'Список городов успешно загружен',

                                                                 life: 3000
                                                             })
                                                         }
                                                     }}/>
                                        )
                                        }

                                        {buildings.map(building =>

                                            <Polygon
                                                pathOptions={fillBlueOptions}
                                                key={building.id}
                                                positions={building.geometryPoints.map(x => [x.x, x.y])}
                                                eventHandlers={{
                                                    click: () => {
                                                        setSelectedEntity(building);

                                                        toast.current.show({
                                                            severity: 'success',
                                                            summary: 'Операция выполнена',
                                                            detail: 'Список городов успешно загружен',

                                                            life: 3000
                                                        })
                                                    }
                                                }}>
                                            </Polygon>)
                                        }
                                    </FeatureGroup>

                                    {/*<LayerGroup>*/}
                                    {/*    */}
                                    {/*</LayerGroup>*/}
                                </LayersControl.Overlay>

                                <LayersControl.Overlay checked name="Теплотрассы">
                                    <LayerGroup>
                                        {heatingPipes.map(pipe =>
                                            <Polyline pathOptions={blackOptions}
                                                      positions={pipe.points.map(x => [x.x, x.y])}/>
                                        )
                                        }
                                    </LayerGroup>
                                </LayersControl.Overlay>

                                <LayersControl.Overlay checked name="Теплоподстанции">
                                    <LayerGroup>
                                        {heatingStations.map(heatingStation =>
                                            <Polygon
                                                pathOptions={limeOptions}
                                                key={heatingStation.id}
                                                positions={heatingStation.points.map(x => [x.x, x.y])}/>
                                        )
                                        }
                                    </LayerGroup>
                                </LayersControl.Overlay>
                            </LayersControl>
                            <MapController city={selectedCity} setCurrentZoom={setCurrentZoom}/>
                        </MapContainer>) : <div></div>}
                </div>
            </div>

            <div className="col-3">
                <div className="card mb-0" style={{height: "90vh"}}>
                    <Dropdown emptyMessage="Опции не найдены"/>
                </div>
            </div>
        </div>);
}

export default Dashboard;