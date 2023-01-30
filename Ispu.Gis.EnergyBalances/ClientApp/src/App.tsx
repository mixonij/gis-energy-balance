import {
    Routes,
    Route, useLocation
} from "react-router-dom";
import classNames from 'classnames';
import 'primereact/resources/primereact.css';
import 'primeicons/primeicons.css';
import 'primeflex/primeflex.css';
import './assets/layout/layout.scss';
import {AppFooter} from "./AppFooter";
import React, {useEffect} from "react";
import Dashboard from "./components/dashboard/Dashboard";
import {AppTopbar} from "./AppTopbar";
import {AppMenu} from "./AppMenu";
import {CSSTransition} from 'react-transition-group';
import {NotFound} from "./components/not-found/NotFound";
import {AuthenticationContext} from "./contexts/authentication-context";
import {useTheme} from "./hooks/theme-hook";
import {useScale} from "./hooks/scale-hook";
import {useAdaptive} from "./hooks/adaptive-hook";
import {useAuthentication} from "./hooks/authentication-hook";

export const App = () => {
    const location = useLocation();
    const [layoutColorMode, onChangeThemeClick] = useTheme();
    const [scale, incrementScale, decrementScale] = useScale();
    const [staticMenuInactive,
        mobileMenuActive,
        mobileTopbarMenuActive,
        onWrapperClick,
        onToggleMenuClick,
        onMobileTopbarMenuClick,
        onMobileSubTopbarMenuClick] = useAdaptive();
    const [authenticationService, initialAuthentication] = useAuthentication(location.pathname);

    useEffect(() => {
        initialAuthentication().catch(console.error);
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [])


    const wrapperClass = classNames('layout-wrapper', {
        'layout-overlay': false,
        'layout-static': true,
        'layout-static-sidebar-inactive': staticMenuInactive,
        'layout-overlay-sidebar-active': false,
        'layout-mobile-sidebar-active': mobileMenuActive,
        'p-input-filled': false,
        'layout-theme-light': layoutColorMode === 'light'
    });


    const menu = [
        {
            label: 'Главное',
            items: [{
                label: 'Карта', icon: 'pi pi-fw pi-map', to: '/'
            }]
        }
    ];


    return (
        <AuthenticationContext.Provider value={authenticationService}>
            <div className={wrapperClass} onClick={onWrapperClick}>
                <AppTopbar onToggleMenuClick={onToggleMenuClick} layoutColorMode={layoutColorMode}
                           onChangeThemeClick={onChangeThemeClick}
                           mobileTopbarMenuActive={mobileTopbarMenuActive}
                           onMobileTopbarMenuClick={onMobileTopbarMenuClick}
                           onMobileSubTopbarMenuClick={onMobileSubTopbarMenuClick}/>

                <div className="layout-sidebar">
                    <AppMenu model={menu} decreaseScale={decrementScale} increaseScale={incrementScale}
                             scale={scale}/>
                </div>

                <div className="layout-main-container">
                    <div className="layout-main">
                        <Routes>
                            <Route path="/"
                                   element={<Dashboard colorMode={layoutColorMode}/>}/>
                            <Route path="*" element={<NotFound/>}/>
                        </Routes>
                    </div>

                    <AppFooter/>
                </div>

                <CSSTransition classNames="layout-mask" timeout={{enter: 200, exit: 200}} in={mobileMenuActive}
                               unmountOnExit>
                    <div className="layout-mask p-component-overlay"></div>
                </CSSTransition>
            </div>
        </AuthenticationContext.Provider>
    );
}