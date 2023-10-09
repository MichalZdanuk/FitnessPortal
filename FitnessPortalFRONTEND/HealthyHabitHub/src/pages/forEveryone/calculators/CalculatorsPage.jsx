import classes from './CalculatorsPage.module.css'
import BMICalculator from '../../../components/calculators/BMICalculator'
import BMRCalculator from '../../../components/calculators/BMRCalculator'
import BodyFatCalculator from '../../../components/calculators/BodyFatCalcualtor'
import Carousel from 'react-material-ui-carousel'

const CalculatorsPage = () => {
  return (
    <div>
      <Carousel
        animation="slide"
        duration={800}
        autoPlay={false}
        navButtonsAlwaysInvisible={true}
      >
        <div className={classes['container']}>
          <BMICalculator />
        </div>
        <div className={classes['container']}>
          <BMRCalculator />
        </div>
        <div className={classes['container']}>
          <BodyFatCalculator />
        </div>
      </Carousel>
    </div>
  )
}

export default CalculatorsPage
