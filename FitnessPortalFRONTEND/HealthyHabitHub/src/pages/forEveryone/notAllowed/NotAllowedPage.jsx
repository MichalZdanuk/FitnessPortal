import { useLocation } from 'react-router-dom'
import classes from './NotAllowedPage.module.css'
import notAllowedIcon from '../../../assets/images/pageNotAllowed.png'
import { Alert } from 'react-bootstrap'

const NotAllowedPage = () => {
  const location = useLocation()
  const message = location.state && location.state.msg

  return (
    <div className={classes['not-allowed-page-div']}>
      <img src={notAllowedIcon} alt="" className={classes['icon']} />
      <Alert variant={'danger'}>
        {message}
        <br />
        <Alert.Link href="/login">Log in to get access</Alert.Link>
      </Alert>
    </div>
  )
}

export default NotAllowedPage
