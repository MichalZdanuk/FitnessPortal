import React, { useContext, useState } from 'react'
import { Navigate, useLocation } from 'react-router-dom'

const AuthContext = React.createContext({
  tokenJWT: '',
  isUserLogged: false,
  // eslint-disable-next-line no-unused-vars
  login: (tokenJWT) => {},
  logout: () => {},
})

export default AuthContext

const retrieveTokenFromStorage = () => {
  const storedToken = localStorage.getItem('healthyhabithubTOKEN')

  return { token: storedToken }
}

export const AuthContextProvider = (props) => {
  const tokenData = retrieveTokenFromStorage()

  let initialToken
  if (tokenData) {
    initialToken = tokenData.token
  }

  const [token, setToken] = useState(initialToken)
  const userIsLoggedIn = !!token

  const handleLogin = (token) => {
    setToken(token)
    localStorage.setItem('healthyhabithubTOKEN', token)
  }

  const handleLogout = () => {
    setToken(null)
    localStorage.removeItem('healthyhabithubTOKEN')
  }

  const contextValue = {
    tokenJWT: token,
    isUserLogged: userIsLoggedIn,
    login: handleLogin,
    logout: handleLogout,
  }

  return (
    <AuthContext.Provider value={contextValue}>
      {props.children}
    </AuthContext.Provider>
  )
}

export function RequiredAuth({ children }) {
  let authState = useContext(AuthContext)
  let location = useLocation()
  if (!authState.isUserLogged) {
    return (
      <Navigate
        to="/notAllowed"
        state={{
          msg: 'You must be logged in to access this page.',
          pathTo: location.pathname,
        }}
      />
    )
  }

  return children
}
