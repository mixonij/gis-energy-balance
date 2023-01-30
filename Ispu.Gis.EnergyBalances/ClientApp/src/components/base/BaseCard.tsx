import React, {MutableRefObject} from "react";
import {Toast} from "primereact/toast";

type BaseCardProps = {
    component: JSX.Element | JSX.Element[],
    toast?: MutableRefObject<Toast>,
    header?: string
}

export const BaseCard = ({component, toast, header}: BaseCardProps) => {
    return (<div className="grid p-fluid">
        <div className="col-12">
            <div className="card">
                <Toast ref={toast}/>
                <h3>{header}</h3>
                <div className="m-0">
                    {component}
                </div>
            </div>
        </div>
    </div>)
}