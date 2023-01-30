import classNames from "classnames";
import React, {useState} from "react";

export const useAdaptive = () => {
    const [staticMenuInactive, setStaticMenuInactive] = useState(false);
    const [mobileMenuActive, setMobileMenuActive] = useState(false);
    const [mobileTopbarMenuActive, setMobileTopbarMenuActive] = useState(false);

    let menuClick = false;
    let mobileTopbarMenuClick = false;

    const isDesktop = () => {
        return window.innerWidth >= 992;
    }

    const onWrapperClick = () => {
        if (!menuClick) {
            setMobileMenuActive(false);
        }

        if (!mobileTopbarMenuClick) {
            setMobileTopbarMenuActive(false);
        }

        mobileTopbarMenuClick = false;
        menuClick = false;
    }

    const onToggleMenuClick = (event: React.FormEvent<HTMLButtonElement>) => {
        menuClick = true;

        if (isDesktop()) {
            setStaticMenuInactive((prevState) => !prevState);
        } else {
            setMobileMenuActive((prevState) => !prevState);
        }

        event.preventDefault();
    }

    const onMobileTopbarMenuClick = (event: React.FormEvent<HTMLButtonElement>) => {
        mobileTopbarMenuClick = true;
        setMobileTopbarMenuActive((prevState) => !prevState);
        event.preventDefault();
    }

    const onMobileSubTopbarMenuClick = (event: React.FormEvent<HTMLButtonElement>) => {
        mobileTopbarMenuClick = true;
        event.preventDefault();
    }

    return [staticMenuInactive, mobileMenuActive, mobileTopbarMenuActive, onWrapperClick, onToggleMenuClick, onMobileTopbarMenuClick, onMobileSubTopbarMenuClick] as const;
}