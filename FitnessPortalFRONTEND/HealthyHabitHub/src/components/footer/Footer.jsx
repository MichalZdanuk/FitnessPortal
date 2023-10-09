import classes from './Footer.module.css'

const Footer = () => {
  const handleFooterClick = () => {
    window.location.href = 'https://github.com/MichalZdanuk'
  }

  return (
    <footer className={classes['footer']} id="footer">
      <h6 className={classes['credentials']} onClick={handleFooterClick}>
        @Michał Żdanuk 2023
      </h6>
    </footer>
  )
}

export default Footer
