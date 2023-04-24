import {FunctionComponent} from "react";

export const AppFooter: FunctionComponent = () => {

    return (
        <a className="layout-footer" href="http://ispu.ru" target="_blank" rel="noreferrer"
           style={{color: 'inherit', textDecoration: 'inherit'}}>
            <img
                src={'assets/layout/images/logo.svg'}
                alt="Logo" height="40" className="mr-2"/>
            <span className="font-medium ml-2">ИГЭУ</span>
        </a>
    );
}