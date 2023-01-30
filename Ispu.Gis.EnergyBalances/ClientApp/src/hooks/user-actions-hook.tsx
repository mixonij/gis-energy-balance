import {HubConnection} from "@microsoft/signalr";
import {useCallback, useEffect, useState} from "react";
import {UserAction} from "../models/user-action";

export const useUserActions = (hubConnection: HubConnection) => {
    const [actions, setActions] = useState<UserAction[]>([]);
    const registerConnection = useCallback(async () => {
        hubConnection.on("UserExecutedAction", (action: any) => {
            setActions(current => {
                let elements = [...current];

                if (elements?.length === 10) {
                    elements.pop();
                    elements.push(action);
                } else {
                    elements.push(action);
                }

                return elements;
            })
        });
    }, [actions]);

    useEffect(() => {
        registerConnection().catch(console.error);
    }, []);
    
    return [actions] as const;
}