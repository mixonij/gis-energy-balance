import {useState} from "react";
import {ISettingsService, SettingsService} from "../services/settings-service";

export const useServerSettingsService = () => {
    const [service] = useState<ISettingsService>(new SettingsService())
    return service;
}