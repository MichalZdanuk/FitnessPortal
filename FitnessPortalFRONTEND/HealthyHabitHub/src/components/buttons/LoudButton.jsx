import classes from './LoudButton.module.css'

const LoudButton = ({
  text,
  widthPercentage,
  type,
  onClick,
  disabled = false,
}) => {
  const buttonType = type === 'submit' ? 'submit' : 'button'

  return (
    <button
      className={classes['loud-button']}
      style={{ width: `${widthPercentage}%` }}
      type={buttonType}
      onClick={onClick}
      disabled={disabled}
    >
      {text}
    </button>
  )
}

export default LoudButton
