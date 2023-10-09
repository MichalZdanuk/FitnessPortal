const fetchMockedData = async (mockData) => {
  try {
    // Simulate a delay to mimic an API call
    await new Promise((resolve) => setTimeout(resolve, 1000))

    return mockData
  } catch (error) {
    throw new Error('An error occurred while fetching mocked data.')
  }
}

export default fetchMockedData
