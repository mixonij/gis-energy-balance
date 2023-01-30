import React, {MouseEventHandler, useState} from 'react';
import {NavLink} from 'react-router-dom';
import {CSSTransition} from 'react-transition-group';
import classNames from 'classnames';
import {Ripple} from "primereact/ripple";
import {Badge} from 'primereact/badge';
import {Button} from "primereact/button";

const AppSubmenu = (props: any) => {

    const [activeIndex, setActiveIndex] = useState(null)

    const onMenuItemClick = (event: any, item: any, index: any) => {
        if (item.disabled) {
            event.preventDefault();
            return true;
        }

        if (item.command) {
            item.command({originalEvent: event, item: item});
        }

        if (index === activeIndex)
            setActiveIndex(null);
        else
            setActiveIndex(index);

        if (props.onMenuItemClick) {
            props.onMenuItemClick({
                originalEvent: event,
                item: item
            });
        }
    }

    const onKeyDown = (event: any) => {
        if (event.code === 'Enter' || event.code === 'Space') {
            event.preventDefault();
            event.target.click();
        }
    }

    const renderLinkContent = (item: any) => {
        let submenuIcon = item.items && <i className="pi pi-fw pi-angle-down menuitem-toggle-icon"></i>;
        let badge = item.badge && <Badge value={item.badge}/>

        return (
            <React.Fragment>
                <i className={item.icon}></i>
                <span>{item.label}</span>
                {submenuIcon}
                {badge}
                <Ripple/>
            </React.Fragment>
        );
    }

    const renderLink = (item: any, i: any) => {
        let content = renderLinkContent(item);

        if (item.to) {
            return (
                <NavLink aria-label={item.label} onKeyDown={onKeyDown} role="menuitem" className="p-ripple" to={item.to}
                         onClick={(e) => onMenuItemClick(e, item, i)} target={item.target}>
                    {content}
                </NavLink>
            )
        } else {
            return (
                <a tabIndex={0} aria-label={item.label} onKeyDown={onKeyDown} role="menuitem" href={item.url}
                   className="p-ripple" onClick={(e) => onMenuItemClick(e, item, i)} target={item.target}>
                    {content}
                </a>
            );
        }
    }

    let items = props.items && props.items.map((item: any, i: any) => {
        let active = activeIndex === i;
        let styleClass = classNames(item.badgeStyleClass, {
            'layout-menuitem-category': props.root,
            'active-menuitem': active && !item.to
        });

        if (props.root) {
            return (
                <li className={styleClass} key={i} role="none">
                    {props.root === true && <React.Fragment>
                        <div className="layout-menuitem-root-text" aria-label={item.label}>{item.label}</div>
                        <AppSubmenu items={item.items} onMenuItemClick={props.onMenuItemClick}/>
                    </React.Fragment>}
                </li>
            );
        } else {
            return (
                <li className={styleClass} key={i} role="none">
                    {renderLink(item, i)}
                    <CSSTransition classNames="layout-submenu-wrapper" timeout={{enter: 1000, exit: 450}} in={active}
                                   unmountOnExit>
                        <AppSubmenu items={item.items} onMenuItemClick={props.onMenuItemClick}/>
                    </CSSTransition>
                </li>
            );
        }
    });

    return items ? <ul className={props.className} role="menu">{items}</ul> : null;
}

type AppMenuProps = {
    model: any,
    onMenuItemClick?: MouseEventHandler<HTMLButtonElement> | undefined,
    decreaseScale: MouseEventHandler<HTMLButtonElement> | undefined,
    increaseScale: MouseEventHandler<HTMLButtonElement> | undefined,
    scale: number
}

export const AppMenu = (props: AppMenuProps) => {
    return (
        <div>
            <div className="layout-menu-container">
                <AppSubmenu items={props.model} className="layout-menu" onMenuItemClick={props.onMenuItemClick}
                            root={true} role="menu"/>
            </div>
            <div className="mt-3">
                <div className="flex align-items-center justify-content-center">
                    <Button icon="pi pi-minus" className="p-button-text" onClick={props.decreaseScale}/>
                    <span>Масштаб ({props.scale})</span>
                    <Button icon="pi pi-plus" className="p-button-text" onClick={props.increaseScale}/>
                </div>

                <a href="/swagger" className="block mt-3 flex align-items-center justify-content-center">
                    <img alt="swagger" className="w-full" height="40" src={'assets/layout/images/swagger.svg'}/>
                </a>
            </div>
        </div>
    );
}
