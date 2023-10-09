import { useContext, useState } from 'react'
import LoudButton from '../../buttons/LoudButton'
import { InputField } from '../../registerAndLogin/InputField'
import classes from './UpdateProfileForm.module.css'
import AuthContext from '../../../contexts/authContext'
import axios from 'axios'
import { apiUrl } from '../../../global/api_url'
import ErrorMessage from '../../messages/ErrorMessage'
import { Alert } from 'react-bootstrap'
import { debounce } from 'lodash'

const UpdateProfileForm = ({ userData }) => {
  const authCtx = useContext(AuthContext)

  const [formData, setFormData] = useState({
    username: userData.username || '',
    email: userData.email || '',
    dateOfBirth: userData.dateOfBirth.substring(0, 10) || '',
    height: userData.height || '',
    weight: userData.weight || '',
  })

  const [success, setSuccess] = useState(false)
  const [error, setError] = useState(null)
  const [isFormDataChanged, setIsFormDataChanged] = useState(false)

  const handleInputChange = (e) => {
    const { name, value } = e.target
    setFormData({
      ...formData,
      [name]: value,
    })
    setIsFormDataChanged(true)
  }
  const url = apiUrl + '/api/account/update-profile'

  const validateInputs = () => {
    if (formData.username.length > 30 || formData.username.length < 6) {
      setError('Username should take (6,30) characters.')
      return false
    }
    if (formData.email.length > 30 || formData.email.length < 6) {
      setError('Email should take (6,30) characters.')
      return false
    }
    if (formData.height > 240 || formData.height < 80) {
      setError('Height should be between (80 and 240) cm.')
      return false
    }
    if (formData.weight > 200 || formData.weight < 30) {
      setError('Weight should be between (30 and 200) kg.')
      return false
    }
    return true
  }

  const debouncedHandleUpdate = debounce(async () => {
    setError(null)
    setSuccess(false)

    if (!isFormDataChanged) {
      return // Don't send a request if nothing changed
    }

    if (!validateInputs()) return
    try {
      const response = await axios.put(
        url,
        {
          username: formData.username,
          email: formData.email,
          dateOfBirth: formData.dateOfBirth,
          weight: formData.weight,
          height: formData.height,
        },
        {
          headers: {
            Authorization: `Bearer ${authCtx.tokenJWT}`,
          },
        },
      )

      if (response.data) {
        const newToken = response.data
        authCtx.login(newToken)
        setSuccess(true)
      }
    } catch (error) {
      setSuccess(false)
      if (error.response && error.response.status === 400) {
        setError('This email is taken')
      }
    }
  }, 1000)

  const handleSubmit = (e) => {
    e.preventDefault()
    debouncedHandleUpdate()
    setIsFormDataChanged(false)
  }

  return (
    <div className={classes['update-div']}>
      {success && (
        <div style={{ display: 'flex', justifyContent: 'center' }}>
          <Alert
            style={{ width: '70%', textAlign: 'center' }}
            variant={'success'}
          >
            Profile successfully updated!
          </Alert>
        </div>
      )}
      {error && <ErrorMessage message={error} widthPercentage={70} />}
      <form onSubmit={handleSubmit} className={classes['form-container']}>
        <InputField
          name="username"
          inputName="Username"
          value={formData.username}
          handleOnChange={handleInputChange}
          placeholder="username"
          type=""
        />
        <InputField
          name="email"
          inputName="Email"
          value={formData.email}
          handleOnChange={handleInputChange}
          placeholder="email"
          type="email"
        />
        <InputField
          name="dateOfBirth"
          inputName="Date of Birth"
          value={formData.dateOfBirth}
          handleOnChange={handleInputChange}
          placeholder="enter date of birth"
          type=""
        />
        <InputField
          name="height"
          inputName="Height"
          value={formData.height}
          handleOnChange={handleInputChange}
          placeholder="enter height"
          type="number"
        />
        <InputField
          name="weight"
          inputName="Weight"
          value={formData.weight}
          handleOnChange={handleInputChange}
          placeholder="enter weight"
          type="number"
        />
        <div className={classes['submit-button-wrapper']}>
          <LoudButton text="UPDATE" widthPercentage={40} type="submit" />
        </div>
      </form>
    </div>
  )
}

export default UpdateProfileForm
