import classes from './RegisterMobile.module.css'
import { RegisterForm } from '../../components/registerAndLogin/RegisterForm'
import logo from '../../assets/images/letterHicon.png'
import { useNavigate } from 'react-router-dom'

const RegisterMobile = () => {
  const navigate = useNavigate()

  return (
    <div className={classes['register-page-container']}>
      <img
        className={classes['img']}
        src={logo}
        alt="logo"
        onClick={() => {
          navigate('/')
        }}
      />
      <RegisterForm />
    </div>
  )
}

export default RegisterMobile
