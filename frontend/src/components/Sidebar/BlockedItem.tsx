import BlockedUserSummaryResponse from '../../data/models/response/BlockedUserSummaryResponse'

interface BlockedItemProps {
  user: BlockedUserSummaryResponse
}

function BlockedItem({ user }: BlockedItemProps) {
  return (
    <div className='flex flex-row items-center justify-between'>
      <div className='flex flex-row items-center gap-4'>
        <img className='w-12 h-12 rounded-full' src={user.PictureUrl} alt={`${user.FirstName} ${user.LastName}`} />
        <div className='flex flex-col'>
          <p className='text-lg font-semibold'>
            {user.FirstName} {user.LastName}
          </p>
        </div>
      </div>
    </div>
  )
}

export default BlockedItem
