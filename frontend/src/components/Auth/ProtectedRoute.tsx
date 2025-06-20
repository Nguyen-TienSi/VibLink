import React from 'react'
import { Navigate } from 'react-router-dom'

const ProtectedRoute: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const token = sessionStorage.getItem('accessToken')
  if (!token) {
    return <Navigate to='/login' replace />
  }
  return <>{children}</>
}

export default ProtectedRoute
