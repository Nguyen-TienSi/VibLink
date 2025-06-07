import React, { useState } from 'react'
import UserRegisterRequest from '../../data/models/request/UserRegisterRequest'
import AuthService from '../../services/api/AuthService'
import ApiRepository from '../../data/api/ApiRepository'
import HttpApiProvider from '../../data/api/HttpApiProvider'

interface RegisterFormProps {
  onSuccess?: () => void
}

const RegisterForm: React.FC<RegisterFormProps> = ({ onSuccess }) => {
  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')
  const [firstName, setFirstName] = useState('')
  const [lastName, setLastName] = useState('')
  const [picture, setPicture] = useState<File | null>(null)
  const [error, setError] = useState<string | null>(null)
  const [loading, setLoading] = useState(false)

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()
    setLoading(true)
    setError(null)
    try {
      const apiRepo = new ApiRepository(new HttpApiProvider())
      const authService = new AuthService(apiRepo)
      const req = new UserRegisterRequest(email, password, firstName, lastName, picture)
      await authService.register(req)
      if (onSuccess) onSuccess()
    } catch (err: unknown) {
      if (err instanceof Error) {
        setError(err.message)
      } else {
        setError('Registration failed')
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
      <h2 className='text-2xl font-semibold text-center mb-2'>Register</h2>
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
          Password:
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
      <div className='flex flex-col gap-1'>
        <label htmlFor='firstName' className='font-medium'>
          First Name:
        </label>
        <input
          id='firstName'
          value={firstName}
          onChange={(e) => setFirstName(e.target.value)}
          required
          className='px-3 py-2 border border-gray-300 rounded focus:outline-none focus:ring-2 focus:ring-blue-400'
        />
      </div>
      <div className='flex flex-col gap-1'>
        <label htmlFor='lastName' className='font-medium'>
          Last Name:
        </label>
        <input
          id='lastName'
          value={lastName}
          onChange={(e) => setLastName(e.target.value)}
          required
          className='px-3 py-2 border border-gray-300 rounded focus:outline-none focus:ring-2 focus:ring-blue-400'
        />
      </div>
      <div className='flex flex-col gap-1'>
        <label htmlFor='picture' className='font-medium'>
          Profile Picture:
        </label>
        <input
          id='picture'
          type='file'
          accept='image/*'
          onChange={(e) => setPicture(e.target.files && e.target.files[0] ? e.target.files[0] : null)}
          className='file:mr-4 file:py-2 file:px-4 file:rounded file:border-0 file:text-sm file:font-semibold file:bg-blue-50 file:text-blue-700 hover:file:bg-blue-100'
        />
      </div>
      <button
        type='submit'
        disabled={loading}
        className='mt-2 py-2 px-4 bg-blue-600 text-white rounded hover:bg-blue-700 transition-colors disabled:opacity-60'
      >
        {loading ? 'Registering...' : 'Register'}
      </button>
    </form>
  )
}

export default RegisterForm
