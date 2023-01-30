import {createContext} from "react";
import {IAuthenticationService} from "../services/authentication-service";

export const AuthenticationContext = createContext<IAuthenticationService | null>(null);
