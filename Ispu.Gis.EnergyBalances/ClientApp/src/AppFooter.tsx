import {FunctionComponent} from "react";

export const AppFooter: FunctionComponent = () => {

    return (
        <a className="layout-footer" href="http://ispu.ru" target="_blank" rel="noreferrer"
           style={{color: 'inherit', textDecoration: 'inherit'}}>
            <span className="font-medium ml-2">ИГЭУ</span>
        </a>
    );
}