import { DataToStore, Role, TokenPayload } from '../models/UserModels'
import {
  ConfirmEmailInputs,
  LoginInputs,
  RegisterInputs
} from '../models/AuthInputsModels'
import { createContext, useEffect, useState } from 'react'
import { useNavigate } from 'react-router-dom'
import {
  registerApiCall,
  loginApiCall,
  confirmEmailApiCall
} from '../services/AuthService'
import React from 'react'
import axios, { AxiosResponse } from 'axios'
import { jwtDecode } from 'jwt-decode'

type UserContextType = {
  user: DataToStore | null
  register: (inputs: RegisterInputs) => void
  login: (inputs: LoginInputs) => void
  confirmEmail: (inputs: ConfirmEmailInputs) => void
  logout: () => void
  isLoggedIn: () => boolean
  currentRole: () => Role
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
    const rawUserData = localStorage.getItem('user')
    const userData: DataToStore = rawUserData ? JSON.parse(rawUserData) : {}
    if (userData) {
      setUser(userData)
      axios.defaults.headers.common['Authorization'] =
        'Bearer ' + userData!.token
      axios.defaults.headers.common['Access-Control-Allow-Origin'] = '*'
    }
    setIsLoaded(true)
  }, [])

  const login = async (inputs: LoginInputs) => {
    try {
      const response = await loginApiCall(inputs)
      if (response?.status === 200) {
        localStorage.setItem('user-email', inputs.email)
        navigate('/confirm-email')
      } else {
        console.log(
          'Error message from server:',
          JSON.parse(response?.data.error)
        )
        return response?.data.Error
      }
    } catch (error: any) {
      console.error('Login failed:', error)
    }
  }

  const register = async (inputs: RegisterInputs) => {
    await registerApiCall(inputs)
      .then((response) => {
        if (response?.status === 200) {
          localStorage.setItem('user-email', inputs.email)
          navigate('/confirm-email')
        }
        return response
      })
      .catch((error) => console.log(error))
  }

  const confrimEmail = async (inputs: ConfirmEmailInputs) => {
    await confirmEmailApiCall(inputs)
      .then((response) => {
        if (response?.status === 200) {
          handleResponse(response)
        }
      })
      .catch((error) => console.log(error))
  }

  const handleResponse = (response: AxiosResponse<DataToStore, any>) => {
    localStorage.setItem('user', JSON.stringify(response.data))
    setUser(response.data)

    axios.defaults.headers.common['Authorization'] =
      'Bearer ' + response.data.token
    axios.defaults.headers.common['Access-Control-Allow-Origin'] = '*'

    const role = currentRole()
    if (role === Role.Student) {
      navigate(`/student-dashboard`)
    } else {
      navigate(`/teacher-dashboard`)
    }
  }

  const isLoggedIn = () => {
    return !!user?.token
  }

  const logout = () => {
    axios.defaults.headers.common['Authorization'] = ''
    localStorage.removeItem('user')
    setUser(null)
    navigate('/login')
  }

  const currentRole = () => {
    const decodedToken: TokenPayload = jwtDecode<TokenPayload>(user?.token!)
    return decodedToken.role as Role
  }

  return (
    <UserContext.Provider
      value={{
        user,
        login: login,
        register: register,
        confirmEmail: confrimEmail,
        isLoggedIn,
        logout,
        currentRole
      }}
    >
      {isLoaded ? children : null}
    </UserContext.Provider>
  )
}

export const useAuth = () => React.useContext(UserContext)
