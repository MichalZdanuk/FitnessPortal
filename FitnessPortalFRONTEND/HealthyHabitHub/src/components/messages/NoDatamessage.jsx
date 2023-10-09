import { useNavigate } from 'react-router-dom'
import classes from './NoDataMessage.module.css'

const NoDataMessage = ({ mainMessage, linkMessage, link }) => {
  const navigate = useNavigate()
  const handleClick = () => {
    navigate(link)
  }

  return (
    <div className={classes['no-data-div']}>
      <h3 className={classes['main-message']}>{mainMessage}</h3>
      {linkMessage && (
        <h4 className={classes['underline']} onClick={handleClick}>
          {linkMessage}
        </h4>
      )}
    </div>
  )
}

export default NoDataMessage
