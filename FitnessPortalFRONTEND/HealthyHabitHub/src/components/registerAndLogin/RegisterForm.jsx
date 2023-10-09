import classes from './RegisterForm.module.css'
import { useState } from 'react'
import axios from 'axios'
import { apiUrl } from '../../global/api_url'
import { Alert } from 'react-bootstrap'
import FirstStepRegisterForm from './FirstStepRegisterForm'
import SecondStepRegisterForm from './SecondStepRegisterForm'

export const RegisterForm = () => {
  const [registerInput, setRegisterInput] = useState({
    username: '',
    email: '',
    password: '',
    confirmPassword: '',
    dateOfBirth: '',
    height: '',
    weight: '',
  })

  const onRegisterInputChange = (e) => {
    e.persist()
    const { name, value } = e.target
    setRegisterInput({ ...registerInput, [name]: value })
  }

  const [registerError, setRegisterError] = useState('')
  const [registerSuccess, setRegisterSuccess] = useState('')
  const [step, setStep] = useState('1')

  const validateInputs = () => {
    if (registerInput.password.length < 6) {
      setRegisterError({ title: "Password's minimum length is 6" })
      return false
    }
    if (registerInput.confirmPassword != registerInput.password) {
      setRegisterError({
        title: 'Password and Confirm Password does not match',
      })
      return false
    }
    if (registerInput.height > 240 || registerInput.height < 80) {
      setRegisterError({ title: 'Height should be between (80 and 240) cm.' })
      return false
    }
    if (registerInput.weight > 200 || registerInput.weight < 30) {
      setRegisterError({ title: 'Weight should be between (30 and 200) kg.' })
      return false
    }
    if (
      registerInput.username.length > 30 ||
      registerInput.username.length < 6
    ) {
      setRegisterError({ title: 'Username should take (6,30) characters.' })
      return false
    }
    if (registerInput.email > 30 || registerInput.email < 6) {
      setRegisterError({ title: 'Email should take (6,30) characters.' })
      return false
    }
    return true
  }

  const handleRegister = async (e) => {
    e.preventDefault()
    setRegisterSuccess(null)
    setRegisterError(null)

    if (step === '1') {
      setStep('2')
    } else if (step === '2') {
      if (!validateInputs()) return
      try {
        const url = apiUrl + '/api/account/register'

        const response = await axios.post(url, {
          username: registerInput.username,
          email: registerInput.email,
          password: registerInput.password,
          confirmPassword: registerInput.confirmPassword,
          dateOfBirth: registerInput.dateOfBirth,
          height: registerInput.height,
          weight: registerInput.weight,
        })

        if (response.status === 200) setRegisterSuccess(true)
      } catch (error) {
        console.log('error: ', error)
        setRegisterError({ title: 'Invalid credentials data' })
      }
    }
  }

  const propsRegisterForm = {
    registerInput,
    handleRegister,
    onRegisterInputChange,
  }

  return (
    <div className={classes['register-form-container']}>
      {step === '1' && <FirstStepRegisterForm {...propsRegisterForm} />}
      {step === '2' && (
        <SecondStepRegisterForm {...propsRegisterForm} setStep={setStep} />
      )}
      {registerError && (
        <div className={classes['alert-div']}>
          <Alert variant={'danger'}>{registerError.title}</Alert>
        </div>
      )}
      {registerSuccess && (
        <div className={classes['alert-div']}>
          <Alert variant={'success'}>
            Successful registration{' '}
            <Alert.Link href="/login">log in NOW</Alert.Link>
          </Alert>
        </div>
      )}
    </div>
  )
}
