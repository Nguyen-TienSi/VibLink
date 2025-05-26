// src/router/index.tsx
import { createBrowserRouter } from 'react-router-dom'
import Home from '../pages/Home'
import Login from '../pages/Login'
import ChatLayout from '../components/Chat/ChatLayout'

export const router = createBrowserRouter([
  {
    path: '/',
    element: <Home />
  },
  {
    path: '/login',
    element: <Login />
  },
  {
    path: '/chat',
    element: <ChatLayout />
  }
])
