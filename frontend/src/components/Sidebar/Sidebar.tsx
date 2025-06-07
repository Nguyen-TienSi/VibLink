import React, { useState } from 'react'
import FriendItem from './FriendItem'
import { friends } from '../../data/models/response/UserFriendSummaryResponse'
import SearchBar from './SearchBar'
import SearchItem from './SearchItem'
import UserSummaryBaseResponse from '../../data/models/response/UserSummaryBaseResponse'
import UserService from '../../services/api/UserService'
import ApiRepository from '../../data/api/ApiRepository'
import HttpApiProvider from '../../data/api/HttpApiProvider'
import FriendshipService from '../../services/api/FriendshipService'

const apiRepository = new ApiRepository(new HttpApiProvider())
const userService = new UserService(apiRepository)
const friendshipService = new FriendshipService(apiRepository)

const Sidebar: React.FC = () => {
  const [searchResult, setSearchResult] = useState<UserSummaryBaseResponse | null>(null)
  const [searchQuery, setSearchQuery] = useState('')
  const [searchError, setSearchError] = useState<string | null>(null)
  const [loading, setLoading] = useState(false)
  const [friendRequestLoading, setFriendRequestLoading] = useState(false)
  const [friendRequestSent, setFriendRequestSent] = useState(false)

  const handleSearch = async (query: string) => {
    setSearchQuery(query)
    setSearchError(null)
    setSearchResult(null)
    setFriendRequestSent(false)
    if (!query) return

    setLoading(true)
    try {
      const result = await userService.getUserByEmail(query)
      setSearchResult(result)
    } catch {
      setSearchResult(null)
      setSearchError('No users found.')
    } finally {
      setLoading(false)
    }
  }

  const handleAddFriend = async (user: UserSummaryBaseResponse) => {
    console.log('Add friend user:', user)
    if (!user.Id) {
      alert('User ID is invalid.')
      return
    }
    setFriendRequestLoading(true)
    try {
      await friendshipService.createFriendshipRequest(user.Id)
      setFriendRequestSent(true)
    } catch (error) {
      console.error('Failed to send friend request:', error)
      alert('Failed to send friend request.')
    } finally {
      setFriendRequestLoading(false)
    }
  }

  return (
    <div className='p-4'>
      <SearchBar onSearch={handleSearch} loading={loading} />
      {searchQuery ? (
        searchResult ? (
          <SearchItem
            user={searchResult}
            onAddFriend={handleAddFriend}
            friendRequestSent={friendRequestSent}
            friendRequestLoading={friendRequestLoading}
          />
        ) : searchError ? (
          <div className='text-gray-500 p-4'>{searchError}</div>
        ) : null
      ) : (
        friends.map((f) => <FriendItem key={f.Id} friend={f} />)
      )}
    </div>
  )
}

export default Sidebar
