import React from "react";
import "./style.css"
//import {BuildingPowerConnections} from "../../app/@shared/g";

interface ConnectionTableProps {
    selectedEnergyBalance: null
}

const ConnectionTable = ({selectedEnergyBalance}: ConnectionTableProps) => {
    return <>
        {/*<div className="center">*/}
        {/*    <table>*/}
        {/*        <tbody>*/}
        {/*        <tr>*/}
        {/*            <th>Вид топлива</th>*/}
        {/*            <th>Вид расхода</th>*/}
        {/*            <th>Количество натурального топлива м3 или кВт·ч</th>*/}
        {/*            <th>Потери, у.т</th>*/}
        {/*            <th>Количество условного топлива, у.т</th>*/}
        {/*            <th>S1</th>*/}
        {/*            <th>S2</th>*/}
        {/*            <th>S3</th>*/}
        {/*            <th>S4</th>*/}
        {/*            <th>S5</th>*/}
        {/*            <th>S6</th>*/}
        {/*            <th>S7</th>*/}
        {/*            <th>S8</th>*/}
        {/*            <th>S9</th>*/}
        {/*        </tr>*/}
        {/*        </tbody>*/}
        {/*        <tbody>*/}
        {/*        <tr>*/}
        {/*            <td rowSpan={3}>*/}
        {/*                Газ*/}
        {/*            </td>*/}
        {/*            <td>*/}
        {/*                Пищеприготовление*/}
        {/*            </td>*/}
        {/*            <td>*/}
        {/*                {selectedEnergyBalance?.gasCookingConsumption}*/}
        {/*            </td>*/}
        {/*            <td>*/}
        {/*                -*/}
        {/*            </td>*/}
        {/*            <td>*/}
        {/*                {selectedEnergyBalance?.gasCookingConsumptionFuel}*/}
        {/*            </td>*/}
        {/*            <td>*/}
        {/*                X*/}
        {/*            </td>*/}
        {/*            <td>*/}

        {/*            </td>*/}
        {/*            <td>*/}
        {/*                X*/}
        {/*            </td>*/}
        {/*            <td>*/}
        {/*                X*/}
        {/*            </td>*/}
        {/*            <td>*/}

        {/*            </td>*/}
        {/*            <td>*/}

        {/*            </td>*/}
        {/*            <td>*/}

        {/*            </td>*/}
        {/*            <td>*/}

        {/*            </td>*/}
        {/*            <td>*/}
        {/*                X*/}
        {/*            </td>*/}
        {/*        </tr>*/}
        {/*        </tbody>*/}
        {/*        <tbody>*/}
        {/*        <tr>*/}
        {/*            <td>*/}
        {/*                Горячее водоснабжение*/}
        {/*            </td>*/}
        {/*            <td>*/}
        {/*                {selectedEnergyBalance?.gasWaterHeatingConsumptionWithoutBoiler}*/}
        {/*            </td>*/}
        {/*            <td>*/}
        {/*                -*/}
        {/*            </td>*/}
        {/*            <td>*/}
        {/*                {selectedEnergyBalance?.gasWaterHeatingConsumptionWithoutBoilerFuel}*/}
        {/*            </td>*/}
        {/*            <td>*/}

        {/*            </td>*/}
        {/*            <td>*/}
        {/*                X*/}
        {/*            </td>*/}
        {/*            <td>*/}
        {/*                X*/}
        {/*            </td>*/}
        {/*            <td>*/}
        {/*                X*/}
        {/*            </td>*/}
        {/*            <td>*/}
        {/*                X*/}
        {/*            </td>*/}
        {/*            <td>*/}
        {/*                X*/}
        {/*            </td>*/}
        {/*            <td>*/}

        {/*            </td>*/}
        {/*            <td>*/}

        {/*            </td>*/}
        {/*            <td>*/}

        {/*            </td>*/}
        {/*        </tr>*/}
        {/*        </tbody>*/}
        {/*        <tbody>*/}
        {/*        <tr>*/}
        {/*            <td>*/}
        {/*                Отопление*/}
        {/*            </td>*/}
        {/*            <td>*/}
        {/*                {selectedEnergyBalance?.gasHeatingConsumption}*/}
        {/*            </td>*/}
        {/*            <td>*/}
        {/*                -*/}
        {/*            </td>*/}
        {/*            <td>*/}
        {/*                {selectedEnergyBalance?.gasHeatingConsumptionFuel}*/}
        {/*            </td>*/}
        {/*            <td>*/}

        {/*            </td>*/}
        {/*            <td>*/}

        {/*            </td>*/}
        {/*            <td>*/}

        {/*            </td>*/}
        {/*            <td>*/}

        {/*            </td>*/}
        {/*            <td>*/}
        {/*                X*/}
        {/*            </td>*/}
        {/*            <td>*/}
        {/*                X*/}
        {/*            </td>*/}
        {/*            <td>*/}

        {/*            </td>*/}
        {/*            <td>*/}
        {/*                X*/}
        {/*            </td>*/}
        {/*            <td>*/}
        {/*                X*/}
        {/*            </td>*/}
        {/*        </tr>*/}
        {/*        </tbody>*/}
        {/*        <tbody>*/}
        {/*        <tr>*/}
        {/*            <td rowSpan={2}>*/}
        {/*                Электрическая энергия*/}
        {/*            </td>*/}
        {/*            <td>*/}
        {/*                Пищеприготовление*/}
        {/*            </td>*/}
        {/*            <td>*/}
        {/*                {selectedEnergyBalance?.electricityCooking}*/}
        {/*            </td>*/}
        {/*            <td>*/}
        {/*                -*/}
        {/*            </td>*/}
        {/*            <td>*/}
        {/*                {selectedEnergyBalance?.electricityCookingFuel}*/}
        {/*            </td>*/}
        {/*            <td>*/}

        {/*            </td>*/}
        {/*            <td>*/}
        {/*                X*/}
        {/*            </td>*/}
        {/*            <td>*/}

        {/*            </td>*/}
        {/*            <td>*/}

        {/*            </td>*/}
        {/*            <td>*/}
        {/*                X*/}
        {/*            </td>*/}
        {/*            <td>*/}
        {/*                X*/}
        {/*            </td>*/}
        {/*            <td>*/}
        {/*                X*/}
        {/*            </td>*/}
        {/*            <td>*/}
        {/*                X*/}
        {/*            </td>*/}
        {/*            <td>*/}

        {/*            </td>*/}
        {/*        </tr>*/}
        {/*        </tbody>*/}
        {/*        <tbody>*/}
        {/*        <tr>*/}
        {/*            <td>*/}
        {/*                Горячее водоснабжение*/}
        {/*            </td>*/}
        {/*            <td>*/}
        {/*                {selectedEnergyBalance?.electricityWaterHeating}*/}
        {/*            </td>*/}
        {/*            <td>*/}
        {/*                -*/}
        {/*            </td>*/}
        {/*            <td>*/}
        {/*                {selectedEnergyBalance?.electricityWaterHeatingFuel}*/}
        {/*            </td>*/}
        {/*            <td>*/}

        {/*            </td>*/}
        {/*            <td>*/}

        {/*            </td>*/}
        {/*            <td>*/}

        {/*            </td>*/}
        {/*            <td>*/}

        {/*            </td>*/}
        {/*            <td>*/}

        {/*            </td>*/}
        {/*            <td>*/}

        {/*            </td>*/}
        {/*            <td>*/}
        {/*                X*/}
        {/*            </td>*/}
        {/*            <td>*/}
        {/*                X*/}
        {/*            </td>*/}
        {/*            <td>*/}
        {/*                X*/}
        {/*            </td>*/}
        {/*        </tr>*/}
        {/*        </tbody>*/}
        {/*        <tbody>*/}
        {/*        <tr>*/}
        {/*            <td rowSpan={2}>*/}
        {/*                Центральное отопление и ГВС*/}
        {/*            </td>*/}
        {/*            <td>*/}
        {/*                Горячее водоснабжение*/}
        {/*            </td>*/}
        {/*            <td>*/}
        {/*                {selectedEnergyBalance?.centralWater}*/}
        {/*            </td>*/}
        {/*            <td>*/}
        {/*                -*/}
        {/*            </td>*/}
        {/*            <td>*/}
        {/*                {selectedEnergyBalance?.centralWaterFuel}*/}
        {/*            </td>*/}
        {/*            <td>*/}
        {/*                X*/}
        {/*            </td>*/}
        {/*            <td>*/}
        {/*                X*/}
        {/*            </td>*/}
        {/*            <td>*/}

        {/*            </td>*/}
        {/*            <td>*/}

        {/*            </td>*/}
        {/*            <td>*/}

        {/*            </td>*/}
        {/*            <td>*/}

        {/*            </td>*/}
        {/*            <td>*/}

        {/*            </td>*/}
        {/*            <td>*/}

        {/*            </td>*/}
        {/*            <td>*/}

        {/*            </td>*/}
        {/*        </tr>*/}
        {/*        </tbody>*/}
        {/*        <tbody>*/}
        {/*        <tr>*/}
        {/*            <td>*/}
        {/*                Отопление*/}
        {/*            </td>*/}
        {/*            <td>*/}
        {/*                {selectedEnergyBalance?.centralHeating}*/}
        {/*            </td>*/}
        {/*            <td>*/}
        {/*                -*/}
        {/*            </td>*/}
        {/*            <td>*/}
        {/*                {selectedEnergyBalance?.centralHeatingFuel}*/}
        {/*            </td>*/}
        {/*            <td>*/}
        {/*                X*/}
        {/*            </td>*/}
        {/*            <td>*/}
        {/*                X*/}
        {/*            </td>*/}
        {/*            <td>*/}
        {/*                X*/}
        {/*            </td>*/}
        {/*            <td>*/}

        {/*            </td>*/}
        {/*            <td>*/}

        {/*            </td>*/}
        {/*            <td>*/}
        {/*                X*/}
        {/*            </td>*/}
        {/*            <td>*/}
        {/*                X*/}
        {/*            </td>*/}
        {/*            <td>*/}

        {/*            </td>*/}
        {/*            <td>*/}

        {/*            </td>*/}
        {/*        </tr>*/}
        {/*        </tbody>*/}
        {/*        <tbody>*/}
        {/*        <tr>*/}
        {/*            <td colSpan={5}>*/}
        {/*                Общее потребление*/}
        {/*            </td>*/}
        {/*            <td>*/}
        {/*                {selectedEnergyBalance?.s1}*/}
        {/*            </td>*/}
        {/*            <td>*/}
        {/*                {selectedEnergyBalance?.s2}*/}
        {/*            </td>*/}
        {/*            <td>*/}
        {/*                {selectedEnergyBalance?.s3}*/}
        {/*            </td>*/}
        {/*            <td>*/}
        {/*                {selectedEnergyBalance?.s4}*/}
        {/*            </td>*/}
        {/*            <td>*/}
        {/*                {selectedEnergyBalance?.s5}*/}
        {/*            </td>*/}
        {/*            <td>*/}
        {/*                {selectedEnergyBalance?.s6}*/}
        {/*            </td>*/}
        {/*            <td>*/}
        {/*                {selectedEnergyBalance?.s7}*/}
        {/*            </td>*/}
        {/*            <td>*/}
        {/*                {selectedEnergyBalance?.s8}*/}
        {/*            </td>*/}
        {/*            <td>*/}
        {/*                {selectedEnergyBalance?.s9}*/}
        {/*            </td>*/}
        {/*        </tr>*/}
        {/*        </tbody>*/}
        {/*    </table>*/}
        {/*</div>*/}
    </>
}

export default ConnectionTable;