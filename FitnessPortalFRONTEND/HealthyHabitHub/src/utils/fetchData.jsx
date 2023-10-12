import axios from 'axios'
import { apiUrl } from '../global/api_url'

const fetchData = async (
  url,
  queryParams = {},
  headers = {},
  method = 'get',
) => {
  const fullUrl = apiUrl + url
  try {
    const response = await axios({
      method,
      url: fullUrl,
      params: queryParams,
      headers,
    })
    return response.data
  } catch (error) {
    //console.log('error: ', error)
    if (error.response && error.response.status === 404) {
      throw error
    }
    if (error.response && error.response.status === 401) {
      throw error
    }
    throw new Error('An error occurred while fetching data.')
  }
}

export default fetchData
