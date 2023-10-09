import classes from './Statistics.module.css'
import { useViewport } from '../../contexts/viewportContext'
const Statistics = () => {
  const { width, breakpoint } = useViewport()

  return width < breakpoint ? <StatisticsMobile /> : <StatisticsDesktop />
}

const StatisticsMobile = () => {
  return (
    <div className={classes['mobile-container']}>
      <Statistic number={978} text="MEMBERS JOINED" />
    </div>
  )
}
const StatisticsDesktop = () => {
  return (
    <div className={classes['dekstop-container']}>
      <Statistic number={56} text="ARTICLES" />
      <Statistic number={978} text="MEMBERS JOINED" />
      <Statistic number={12} text="EXERCISES" />
    </div>
  )
}

const Statistic = ({ number, text }) => {
  return (
    <div className={classes['statistic']}>
      <h4>+ {number}</h4>
      <p>{text}</p>
    </div>
  )
}

export default Statistics
