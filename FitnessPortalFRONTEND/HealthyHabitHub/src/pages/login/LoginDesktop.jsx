import { LoginForm } from '../../components/registerAndLogin/LoginForm'
import classes from './LoginDesktop.module.css'
import lotusIcon from '../../assets/images/registerAndLogin/lotus.png'
import logoIcon from '../../assets/images/letterHicon.png'
import { useNavigate } from 'react-router-dom'

const LoginDesktop = () => {
  const navigate = useNavigate()

  return (
    <div className={classes['login-page-container']}>
      <div className={classes['left-side-container']}>
        <img
          onClick={() => {
            navigate('/')
          }}
          className={classes['logo-img']}
          src={logoIcon}
          alt="logo"
        />
        <LoginForm />
      </div>
      <div className={classes['right-side-container']}>
        <img className={classes['img']} src={lotusIcon} alt="lotus icon" />
        <h1 className={classes['quote-text']}>
          „The pain you feel today will be the strength you fell tomorrow”
        </h1>
      </div>
    </div>
  )
}

export default LoginDesktop
