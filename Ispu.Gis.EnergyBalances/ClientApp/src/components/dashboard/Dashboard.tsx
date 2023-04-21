import React, {MutableRefObject, useCallback, useEffect, useRef, useState} from 'react';
import {LayerGroup, LayersControl, MapContainer, Marker, Polygon, Polyline, TileLayer} from 'react-leaflet';
import {useGeolocated} from "react-geolocated";
import "./style.css"
import {
    City,
    CityDistrict,
    GeoDataService,
    IGeoDataService,
    Pipe
} from "../../app/@shared/g";
import MapController from "../map/MapController";
import {TabPanel, TabView} from "primereact/tabview";
import {Dropdown} from "primereact/dropdown";
import {Toolbar} from 'primereact/toolbar';
import {Button} from 'primereact/button';
import {SplitButton} from 'primereact/splitbutton';
import {Toast} from 'primereact/toast';


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
    //const [buildings, setBuildings] = useState<Array<Building>>([]);
    const [areas, setAreas] = useState<Array<CityDistrict>>([]);
    const [cities, setCities] = useState<Array<City>>([]);
    const [pipes, setPipes] = useState<Array<Pipe>>([]);
    //const [heatingStations, setHeatingStations] = useState<Array<HeatingStation>>([]);
    const [selectedCity, setSelectedCity] = useState<City | null>(null);
    //const [selectedBuilding, setSelectedBuilding] = useState<Building | null>(null);
    //const [selectedEnergyBalance, setSelectedEnergyBalance] = useState<BuildingPowerConnections | null>(null);
    //const [selectedBuildingInfo, setSelectedBuildingInfo] = useState<BuildingsInfo | null>(null);
    const [, setCurrentZoom] = useState<number>(0);

    const onCityChange = (e: { value: City }) => {
        setSelectedCity(e.value);
    }

    const loadData = useCallback(async () => {
        // const data = await geoService.getHouses(selectedCity?.id!);
        // setBuildings(data ?? []);

        const area = await geoService.getAreas(selectedCity?.id!)
        setAreas(area ?? []);

        // const pipes = await geoService.getPipeGroups();
        // setPipes(pipes ?? []);

        // const heatingStations = await geoService.getHeatingStations();
        // setHeatingStations(heatingStations ?? []);
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
    }, [getCities, loadData]);

    useEffect(() => {
        if (!selectedCity) {
            return;
        }

        loadData();
    }, [loadData, selectedCity])

    useEffect(() => {
        loadBuildingInfo();
    }, [loadBuildingInfo])

    const fillBlueOptions = {fillColor: 'blue'}
    const fillOrangeOptions = {fillColor: 'orange', color: 'red'}
    const purpleOptions = {fillColor: 'red'}
    const limeOptions = {color: 'lime'}

    const items = [
        {
            label: 'Update',
            icon: 'pi pi-refresh'
        },
        {
            label: 'Delete',
            icon: 'pi pi-times'
        },
        {
            label: 'React Website',
            icon: 'pi pi-external-link',
            command: () => {
                window.location.href = 'https://reactjs.org/'
            }
        },
        {
            label: 'Upload',
            icon: 'pi pi-upload',
            command: () => {
                window.location.hash = "/fileupload"
            }
        }
    ];

    const leftContents = (
        <>
            <Dropdown value={selectedCity} options={cities} onChange={onCityChange}
                      optionLabel="nativeName" placeholder="Выберите город"/>
        </>
    );

    return (
        <div className="grid">
            <Toast ref={toast} position="bottom-right"/>
            <div className="col-12">
                <div className="card mb-0"><Toolbar left={leftContents}/></div>

            </div>
            <div className="col-10">
                <div className="card mb-0" style={{height: "70vh"}}>
                    {!isGeolocationAvailable ? (
                        <div>Браузер не поддерживает геолокацию</div>
                    ) : !isGeolocationEnabled ? (
                        <div>Геолокация не включена</div>
                    ) : coords ? (
                        <MapContainer
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
                                <LayersControl.Overlay checked name="Здания">
                                    {/*<LayerGroup>*/}
                                    {/*    {buildings.map(building =>*/}

                                    {/*        <Polygon*/}
                                    {/*            pathOptions={selectedBuilding?.id == building.id ? purpleOptions : fillBlueOptions}*/}
                                    {/*            key={building.id}*/}
                                    {/*            positions={building.polygonCoordinates.map(x => [x.x, x.y])}*/}
                                    {/*            eventHandlers={{*/}
                                    {/*                click: () => {*/}
                                    {/*                    setSelectedBuilding(building);*/}
                                    {/*                }*/}
                                    {/*            }}>*/}
                                    {/*            ffg*/}
                                    {/*        </Polygon>)*/}
                                    {/*    }*/}
                                    {/*</LayerGroup>*/}
                                </LayersControl.Overlay>
                                <LayersControl.Overlay name="Жилые районы">
                                    <LayerGroup>
                                        {areas.map(area =>
                                            <div style={{zIndex: 1001}}>
                                                <Polygon pathOptions={fillOrangeOptions}
                                                          key={area.id}
                                                          positions={area.geometryPoints.map(x => [x.x, x.y])}/>
                                            </div>
                                        )
                                        }
                                    </LayerGroup>
                                </LayersControl.Overlay>
                                <LayersControl.Overlay checked name="Теплотрассы">
                                    <LayerGroup>
                                        {pipes.map(pipe =>
                                            <div style={{zIndex: 1001}}>
                                                <Polyline pathOptions={limeOptions}
                                                          positions={pipe.points.map(x => [x.y, x.x])}/>

                                            </div>
                                        )
                                        }
                                    </LayerGroup>
                                </LayersControl.Overlay>

                                <LayersControl.Overlay checked name="Теплоподстанции">
                                    {/*<LayerGroup>*/}
                                    {/*    {heatingStations.map(heatingStation =>*/}
                                    {/*        <div style={{zIndex: 1001}}>*/}
                                    {/*            <Marker*/}
                                    {/*                position={[heatingStation.coords.x, heatingStation.coords.y]}/>*/}

                                    {/*        </div>*/}
                                    {/*    )*/}
                                    {/*    }*/}
                                    {/*</LayerGroup>*/}
                                </LayersControl.Overlay>
                            </LayersControl>
                            <MapController city={selectedCity} setCurrentZoom={setCurrentZoom}/>
                        </MapContainer>) : <div></div>}
                </div>
            </div>

            <div className="col-2">
                <div className="card mb-0" style={{height: "70vh"}}></div>
            </div>
            <div className="col-12">
                <div className="card">
                    <TabView>
                        {/*<TabPanel header="Информация о здании">*/}
                        {/*    <p>Год постройки здания - {selectedBuildingInfo?.builtYear}</p>*/}
                        {/*    <p>Число жителей здания - {selectedBuildingInfo?.residentsCount}</p>*/}
                        {/*    <p>Общая жилая площадь здания - {selectedBuildingInfo?.area}</p>*/}
                        {/*</TabPanel>*/}
                        <TabPanel header="Расчет способа подключения здания">
                            {/*<ConnectionTable selectedEnergyBalance={selectedEnergyBalance}/>*/}
                        </TabPanel>
                    </TabView>
                </div>
            </div>
        </div>);
}

export default Dashboard;