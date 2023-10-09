import WelcomeBanner from '../../../components/mainPageContent/WelcomeBanner'
import Statistics from '../../../components/mainPageContent/Statistics'
import AdvantagesList from '../../../components/mainPageContent/AdvantagesList'
import OfferList from '../../../components/mainPageContent/OfferList'
import { advantages, offerList } from '../../../mocks/mockedHomePage'
import classes from './MainPage.module.css'

const MainPage = () => {
  return (
    <div className={classes['main-page-container']}>
      <WelcomeBanner />
      <Statistics />
      <AdvantagesList advantageList={advantages} />
      <OfferList offerList={offerList} />
    </div>
  )
}

export default MainPage
