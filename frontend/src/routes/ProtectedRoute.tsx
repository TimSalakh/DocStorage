import React from 'react'
import { Navigate, useLocation } from 'react-router-dom'
import { useAuth } from '../context/Context'
import { Role } from '../models/UserModels'

type Props = {
  children: React.ReactNode
  role: Role
}

const ProtectedRoute = ({ children, role }: Props) => {
  const location = useLocation()
  const { isLoggedIn, currentRole } = useAuth()

  if (role !== currentRole()) {
    return (
      <Navigate to='/permission-denied' state={{ from: location }} replace />
    )
  }

  return isLoggedIn() ? (
    <>{children}</>
  ) : (
    <Navigate to='/login' state={{ from: location }} replace />
  )
}

export default ProtectedRoute
