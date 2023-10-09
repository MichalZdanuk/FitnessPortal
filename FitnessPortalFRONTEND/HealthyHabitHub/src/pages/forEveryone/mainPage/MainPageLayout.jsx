import { Outlet, useLocation } from 'react-router-dom'
import MyNavbar from '../../../components/navbar/MyNavbar'
import MainPage from './MainPage'
import Footer from '../../../components/footer/Footer'
import classes from './MainPageLayout.module.css' // Import debounce from Lodash
import useContentHeightDesktop from '../../../hooks/useContentHeightDesktop'

const MainPageLayout = () => {
  const location = useLocation()
  const { contentHeight } = useContentHeightDesktop()

  return (
    <div className={classes['main-page-layout']}>
      <div className={classes['content-wrapper']}>
        <MyNavbar />
        {location.pathname === '/' ? (
          <MainPage />
        ) : (
          <div
            className={classes['background-color']}
            style={{ minHeight: contentHeight }}
          >
            <Outlet />
          </div>
        )}
      </div>
      <Footer />
    </div>
  )
}

export default MainPageLayout
