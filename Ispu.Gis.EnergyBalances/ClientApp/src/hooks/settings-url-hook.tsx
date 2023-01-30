import {useState} from "react";
import {IUrlsService, UrlsService} from "../services/settings-urls-service";

export const useServerUrlsService = ()=>{
    const[service] = useState<IUrlsService>(new UrlsService())
    return service;
}