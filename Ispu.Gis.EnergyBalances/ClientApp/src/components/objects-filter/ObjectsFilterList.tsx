import React, {Dispatch, useEffect, useState} from "react";
import {Dropdown} from "primereact/dropdown";
import {ObjectFilterOption, ObjectFilterOptions} from "./objects-options";
import {HeatingStation, Point} from "../../app/@shared/g";
import {Card} from "primereact/card";
import {LatLngExpression} from "leaflet";

export interface ObjectsFilterListProps {
    heatingStations: HeatingStation[],
    setMapCenter:  Dispatch<React.SetStateAction<LatLngExpression | undefined>>
}

const ObjectsFilterList = ({heatingStations, setMapCenter}: ObjectsFilterListProps) => {
    const [selectedOption, setSelectedOption] = useState<ObjectFilterOption | null>(null);
    const [items, setItems] = useState<HeatingStation[]>([]);
    
    useEffect(() => {
        if (!selectedOption) {
            return;
        }
        
        if(selectedOption.id === "heatingStation"){
            setItems(heatingStations);
        }

    }, [heatingStations, items, selectedOption])

    // const footer = (
    //     <span>
    //         <Button label="Перейти" icon="pi pi-check" onClick={()=> }/>
    //     </span>
    // );

    return <>
        <div className="card mb-0" style={{height: "90vh"}}>
            <Dropdown style={{width: "100%"}} options={ObjectFilterOptions} value={selectedOption}
                      placeholder="Выберите тип объектов"
                      onChange={(e) => setSelectedOption(e.value)} optionLabel="name" emptyMessage="Опции не найдены"/>

            <div className="mt-4">
                {items.map(item=><Card onClick={()=> {
                    console.log(item.center)
                    setMapCenter([item.center.x, item.center.y])
                }} title={item.name} style={{ width: "100%", marginBottom: '2em' }}>

                </Card>)}
            </div>
        </div>
    </>
}

export default ObjectsFilterList;