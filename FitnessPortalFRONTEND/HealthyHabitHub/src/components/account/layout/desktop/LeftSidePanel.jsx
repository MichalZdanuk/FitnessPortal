import classes from './LeftSidePanel.module.css'
import { useNavigate } from 'react-router-dom'
import AccountBoxIcon from '@mui/icons-material/AccountBox'
import Diversity3Icon from '@mui/icons-material/Diversity3'
import AutoGraphIcon from '@mui/icons-material/AutoGraph'
import FitnessCenterIcon from '@mui/icons-material/FitnessCenter'
import ScaleIcon from '@mui/icons-material/Scale'

const LeftSidePanel = () => {
  const navigate = useNavigate()

  return (
    <div className={classes['panel-div']}>
      <h4 className={classes['panel-title']}>Account Dashboard</h4>
      <ul className={classes['ul-blank']}>
        <li
          className={classes['li-item']}
          onClick={() => {
            navigate('/account/profile')
          }}
        >
          <AccountBoxIcon />
          My Profile
        </li>
        <li
          className={classes['li-item']}
          onClick={() => {
            navigate('/account/friendList')
          }}
        >
          <Diversity3Icon />
          Friends
        </li>
        <li
          className={classes['li-item']}
          onClick={() => {
            navigate('/account/trainingProgress')
          }}
        >
          <AutoGraphIcon />
          Training
          <br />
          Progress
        </li>
        <li
          className={classes['li-item']}
          onClick={() => {
            navigate('/account/trainingHistory')
          }}
        >
          <FitnessCenterIcon />
          Training
          <br />
          History
        </li>
        <li
          className={classes['li-item']}
          onClick={() => {
            navigate('/account/bmiHistory')
          }}
        >
          <ScaleIcon />
          BMI History
        </li>
      </ul>
    </div>
  )
}

export default LeftSidePanel
