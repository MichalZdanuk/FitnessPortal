/* eslint-disable react/no-unescaped-entities */
import { RegisterForm } from '../../components/registerAndLogin/RegisterForm'
import classes from './RegisterDesktop.module.css'
import extended from '../../assets/images/registerAndLogin/extended.png'
import logoIcon from '../../assets/images/letterHicon.png'
import { useNavigate } from 'react-router-dom'

const RegisterDesktop = () => {
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
        <RegisterForm />
      </div>
      <div className={classes['right-side-container']}>
        <img className={classes['img']} src={extended} alt="extended" />
        <h1 className={classes['quote-text']}>
          „The only bad workout is the one that didn't happen”
        </h1>
      </div>
    </div>
  )
}

export default RegisterDesktop
