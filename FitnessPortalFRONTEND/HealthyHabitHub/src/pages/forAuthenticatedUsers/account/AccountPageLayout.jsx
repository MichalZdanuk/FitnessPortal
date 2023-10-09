import { RequiredAuth } from '../../../contexts/authContext'
import { useViewport } from '../../../contexts/viewportContext'
import AccountLayoutDesktop from '../../../components/account/layout/desktop/AccountLayoutDesktop'
import AccountLayoutMobile from '../../../components/account/layout/mobile/AccountLayoutMobile'
import { useCheckTokenExpiration } from '../../../hooks/useCheckTokenExpiration'

const AccountPageLayout = () => {
  const { isMobile } = useViewport()
  useCheckTokenExpiration()

  return (
    <RequiredAuth>
      {isMobile ? <AccountLayoutMobile /> : <AccountLayoutDesktop />}
    </RequiredAuth>
  )
}

export default AccountPageLayout
