import classes from './CalculatorInput.module.css'

export const CalculatorInput = ({
  name,
  inputName,
  value,
  handleOnChange,
  placeholder,
  type,
  options,
}) => {
  if (type === 'radio') {
    return (
      <div className={classes['input-container']}>
        <label className={classes['label-text']}>{inputName}</label>
        <div className={classes['radio-options']}>
          {options.map((option) => (
            <label key={option} className={classes['radio-label']}>
              <input
                name={name}
                value={option}
                checked={value === option}
                onChange={handleOnChange}
                type="radio"
                className={classes['radio-input']}
              />
              {option}
            </label>
          ))}
        </div>
      </div>
    )
  }

  return (
    <div className={classes['input-container']}>
      <label className={classes['label-text']}>{inputName}</label>
      <input
        name={name}
        value={value}
        onChange={handleOnChange}
        placeholder={placeholder}
        type={type}
        required
        min={0}
        className={classes['input-box']}
      />
    </div>
  )
}
