import { useNavigate } from 'react-router-dom'
import pageNotFoundIcon from '../../../assets/images/pageNotFound.png'
import classes from './NotFoundPage.module.css'
import { Button } from 'react-bootstrap'

const NotFoundPage = () => {
  const navigate = useNavigate()

  return (
    <div className={classes['not-found-page-container']}>
      <img
        className={classes['img']}
        src={pageNotFoundIcon}
        alt="Page Not Found"
      />
      <div className={classes['text-container']}>
        <h2 className={classes['main-text']}>PAGE NOT FOUND</h2>
        <p>
          We looked everywhere for this page.
          <br />
          Are you sure the website URL is correct?
          <br />
          Get in touch with the site owner. `
        </p>
      </div>
      <Button
        onClick={() => {
          navigate('/')
        }}
        variant="outline-info"
        size="lg"
      >
        Go Back Home
      </Button>
    </div>
  )
}

export default NotFoundPage
