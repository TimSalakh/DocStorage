import axios from 'axios'
import { PublicationType } from '../models/PublicationModels'

const baseApiUrl: string = 'https://localhost:443/api/publication'

const fetchPublicationsApiCall = async () => {
  try {
    console.log('данные улетели на бэкенд')
    const response = await axios.get<PublicationType[]>(
      `${baseApiUrl}/publications`,
      {
        headers: {
          'Access-Control-Allow-Origin': '*'
        }
      }
    )
    return response
  } catch (error) {
    console.error(error)
  }
}

export { fetchPublicationsApiCall }
