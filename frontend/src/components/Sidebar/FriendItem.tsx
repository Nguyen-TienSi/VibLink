import UserFriendSummaryResponse from '../../data/models/response/UserFriendSummaryResponse'

interface FriendItemProps {
  friend: UserFriendSummaryResponse
  onClick?: (friend: UserFriendSummaryResponse) => void
}

const FriendItem: React.FC<FriendItemProps> = ({ friend, onClick }) => {
  return (
    <div className='flex items-center p-2 hover:bg-gray-100 rounded cursor-pointer' onClick={() => onClick?.(friend)}>
      <div className='w-10 h-10 bg-purple-200 rounded-full mr-3' />
      <div className='flex-1'>
        <p className='font-semibold'>
          {friend.FirstName} {friend.LastName}
        </p>
      </div>
    </div>
  )
}

export default FriendItem
