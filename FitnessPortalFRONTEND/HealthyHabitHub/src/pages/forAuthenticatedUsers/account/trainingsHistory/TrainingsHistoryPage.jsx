import classes from './TrainingsHistoryPage.module.css'
import { useContext, useState } from 'react'
import PaginationPanel from '../../../../components/pagination/PaginationPanel'
import MySpinner from '../../../../components/mySpinner/MySpinner'
import TrainingCard from '../../../../components/trainings/TrainingCard'
import NoDataMessage from '../../../../components/messages/NoDatamessage'
import AuthContext from '../../../../contexts/authContext'
import { apiUrl } from '../../../../global/api_url'
import { useViewport } from '../../../../contexts/viewportContext'
import useContentHeightDesktop from '../../../../hooks/useContentHeightDesktop'
import useFetchPaginatedData from '../../../../hooks/useFetchPaginatedData'
import ErrorMessage from '../../../../components/messages/ErrorMessage'
import { useCheckTokenExpiration } from '../../../../hooks/useCheckTokenExpiration'

const TrainingsHistoryPage = () => {
  useCheckTokenExpiration()
  const url = apiUrl + '/api/training'
  const [pageNumber, setPageNumber] = useState(1)
  const [pageSize, setPageSize] = useState(3)
  const pageSizes = [3, 5, 10]
  const authCtx = useContext(AuthContext)
  const token = authCtx.tokenJWT
  const { isMobile } = useViewport()
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

  let trainingsList = null

  if (loading) {
    trainingsList = <MySpinner />
  } else if (data && data.length > 0) {
    trainingsList = data.map((training) => (
      <TrainingCard
        key={training.id}
        date={training.dateOfTraining}
        totalPayload={training.totalPayload}
        numOfSeries={training.numberOfSeries}
        listOfExercises={training.exercises}
      />
    ))
  } else if (error) {
    trainingsList = (
      <div>
        <ErrorMessage message={error} widthPercentage={100} />
      </div>
    )
  } else {
    trainingsList = (
      <NoDataMessage
        mainMessage="You haven't added trainings yet."
        linkMessage="Click here to add a training"
        link="/addTraining"
      />
    )
  }

  const historyDivHeight = isMobile ? contentHeight : '100%'

  return (
    <div
      className={classes['training-history-div']}
      style={{ minHeight: historyDivHeight }}
    >
      <div>
        <h2>Your Training History</h2>
        <hr />
      </div>
      <div className={classes['trainings-list-div']}>{trainingsList}</div>

      <PaginationPanel
        pageSize={pageSize}
        itemName="Trainings"
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

export default TrainingsHistoryPage
