import {useEffect, useState} from "react";
import {LocalStorage} from "ts-localstorage";
import {scaleKey} from "../configuration/local-storage-configuration";

const getScale = () => {
    return LocalStorage.getItem(scaleKey)!;
}

export const useScale = () =>{
    const [scale, setScale] = useState(getScale());

    useEffect(() => {
        document.documentElement.style.fontSize = scale + 'px';
        LocalStorage.setItem(scaleKey, scale);
    }, [scale])

    const decrementScale = () => {
        setScale((prevState) => --prevState);
    }

    const incrementScale = () => {
        setScale((prevState) => ++prevState);
    }
    
    return [scale,incrementScale, decrementScale] as const;
}

