import classes from './Calculator.module.css'
import { useState } from 'react'
import { CalculatorInput } from './CalculatorInput'
import fetchData from '../../utils/fetchData'
import { Alert } from 'react-bootstrap'
import LoudButton from '../buttons/LoudButton'

const BodyFatCalculator = () => {
  const [result, setResult] = useState({
    bodyFatLevel: 0,
  })
  const [error, setError] = useState('')

  const fetchBMI = async (height, waist, hip, neck, sex) => {
    setError('')
    try {
      const response = await fetchData(`/api/calculator/bodyFat/anonymous`, {
        height,
        waist,
        hip,
        neck,
        sex,
      })

      setResult({
        bodyFatLevel: response.bodyFatLevel,
      })
    } catch (error) {
      setError('Inavlid data')
    }
  }

  const handleCaluclate = async (e, calculatorInput) => {
    e.preventDefault()
    const { height, waist, hip, neck, sex } = calculatorInput

    fetchBMI(height, waist, hip, neck, sex)
  }

  return (
    <div className={classes['calculator-container']}>
      <div className={classes['alert-div']}>
        {error && <Alert variant="danger">{error}</Alert>}
      </div>
      <BodyFatResult bodyFatLevel={result.bodyFatLevel} />
      <BodyFatCalculatorForm handleCaluclate={handleCaluclate} />
    </div>
  )
}

const BodyFatCalculatorForm = ({ handleCaluclate }) => {
  const [calculatorInput, setCalculatorInput] = useState({
    height: '',
    waist: '',
    hip: '',
    neck: '',
    sex: '',
  })

  const onCalculatorInputChange = (e) => {
    e.persist()
    const { name, value } = e.target
    setCalculatorInput((prevState) => ({
      ...prevState,
      [name]: value,
    }))
  }

  return (
    <form
      onSubmit={(e) => handleCaluclate(e, calculatorInput)}
      className={classes['form-container']}
    >
      <CalculatorInput
        name="height"
        inputName="Height"
        value={calculatorInput.height}
        handleOnChange={onCalculatorInputChange}
        placeholder="height"
        type="number"
      />
      <CalculatorInput
        name="waist"
        inputName="Waist"
        value={calculatorInput.waist}
        handleOnChange={onCalculatorInputChange}
        placeholder="waist"
        type="number"
      />
      <CalculatorInput
        name="hip"
        inputName="Hip"
        value={calculatorInput.hip}
        handleOnChange={onCalculatorInputChange}
        placeholder="hip"
        type="number"
      />
      <CalculatorInput
        name="neck"
        inputName="Neck"
        value={calculatorInput.neck}
        handleOnChange={onCalculatorInputChange}
        placeholder="neck"
        type="number"
      />
      <CalculatorInput
        name="sex"
        inputName="Sex"
        value={calculatorInput.sex}
        handleOnChange={onCalculatorInputChange}
        type="radio"
        options={['Male', 'Female']}
      />
      <div className={classes['calculate-button-div']}>
        <LoudButton text="CALCULATE" widthPercentage={90} type="submit" />
      </div>
    </form>
  )
}

const BodyFatResult = ({ bodyFatLevel }) => {
  const formattedBodyFatLevel = bodyFatLevel.toFixed(2)
  return (
    <div className={classes['result-div']}>
      <h2>Your BF is: {formattedBodyFatLevel} %</h2>
    </div>
  )
}

export default BodyFatCalculator
