import React from 'react'
import FriendshipDetailsResponse from '../../data/models/response/FriendshipDetailsResponse'

interface FriendshipRequestItemProps {
  request: FriendshipDetailsResponse
  type: 'invites' | 'invited' // 'invites' = received, 'invited' = sent
  onAccept?: (request: FriendshipDetailsResponse) => void
  onReject?: (request: FriendshipDetailsResponse) => void
  onCancel?: (request: FriendshipDetailsResponse) => void
}

const FriendshipRequestItem: React.FC<FriendshipRequestItemProps> = ({
  request,
  type,
  onAccept,
  onReject,
  onCancel
}) => {
  const user = type === 'invites' ? request.Requester : request.Addressee

  return (
    <div className='flex items-center p-2 hover:bg-gray-100 rounded cursor-pointer'>
      <img
        className='w-10 h-10 rounded-full mr-3'
        src={user?.PictureUrl}
        alt={`${user?.FirstName} ${user?.LastName}`}
      />
      <div className='flex-1'>
        <p className='font-semibold'>
          {user?.FirstName} {user?.LastName}
        </p>
        <p className='text-xs text-gray-500'>{type === 'invites' ? 'Invited you' : 'You invited'}</p>
      </div>
      {type === 'invites' ? (
        <div className='flex gap-2'>
          <button
            className='px-2 py-1 bg-green-500 text-white rounded text-xs'
            onClick={(e) => {
              e.stopPropagation()
              onAccept?.(request)
            }}
          >
            Accept
          </button>
          <button
            className='px-2 py-1 bg-red-500 text-white rounded text-xs'
            onClick={(e) => {
              e.stopPropagation()
              onReject?.(request)
            }}
          >
            Reject
          </button>
        </div>
      ) : (
        <button
          className='px-2 py-1 bg-gray-400 text-white rounded text-xs'
          onClick={(e) => {
            e.stopPropagation()
            onCancel?.(request)
          }}
        >
          Cancel
        </button>
      )}
    </div>
  )
}

export default FriendshipRequestItem
