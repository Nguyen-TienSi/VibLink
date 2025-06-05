import UserFriendSummaryResponse from '../../data/models/response/UserFriendSummaryResponse'

interface FriendItemProps {
  friend: UserFriendSummaryResponse
}

const FriendItem: React.FC<FriendItemProps> = ({ friend }) => {
  return (
    <div className='flex items-center p-2 hover:bg-gray-100 rounded cursor-pointer'>
      <div className='w-10 h-10 bg-purple-200 rounded-full mr-3' />
      <div className='flex-1'>
        <p className='font-semibold'>
          {friend.firstName} {friend.lastName}
        </p>
        <p className='text-sm text-gray-500'>{friend.message}</p>
      </div>
      <span className='text-xs text-gray-400'>{friend.time}</span>
    </div>
  )
}

export default FriendItem
