import { useViewport } from '../../contexts/viewportContext'
import LoginMobile from './LoginMobile'
import LoginDesktop from './LoginDesktop'

const LoginPage = () => {
  const { isMobile } = useViewport()

  return isMobile ? <LoginMobile /> : <LoginDesktop />
}

export default LoginPage
