import React from 'react'
import UserSummaryBaseResponse from '../../data/models/response/UserSummaryBaseResponse'

interface SearchItemProps {
  user: UserSummaryBaseResponse
  onAddFriend?: (user: UserSummaryBaseResponse) => void
  friendRequestSent?: boolean
  friendRequestLoading?: boolean
}

const SearchItem: React.FC<SearchItemProps> = ({ user, onAddFriend, friendRequestSent, friendRequestLoading }) => {
  return (
    <div className='flex items-center p-2 hover:bg-gray-100 rounded cursor-pointer'>
      {user.PictureUrl ? (
        <img
          src={user.PictureUrl}
          alt={`${user.FirstName} ${user.LastName}`}
          className='w-10 h-10 rounded-full object-cover mr-3'
        />
      ) : (
        <div className='w-10 h-10 bg-purple-200 rounded-full mr-3 flex items-center justify-center text-lg font-bold text-white'>
          {user.FirstName[0]}
        </div>
      )}
      <div className='flex-1 min-w-0'>
        <div className='font-medium truncate'>
          {user.FirstName} {user.LastName}
        </div>
        <div className='text-sm text-gray-500 truncate'>{user.Email}</div>
      </div>
      <button
        type='button'
        onClick={(e) => {
          e.stopPropagation()
          if (onAddFriend && !friendRequestSent && !friendRequestLoading) {
            onAddFriend(user)
          }
        }}
        className={`ml-auto px-3 py-1 rounded-full text-lg font-bold transition ${
          friendRequestSent
            ? 'bg-green-500 text-white cursor-default'
            : 'bg-blue-500 text-white opacity-80 hover:opacity-100'
        }`}
        title='Add friend'
        disabled={friendRequestSent || friendRequestLoading}
      >
        {friendRequestSent ? '✓' : friendRequestLoading ? '...' : '+'}
      </button>
    </div>
  )
}

export default SearchItem
