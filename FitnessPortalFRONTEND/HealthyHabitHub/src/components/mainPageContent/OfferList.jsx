import classes from './OfferList.module.css'
import { useNavigate } from 'react-router-dom'

const OfferList = ({ offerList }) => {
  return (
    <div className={classes['offers-container']}>
      {offerList.map((o) => (
        <OfferItem
          key={o.id}
          image={o.img}
          title={o.title}
          content={o.content}
          url={o.url}
        />
      ))}
    </div>
  )
}

const OfferItem = ({ image, title, content, url }) => {
  const navigate = useNavigate()
  const handleClick = () => {
    navigate(url)
  }

  return (
    <div className={classes['offer-div']} onClick={handleClick}>
      <img
        className={classes['offer-image']}
        src={image}
        alt="offer-item-image"
      />
      <div>
        <h4 className={classes['offer-title']}>{title}</h4>
        <p>{content}</p>
      </div>
    </div>
  )
}

export default OfferList
