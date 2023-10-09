import classes from './StickyButton.module.css'

const StickyButton = ({ text, onClick, disabled = false }) => {
  return (
    <div className={classes['bottom-button-container']}>
      <button
        className={classes['bottom-button']}
        onClick={onClick}
        disabled={disabled}
      >
        {text}
      </button>
    </div>
  )
}

export default StickyButton
