import { useNavigate } from 'react-router-dom'
import classes from './ArticleThumbnail.module.css'
import AccessTimeIcon from '@mui/icons-material/AccessTime'
const ArticleThumbnail = ({ article }) => {
  const navigate = useNavigate()

  const handleThumbnailClick = () => {
    navigate(`/articles/${article.id}`, { state: article })
  }

  return (
    <div
      className={classes['articleThumbnail-container']}
      onClick={handleThumbnailClick}
    >
      <div>
        <Title title={article.title} />
        <AdditionalInfo
          date={article.dateOfPublication}
          category={article.category}
        />
      </div>
      <ShortDescription shortDescription={article.shortDescription} />
    </div>
  )
}

const Title = ({ title }) => {
  return <h4 className={classes['articleThumbnail-title']}>{title}</h4>
}

const AdditionalInfo = ({ date, category }) => {
  return (
    <div className={classes['additionalInfo-container']}>
      <h5>{category}</h5>
      <h6 className={classes['article-date-container']}>
        <AccessTimeIcon />
        {date.substring(0, 10)}
      </h6>
    </div>
  )
}

const ShortDescription = ({ shortDescription }) => {
  return <h5 className={classes['shortDescription']}>{shortDescription}</h5>
}

export default ArticleThumbnail
