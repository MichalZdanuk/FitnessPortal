import SentimentVeryDissatisfiedIcon from '@mui/icons-material/SentimentVeryDissatisfied'
import SentimentSatisfiedAltIcon from '@mui/icons-material/SentimentSatisfiedAlt'
import SentimentNeutralIcon from '@mui/icons-material/SentimentNeutral'
import classes from './bmi.module.css'

export const getBMICategoryIcon = (categoryName) => {
  const iconMap = {
    obesity: {
      icon: <SentimentVeryDissatisfiedIcon />,
      className: 'obesity-icon',
    },
    overweight: {
      icon: <SentimentVeryDissatisfiedIcon />,
      className: 'overweight-icon',
    },
    normalweight: {
      icon: <SentimentSatisfiedAltIcon />,
      className: 'normalweight-icon',
    },
    underweight: {
      icon: <SentimentVeryDissatisfiedIcon />,
      className: 'underweight-icon',
    },
    default: {
      icon: <SentimentNeutralIcon />,
    },
  }

  const category = iconMap[categoryName.toLowerCase()] || iconMap.default

  return <span className={classes[category.className]}>{category.icon}</span>
}
