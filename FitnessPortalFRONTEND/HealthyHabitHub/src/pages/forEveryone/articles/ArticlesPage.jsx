import classes from './ArticlesPage.module.css'
import ArticleThumbnail from '../../../components/articlePage/ArticleThumbnail'
import MySpinner from '../../../components/mySpinner/MySpinner'
import { apiUrl } from '../../../global/api_url'
import useFetchPaginatedData from '../../../hooks/useFetchPaginatedData'
import PaginationPanel from '../../../components/pagination/PaginationPanel'
import { useState } from 'react'
import useContentHeightDesktop from '../../../hooks/useContentHeightDesktop'
import ErrorMessage from '../../../components/messages/ErrorMessage'

const ArticlesPage = () => {
  const url = apiUrl + '/api/article'
  const [pageNumber, setPageNumber] = useState(1)
  const [pageSize, setPageSize] = useState(5)
  const pageSizes = [3, 5, 10]
  const { contentHeight } = useContentHeightDesktop()

  const {
    data,
    loading,
    itemFrom,
    itemTo,
    totalItemsCount,
    totalPages,
    error,
  } = useFetchPaginatedData(url, pageNumber, pageSize)

  const handlePageSizeChange = (event) => {
    setPageSize(Number(event.target.value))
    setPageNumber(1)
  }

  const handlePageChange = (newPageNumber) => {
    setPageNumber(newPageNumber)
  }

  let articleThumbnailList = null

  if (loading) {
    articleThumbnailList = (
      <div style={{ paddingTop: '35dvh' }}>
        <MySpinner />
      </div>
    )
  } else if (error) {
    return (
      <div style={{ minHeight: contentHeight, paddingTop: '45dvh' }}>
        <ErrorMessage message={error} widthPercentage={50} />
      </div>
    )
  } else if (data && data.length > 0) {
    articleThumbnailList = data.map((article) => (
      <ArticleThumbnail key={article.id} article={article} />
    ))
  } else {
    articleThumbnailList = <p>No articles available yet</p>
  }

  return (
    <div
      className={classes['articles-container']}
      style={{ minHeight: contentHeight }}
    >
      <div className={classes['articles']}>{articleThumbnailList}</div>
      {!loading && (
        <PaginationPanel
          pageSize={pageSize}
          itemName="Articles"
          handlePageSizeChange={handlePageSizeChange}
          handlePageChange={handlePageChange}
          itemFrom={itemFrom}
          itemTo={itemTo}
          pageNumber={pageNumber}
          totalItemsCount={totalItemsCount}
          totalPages={totalPages}
          pageSizes={pageSizes}
        />
      )}
    </div>
  )
}

export default ArticlesPage
