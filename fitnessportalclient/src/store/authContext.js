import jwtDecode from "jwt-decode";
import React, {useState} from "react";
import { Navigate, useLocation } from "react-router-dom";

const AuthContext = React.createContext({
    tokenJWT: "",
    isUserLogged: false,
    username: "",
    login: (tokenJWT) => {},
    logout: () => {},
});

export default AuthContext;

const retrieveTokenFromStorage = () => {
    const storedToken = localStorage.getItem("healthyhabithubTOKEN");

    return {token: storedToken};
};

export const AuthContextProvider = (props) => {
    const tokenData = retrieveTokenFromStorage();
    const usernameData = localStorage.getItem("healthyhabithubUSERNAME");

    let initialToken;
    let initialUsername;
    if(tokenData){
        initialToken = tokenData.token;
    }

    if(usernameData){
        initialUsername = usernameData;
    }

    const [token, setToken] = useState(initialToken);
    const userIsLoggedIn = !!token;
    const [username, setUsername] = useState(initialUsername);

    const handleLogin = (token) => {
        setToken(token);
        localStorage.setItem("healthyhabithubTOKEN", token);
        try{
            const decodedToken = jwtDecode(token);
            const username = decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'];
            localStorage.setItem("healthyhabithubUSERNAME", username);
            setUsername(username);
            // console.log(decodedToken);
        } catch (error){
            console.log('Error decoding token:', error);
        }
    };

    const handleLogout = () => {
        setToken(null);
        localStorage.removeItem("healthyhabithubTOKEN");
        localStorage.removeItem("healthyhabithubUSERNAME");
    }

    const contexValue = {
        tokenJWT: token,
        isUserLogged: userIsLoggedIn,
        username: username,
        login: handleLogin,
        logout: handleLogout,
    };

    return (
        <AuthContext.Provider value={contexValue}>{props.children}</AuthContext.Provider>
    )

};

export function RequiredAuth(props){
    let authState = React.useContext(AuthContext);
    let location = useLocation();
    if(!authState.isUserLogged){
        return (
            <Navigate to="/"
                state={{
                    msg: "You must be logged to use this function.",
                    pathTo: location.pathname
                }}/>
        )
    }

    return props.children;
}
