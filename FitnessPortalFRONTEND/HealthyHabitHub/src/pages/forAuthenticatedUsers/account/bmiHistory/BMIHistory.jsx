import { useContext, useState } from 'react'
import { apiUrl } from '../../../../global/api_url'
import classes from './BMIHistory.module.css'
import AuthContext from '../../../../contexts/authContext'
import { useViewport } from '../../../../contexts/viewportContext'
import useContentHeightDesktop from '../../../../hooks/useContentHeightDesktop'
import useFetchPaginatedData from '../../../../hooks/useFetchPaginatedData'
import MySpinner from '../../../../components/mySpinner/MySpinner'
import ErrorMessage from '../../../../components/messages/ErrorMessage'
import NoDataMessage from '../../../../components/messages/NoDatamessage'
import PaginationPanel from '../../../../components/pagination/PaginationPanel'
import BMITable from '../../../../components/bmiTable/BMITable'
import { useCheckTokenExpiration } from '../../../../hooks/useCheckTokenExpiration'

const BMIHistory = () => {
  useCheckTokenExpiration()
  const { isMobile } = useViewport()
  const url = apiUrl + '/api/calculator/bmi'
  const [pageNumber, setPageNumber] = useState(1)
  const initialPageSize = isMobile ? 15 : 10
  const [pageSize, setPageSize] = useState(initialPageSize)
  const pageSizes = isMobile ? [10, 15, 20] : [5, 10, 15]
  const authCtx = useContext(AuthContext)
  const token = authCtx.tokenJWT
  const { contentHeight } = useContentHeightDesktop()
  const {
    data,
    loading,
    itemFrom,
    itemTo,
    totalItemsCount,
    totalPages,
    error,
  } = useFetchPaginatedData(url, pageNumber, pageSize, token)

  const handlePageSizeChange = (event) => {
    setPageSize(Number(event.target.value))
    setPageNumber(1)
  }

  const handlePageChange = (newPageNumber) => {
    setPageNumber(newPageNumber)
  }

  let bmiList = null

  if (loading) {
    bmiList = <MySpinner />
  } else if (data && data.length > 0) {
    bmiList = <BMITable bmiHistory={data} startNumber={itemFrom} />
  } else if (error) {
    bmiList = (
      <div>
        <ErrorMessage message={error} widthPercentage={100} />
      </div>
    )
  } else {
    bmiList = (
      <NoDataMessage
        mainMessage="You haven't calculated BMI yet."
        linkMessage="Click here to check your BMI"
        link="/calculators"
      />
    )
  }

  const bmiHistoryDivHeight = isMobile ? contentHeight : '100%'

  return (
    <div
      className={classes['bmi-history-div']}
      style={{ minHeight: bmiHistoryDivHeight }}
    >
      <div>
        <h2>Your BMI History</h2>
        <hr />
      </div>
      <div className={classes['bmi-list-div']}>{bmiList}</div>

      <PaginationPanel
        pageSize={pageSize}
        itemName="BMIs"
        handlePageSizeChange={handlePageSizeChange}
        handlePageChange={handlePageChange}
        itemFrom={itemFrom}
        itemTo={itemTo}
        pageNumber={pageNumber}
        totalItemsCount={totalItemsCount}
        totalPages={totalPages}
        pageSizes={pageSizes}
      />
    </div>
  )
}

export default BMIHistory
