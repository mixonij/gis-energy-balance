import {Navigate} from "react-router-dom";
import {useContext} from "react";
import {AuthenticationContext} from "../../contexts/authentication-context";

export type ProtectedRouteProps = {
    element: JSX.Element,
    redirectPath?: string | null
};

export const PrivateRoute = ({element, redirectPath}: ProtectedRouteProps) => {
    const authContext = useContext(AuthenticationContext);

    if (authContext?.user !== null && authContext?.user?.isNormalUser()) {
        return element;
    } else {
        return <Navigate to={{pathname: "/login"}} state={{redirectPath: redirectPath}}/>;
    }
};