import React, { useState } from 'react'
import UserService from '../../services/api/UserService'
import ApiRepository from '../../data/api/ApiRepository'
import UserSummaryBaseResponse from '../../data/models/response/UserSummaryBaseResponse'
import HttpApiProvider from '../../data/api/HttpApiProvider'

const apiRepository = new ApiRepository(new HttpApiProvider())
const userService = new UserService(apiRepository)

const SearchBar: React.FC = () => {
  const [email, setEmail] = useState('')
  const [user, setUser] = useState<UserSummaryBaseResponse | null>(null)
  const [error, setError] = useState<string | null>(null)
  const [loading, setLoading] = useState(false)

  const handleSearch = async () => {
    setLoading(true)
    setError(null)
    setUser(null)
    try {
      const result = await userService.getUserByEmail(email)
      console.log('User found:', result)
      setUser(result)
    } catch (err) {
      console.error('Search error:', err)
      setError('User not found')
    } finally {
      setLoading(false)
    }
  }

  return (
    <div>
      <div className='flex mb-2'>
        <input
          type='text'
          placeholder='Search by email'
          className='flex-1 p-2 rounded bg-gray-100'
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />
        <button className='ml-2 px-4 py-2 bg-purple-600 text-white rounded' onClick={handleSearch} disabled={loading}>
          Search
        </button>
      </div>
      {loading && <div>Searching...</div>}
      {error && <div className='text-red-500'>{error}</div>}
      {user && (
        <div className='mt-2 p-2 border rounded bg-white'>
          <div>ID: {user.Id}</div>
          <div>
            Name: {user.FirstName} {user.LastName}
          </div>
          <div>Email: {user.Email}</div>
          <div>
            <img src={user.PictureUrl} alt='User' style={{ width: 48, height: 48, borderRadius: 24 }} />
          </div>
        </div>
      )}
    </div>
  )
}

export default SearchBar
