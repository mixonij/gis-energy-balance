import React, {useCallback, useEffect, useState} from "react";
import {LocalStorage} from "ts-localstorage";
import {colorModeKey, themeKey} from "../configuration/local-storage-configuration";

const getLightMode = () => {
    return LocalStorage.getItem(colorModeKey)!;
}

const getThemeName = () => {
    return LocalStorage.getItem(themeKey)!;
}

export const useTheme = () =>{
    const [layoutColorMode, setLayoutColorMode] = useState(getLightMode()!)
    const [theme, setTheme] = useState(getThemeName()!);

    const onChangeThemeClick = (event: React.FormEvent<HTMLButtonElement>) => {
        if (layoutColorMode === "light") {
            setLayoutColorMode("dark");
            setTheme("bootstrap4-dark-blue");
        } else {
            setLayoutColorMode("light");
            setTheme("bootstrap4-light-blue");
        }

        event.preventDefault();
    }

    const replaceLink = useCallback((linkElement: HTMLElement, href: string) => {

        const id = linkElement.getAttribute('id');
        const cloneLinkElement = linkElement.cloneNode(true) as HTMLElement;

        cloneLinkElement.setAttribute('href', href);
        cloneLinkElement.setAttribute('id', id + '-clone');

        linkElement.parentNode!.insertBefore(cloneLinkElement, linkElement.nextSibling);

        cloneLinkElement.addEventListener('load', () => {
            linkElement.remove();
            cloneLinkElement.setAttribute('id', id!);
        });
    }, [])

    useEffect(() => {
        LocalStorage.setItem(colorModeKey, layoutColorMode);
        LocalStorage.setItem(themeKey, theme);

        let themeElement = document.getElementById('theme-link');
        const themeHref = 'assets/themes/' + theme + '/theme.css';
        replaceLink(themeElement!, themeHref);

    }, [layoutColorMode, replaceLink, theme])
    
    return [layoutColorMode, onChangeThemeClick] as const;
}