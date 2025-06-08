import React, { useState } from 'react'
import LoginRequest from '../../data/models/request/LoginRequest'
import AuthService from '../../services/api/AuthService'
import ApiRepository from '../../data/api/ApiRepository'
import HttpApiProvider from '../../data/api/HttpApiProvider'

interface LoginFormProps {
  onSuccess?: () => void
}

const LoginForm: React.FC<LoginFormProps> = ({ onSuccess }) => {
  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')
  const [error, setError] = useState<string | null>(null)
  const [loading, setLoading] = useState(false)

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()
    setLoading(true)
    setError(null)
    try {
      const apiRepo = new ApiRepository(new HttpApiProvider())
      const authService = new AuthService(apiRepo)
      const req = new LoginRequest(email, password)
      await authService.login(req)
      if (onSuccess) onSuccess()
    } catch (err: unknown) {
      if (err instanceof Error) {
        setError(err.message || 'Login failed')
      } else {
        setError('Login failed')
      }
    } finally {
      setLoading(false)
    }
  }

  return (
    <form
      onSubmit={handleSubmit}
      className='max-w-md mx-auto mt-10 bg-white border border-gray-200 rounded-lg shadow-md p-8 flex flex-col gap-6'
    >
      <h2 className='text-2xl font-semibold text-center mb-2'>Đăng nhập</h2>
      {error && <div className='text-red-500 text-center'>{error}</div>}
      <div className='flex flex-col gap-1'>
        <label htmlFor='email' className='font-medium'>
          Email:
        </label>
        <input
          id='email'
          type='email'
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          required
          className='px-3 py-2 border border-gray-300 rounded focus:outline-none focus:ring-2 focus:ring-blue-400'
        />
      </div>
      <div className='flex flex-col gap-1'>
        <label htmlFor='password' className='font-medium'>
          Mật khẩu:
        </label>
        <input
          id='password'
          type='password'
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          required
          className='px-3 py-2 border border-gray-300 rounded focus:outline-none focus:ring-2 focus:ring-blue-400'
        />
      </div>
      <button
        type='submit'
        disabled={loading}
        className='mt-2 py-2 px-4 bg-blue-600 text-white rounded hover:bg-blue-700 transition-colors disabled:opacity-60'
      >
        {loading ? 'Đang đăng nhập...' : 'Đăng nhập'}
      </button>
      <div className='text-center mt-2'>
        Chưa có tài khoản?{' '}
        <a href='/register' className='text-blue-600 hover:underline'>
          Đăng ký
        </a>
      </div>
    </form>
  )
}

export default LoginForm
