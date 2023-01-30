import {useContext, useState} from "react";
import {ISolutionService, SolutionService} from "../services/solution-service";
import {AuthenticationContext} from "../contexts/authentication-context";

export const useSolutionService = () => {
    const authContext = useContext(AuthenticationContext);

    const [service] = useState<ISolutionService>(new SolutionService(authContext!))
    return service;
}