import { useContext, useEffect } from 'react'
import jwtDecode from 'jwt-decode'
import AuthContext from '../contexts/authContext'

export const useCheckTokenExpiration = () => {
  const authCtx = useContext(AuthContext)

  useEffect(() => {
    const checkToken = () => {
      const token = authCtx.tokenJWT
      if (token) {
        const decodedToken = jwtDecode(token)
        if (decodedToken.exp < Date.now() / 1000) {
          authCtx.logout()
        }
      }
    }

    checkToken()
  }, [authCtx])

  return authCtx.isUserLogged
}
