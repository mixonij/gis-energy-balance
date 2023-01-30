import {useState} from "react";
import {ConnectedServersService, IConnectedServersService} from "../services/connected-servers-service";

export const useConnectedServersService = () => {
    const [service] = useState<IConnectedServersService>(new ConnectedServersService())
    return service;
}