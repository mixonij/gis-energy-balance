import React, {MouseEventHandler, useCallback, useContext} from 'react';
import {Link, useNavigate} from 'react-router-dom';
import classNames from 'classnames';
import {AuthenticationContext} from "./contexts/authentication-context";

type TopbarProps = {
    layoutColorMode: string,
    mobileTopbarMenuActive: boolean,
    onMobileTopbarMenuClick: MouseEventHandler<HTMLButtonElement> | undefined,
    onMobileSubTopbarMenuClick: MouseEventHandler<HTMLButtonElement> | undefined,
    onToggleMenuClick: MouseEventHandler<HTMLButtonElement> | undefined,
    onChangeThemeClick: MouseEventHandler<HTMLButtonElement> | undefined,
}

export const AppTopbar = (props: TopbarProps) => {
    const authContext = useContext(AuthenticationContext);

    let navigate = useNavigate();
    const routeChange = useCallback(() => {
        let path = "/login";
        navigate(path);
    }, [navigate]);

    return (
        <div className="layout-topbar">
            <Link to="/" className="layout-topbar-logo">
                <img
                    // src={'assets/layout/images/logo.svg'}
                    alt="logo"/>
                <span className="ml-2 text-center">Моделирование энергобалансов</span>
            </Link>

            <button type="button" className="p-link  layout-menu-button layout-topbar-button"
                    onClick={props.onToggleMenuClick}>
                <i className="pi pi-bars"/>
            </button>

            <button type="button" className="p-link layout-topbar-menu-button layout-topbar-button"
                    onClick={props.onMobileTopbarMenuClick}>
                <i className="pi pi-ellipsis-v"/>
            </button>

            <ul className={classNames("layout-topbar-menu lg:flex origin-top", {'layout-topbar-menu-mobile-active': props.mobileTopbarMenuActive})}>
                <li>
                    <button className="p-link layout-topbar-button" onClick={props.onChangeThemeClick}>
                        <i className={props.layoutColorMode === 'light' ? 'pi pi-sun' : 'pi pi-moon'}/>
                        <span>{props.layoutColorMode === 'light' ? 'Светлая тема' : 'Темная тема'}</span>
                    </button>
                </li>
                {
                    authContext?.user &&
                    <>
                        <li>
                            <button className="p-link layout-topbar-button" onClick={props.onMobileSubTopbarMenuClick}>
                                <i className="pi pi-user"/>
                                <span>Профиль</span>
                            </button>
                        </li>
                        <li>
                            <button className="p-link layout-topbar-button" onClick={() => {
                                authContext?.signout();
                                routeChange();
                            }}>
                                <i className="pi pi-sign-out"/>
                                <span>Выйти из профиля</span>
                            </button>
                        </li>
                    </>
                }
            </ul>
        </div>
    );
}
