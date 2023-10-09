import { Outlet } from 'react-router-dom'
import classes from './AccountLayoutDesktop.module.css'
import LeftSidePanel from './LeftSidePanel'
import useContentHeightDesktop from '../../../../hooks/useContentHeightDesktop'

const AccountLayoutDesktop = () => {
  const { contentHeight } = useContentHeightDesktop()

  return (
    <div className={classes['container']} style={{ minHeight: contentHeight }}>
      <div className={classes['left-side']}>
        <LeftSidePanel />
      </div>
      <div className={classes['right-side']}>
        <Outlet />
      </div>
    </div>
  )
}

export default AccountLayoutDesktop
