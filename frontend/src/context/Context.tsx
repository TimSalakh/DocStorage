import { DataToStore } from '../models/UserDataModels'
import { LoginInputs, RegisterInputs } from '../models/AuthInputsModels'
import { createContext, useEffect, useState } from 'react'
import { useNavigate } from 'react-router-dom'
import { registerApiCall, loginApiCall } from '../services/AuthService'
import React from 'react'
import axios, { AxiosResponse } from 'axios'

type UserContextType = {
  user: DataToStore | null
  registerUser: (inputs: RegisterInputs) => void
  loginUser: (inputs: LoginInputs) => void
  logout: () => void
  isLoggedIn: () => boolean
}

type Props = {
  children: React.ReactNode
}

const UserContext = createContext<UserContextType>({} as UserContextType)

export const UserProvider = ({ children }: Props) => {
  const navigate = useNavigate()
  const [user, setUser] = useState<DataToStore | null>(null)
  const [isLoaded, setIsLoaded] = useState<boolean>(false)

  useEffect(() => {
    const userDataString = localStorage.getItem('user')
    const userData: DataToStore = userDataString
      ? JSON.parse(userDataString)
      : {}
    if (userData) {
      setUser(userData)
      axios.defaults.headers.common['Authorization'] =
        'Bearer ' + userData!.token
      axios.defaults.headers.common['Access-Control-Allow-Origin'] = '*'
    }
    setIsLoaded(true)
  }, [])

  const loginUser = async (inputs: LoginInputs) => {
    await loginApiCall(inputs)
      .then((response) => {
        if (response?.status === 200) {
          handleResponse(response)
        }
      })
      .catch((error) => console.log(error))
  }

  const registerUser = async (inputs: RegisterInputs) => {
    await registerApiCall(inputs)
      .then((response) => {
        if (response?.status === 200) {
          handleResponse(response)
        }
      })
      .catch((error) => console.log(error))
  }

  const handleResponse = (response: AxiosResponse<DataToStore, any>): void => {
    localStorage.setItem('user', JSON.stringify(response.data))
    setUser(response.data)
    axios.defaults.headers.common['Authorization'] =
      'Bearer ' + response.data.token
    axios.defaults.headers.common['Access-Control-Allow-Origin'] = '*'
    //navigate(`/uid/${response.data.id}/inbox`)
  }

  const isLoggedIn = () => {
    return !!user?.token
  }

  const logout = () => {
    localStorage.removeItem('token')
    localStorage.removeItem('user')
    setUser(null)
    navigate('/login')
  }

  return (
    <UserContext.Provider
      value={{ loginUser, registerUser, user, isLoggedIn, logout }}
    >
      {isLoaded ? children : null}
    </UserContext.Provider>
  )
}

export const useAuth = () => React.useContext(UserContext)
