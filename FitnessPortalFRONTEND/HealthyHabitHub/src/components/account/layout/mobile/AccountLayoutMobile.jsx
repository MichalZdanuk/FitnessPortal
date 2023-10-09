import { Outlet, useLocation } from 'react-router-dom'
import classes from './AccountLayoutMobile.module.css'
import TileList from './TileList'

const AccountLayoutMobile = () => {
  const location = useLocation()

  return location.pathname === '/account' ? (
    <div className={classes['layout-div']}>
      <TileList />
    </div>
  ) : (
    <Outlet />
  )
}

export default AccountLayoutMobile
