import classes from './LoginMobile.module.css'
import { LoginForm } from '../../components/registerAndLogin/LoginForm'
import logo from '../../assets/images/letterHicon.png'
import { useNavigate } from 'react-router-dom'

const LoginMobile = () => {
  const navigate = useNavigate()

  return (
    <div className={classes['login-page-container']}>
      <img
        className={classes['img']}
        src={logo}
        alt="logo"
        onClick={() => {
          navigate('/')
        }}
      />
      <LoginForm />
    </div>
  )
}

export default LoginMobile
