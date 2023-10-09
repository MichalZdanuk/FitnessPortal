import classes from './InputField.module.css'

export const InputField = ({
  name,
  inputName,
  value,
  handleOnChange,
  placeholder,
  type,
  options,
  endIcon = null,
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
      <div className={classes['input-box-wrapper']}>
        <input
          name={name}
          value={value}
          onChange={handleOnChange}
          placeholder={placeholder}
          type={type}
          required
          className={classes['input-box']}
        />
        {endIcon !== null && (
          <label className={classes['end-icon']}>{endIcon}</label>
        )}
      </div>
    </div>
  )
}
