import axios from 'axios'
import { LoginInputs, RegisterInputs } from '../models/AuthInputsModels'
import { DataToStore } from '../models/UserDataModels'

const baseApiUrl: string = 'https://localhost:8888/api/account'

const registerApiCall = async (inputs: RegisterInputs) => {
  try {
    console.log('данные улетели на бэкенд')
    const response = await axios.post<DataToStore>(
      `${baseApiUrl}/register`,
      inputs,
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

const loginApiCall = async (inputs: LoginInputs) => {
  try {
    const response = await axios.post<DataToStore>(
      `${baseApiUrl}/login`,
      inputs,
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

export { registerApiCall, loginApiCall }
