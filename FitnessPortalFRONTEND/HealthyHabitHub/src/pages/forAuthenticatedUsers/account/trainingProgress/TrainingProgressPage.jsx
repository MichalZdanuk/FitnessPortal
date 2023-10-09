import { useContext, useEffect, useState } from 'react'
import classes from './TrainingProgressPage.module.css'
import MySpinner from '../../../../components/mySpinner/MySpinner'
import ErrorMessage from '../../../../components/messages/ErrorMessage'
import TrainingProgressChart from '../../../../components/trainingProgress/TrainingProgressChart'
import { useViewport } from '../../../../contexts/viewportContext'
import useContentHeightDesktop from '../../../../hooks/useContentHeightDesktop'
import ChartButtons from '../../../../components/trainingProgress/ChartButtons'
import NoDataMessage from '../../../../components/messages/NoDatamessage'
import { useCheckTokenExpiration } from '../../../../hooks/useCheckTokenExpiration'
import fetchData from '../../../../utils/fetchData'
import AuthContext from '../../../../contexts/authContext'

const TrainingProgressPage = () => {
  useCheckTokenExpiration()
  const [data, setData] = useState(null)
  const [error, setError] = useState(null)
  const [isLoading, setIsLoading] = useState(false)
  const [selectedPeriod, setSelectedPeriod] = useState('month')
  const { isMobile } = useViewport()
  const { contentHeight } = useContentHeightDesktop()
  const url = '/api/training/chart-data'
  const authCtx = useContext(AuthContext)
  const token = authCtx.tokenJWT

  useEffect(() => {
    const fetchDataFromApi = async () => {
      setIsLoading(true)
      setError(null)

      try {
        const response = await fetchData(
          url,
          {
            period: selectedPeriod,
          },
          {
            Authorization: `Bearer ${token}`,
          },
        )
        setData(response)
      } catch (error) {
        setError(error.message)
      } finally {
        setIsLoading(false)
      }
    }

    fetchDataFromApi()
  }, [url, selectedPeriod, token])

  const trainingProgressDivHeight = isMobile ? contentHeight : '100%'
  const backgroundColor = isMobile ? '' : ''

  return (
    <div
      className={classes['training-page-div']}
      style={{
        minHeight: trainingProgressDivHeight,
        backgroundColor: backgroundColor,
      }}
    >
      {isLoading ? (
        <div
          className={classes['center-div']}
          style={{ minHeight: trainingProgressDivHeight }}
        >
          <MySpinner />
        </div>
      ) : error ? (
        <div
          className={classes['center-div']}
          style={{ minHeight: trainingProgressDivHeight }}
        >
          <ErrorMessage
            message="Something went wrong while fetching trainings data."
            widthPercentage={80}
          />
        </div>
      ) : data && data.length > 0 ? (
        <div className={classes['chart-container']}>
          <div className={classes['title-div']}>
            <h2>Your Training Progress</h2>
            <hr />
          </div>
          <ChartButtons
            selectedPeriod={selectedPeriod}
            setSelectedPeriod={setSelectedPeriod}
          />
          <TrainingProgressChart data={data} />
        </div>
      ) : (
        <NoDataMessage
          mainMessage="No data to present on chart."
          linkMessage="Add trainings to measure progress"
          link="/addTraining"
        />
      )}
    </div>
  )
}

export default TrainingProgressPage
