import {AuthenticationService, IAuthenticationService} from "../services/authentication-service";
import {useCallback, useState} from "react";
import {LocalStorage} from "ts-localstorage";
import {userEmailKey, userPasswordHashKey} from "../configuration/local-storage-configuration";
import {AuthenticateRequest} from "../models/authentication";
import {useNavigate} from "react-router-dom";

export const useAuthentication = (location: string) => {
    const [authenticationService] = useState<IAuthenticationService>(new AuthenticationService());

    let navigate = useNavigate();
    const routeChange = useCallback(() => {
        navigate(location);
    }, [location, navigate]);

    const initialAuthentication = useCallback(async () => {
        const email = LocalStorage.getItem(userEmailKey);
        const passwordHash = LocalStorage.getItem(userPasswordHashKey);

        if (!email || !passwordHash) {
            return;
        }

        const authenticationRequest = new AuthenticateRequest();
        authenticationRequest.initFromValues(email, passwordHash)

        await authenticationService.authenticateUser(authenticationRequest).catch(console.error);

        routeChange();
    }, [authenticationService, routeChange])

    return [authenticationService, initialAuthentication] as const;
}