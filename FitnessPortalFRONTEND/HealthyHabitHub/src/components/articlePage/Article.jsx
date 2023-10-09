import { useEffect, useState } from 'react'
import { useLocation, useParams } from 'react-router-dom'
import fetchData from '../../utils/fetchData'
import { MySpinnerWithContainer } from '../mySpinner/MySpinner'
import NotFoundMessage from '../messages/NotFoundMessage'
import AccessTimeIcon from '@mui/icons-material/AccessTime'
import classes from './Article.module.css'
import useContentHeightDesktop from '../../hooks/useContentHeightDesktop'

const Article = () => {
  const location = useLocation()
  const { articleId } = useParams()
  const [articleData, setArticleData] = useState(location.state)
  const [notFound, setNotFound] = useState(false)
  const { contentHeight } = useContentHeightDesktop()
  const unused = 1

  useEffect(() => {
    if (!articleData) {
      fetchArticleData(articleId)
    }
  }, [articleData, articleId])

  const fetchArticleData = async (id) => {
    try {
      const response = await fetchData(`/api/article/${id}`)
      setArticleData(response)
    } catch (error) {
      if (error.response && error.response.status === 404) {
        setNotFound(true)
      } else {
        console.error(error)
      }
    }
  }

  const notFoundStyle = {
    display: 'flex',
    justifyContent: 'center',
    alignItems: 'center',
    height: contentHeight,
  }

  if (notFound) {
    return (
      <div style={notFoundStyle}>
        <NotFoundMessage name="Article" />
      </div>
    )
  }

  if (!articleData) {
    return <MySpinnerWithContainer />
  }

  const { title, category, dateOfPublication, author, content } = articleData
  const formattedDate = dateOfPublication.substring(0, 10)

  return (
    <div className={classes['articlePage-wrapper']}>
      <div className={classes['article-container']}>
        <Title title={title} />
        <AdditionalInfo date={formattedDate} category={category} />
        <ArticleContent content={content} />
        <Author author={author} />
      </div>
    </div>
  )
}

const Title = ({ title }) => {
  return <h3 className={classes['article-title']}>{title}</h3>
}

const AdditionalInfo = ({ date, category }) => {
  return (
    <div className={classes['additionalInfo-container']}>
      <h5 className={classes['article-date-container']}>
        <AccessTimeIcon />
        {date}
      </h5>
      <h5 className={classes['bold-text']}>{category}</h5>
    </div>
  )
}

const ArticleContent = ({ content }) => {
  return <p className={classes['content']}>{content}</p>
}

const Author = ({ author }) => {
  return <div className={classes['author-text']}>{author}</div>
}

export default Article
