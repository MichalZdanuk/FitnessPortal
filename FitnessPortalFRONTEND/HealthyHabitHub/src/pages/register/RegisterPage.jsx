import { useViewport } from '../../contexts/viewportContext'
import RegisterDesktop from './RegisterDesktop'
import RegisterMobile from './RegisterMobile'

const RegisterPage = () => {
  const { isMobile } = useViewport()

  return isMobile ? <RegisterMobile /> : <RegisterDesktop />
}

export default RegisterPage
