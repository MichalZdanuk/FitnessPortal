import axios from 'axios'
import { useEffect, useState, useMemo } from 'react'

const useFetchPaginatedData = (url, pageNumber, pageSize, jwtToken = null) => {
  const [data, setData] = useState(null)
  const [loading, setLoading] = useState(true)
  const [itemFrom, setItemFrom] = useState(0)
  const [itemTo, setItemTo] = useState(0)
  const [totalItemsCount, setTotalItemsCount] = useState(0)
  const [totalPages, setTotalPages] = useState(0)
  const [error, setError] = useState(null)

  useEffect(() => {
    const fetchData = async () => {
      try {
        setLoading(true)
        setError(null)
        const headers = {}
        if (jwtToken) {
          headers['Authorization'] = `Bearer ${jwtToken}`
        }
        const response = await axios.get(url, {
          params: {
            pageNumber,
            pageSize,
          },
          headers,
        })

        setData(response.data.items)
        setItemFrom(response.data.itemFrom)
        setItemTo(response.data.itemTo)
        setTotalItemsCount(response.data.totalItemsCount)
        setTotalPages(response.data.totalPages)
        setLoading(false)
      } catch (error) {
        console.error(error)
        setError('Error fetching data')
        setLoading(false)
      }
    }

    fetchData()
  }, [url, pageNumber, pageSize, jwtToken])

  const memorizedResult = useMemo(
    () => ({
      data,
      loading,
      itemFrom,
      itemTo,
      totalItemsCount,
      totalPages,
      error,
    }),
    [data, loading, itemFrom, itemTo, totalItemsCount, totalPages, error],
  )

  return memorizedResult
}

export default useFetchPaginatedData
