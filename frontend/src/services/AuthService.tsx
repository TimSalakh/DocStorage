import axios from 'axios'
import {
  ConfirmEmailInputs,
  LoginInputs,
  RegisterInputs
} from '../models/AuthInputsModels'
import { DataToStore } from '../models/UserModels'

const baseApiUrl: string = 'https://localhost:443/api/account'

const registerApiCall = async (inputs: RegisterInputs) => {
  try {
    console.log('данные улетели на бэкенд')
    const response = await axios.post(`${baseApiUrl}/register`, inputs, {
      headers: {
        'Access-Control-Allow-Origin': '*'
      }
    })
    return response
  } catch (error) {
    console.error(error)
  }
}

const loginApiCall = async (inputs: LoginInputs) => {
  try {
    const response = await axios.post(`${baseApiUrl}/login`, inputs, {
      headers: {
        'Access-Control-Allow-Origin': '*'
      }
    })
    return response
  } catch (error) {
    console.error(error)
  }
}

const confirmEmailApiCall = async (inputs: ConfirmEmailInputs) => {
  try {
    const response = await axios.post<DataToStore>(
      `${baseApiUrl}/confirm-email`,
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

export { registerApiCall, loginApiCall, confirmEmailApiCall }
