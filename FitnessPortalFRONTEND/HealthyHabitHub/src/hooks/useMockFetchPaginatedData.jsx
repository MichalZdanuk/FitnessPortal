import { useEffect, useState, useMemo } from 'react'

const useMockFetchPaginatedData = (mockData) => {
  const [data, setData] = useState(null)
  const [loading, setLoading] = useState(true)
  const [itemFrom, setItemFrom] = useState(0)
  const [itemTo, setItemTo] = useState(0)
  const [totalItemsCount, setTotalItemsCount] = useState(0)
  const [totalPages, setTotalPages] = useState(0)

  useEffect(() => {
    // Simulate an asynchronous request with a delay
    const fetchData = async () => {
      try {
        // Simulate a delay (you can adjust the delay time)
        await new Promise((resolve) => setTimeout(resolve, 1000))

        // Use the provided mock data instead of the global variable
        const response = mockData

        setData(response.items)
        setItemFrom(response.itemFrom)
        setItemTo(response.itemTo)
        setTotalItemsCount(response.totalItemsCount)
        setTotalPages(response.totalPages)
        setLoading(false)
      } catch (error) {
        console.error(error)
      }
    }

    fetchData()
  }, [mockData])

  const memorizedResult = useMemo(
    () => ({
      data,
      loading,
      itemFrom,
      itemTo,
      totalItemsCount,
      totalPages,
    }),
    [data, loading, itemFrom, itemTo, totalItemsCount, totalPages],
  )

  return memorizedResult
}

export default useMockFetchPaginatedData
