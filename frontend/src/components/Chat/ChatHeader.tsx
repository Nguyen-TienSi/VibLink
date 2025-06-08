import { MagnifyingGlassIcon, PhoneIcon, EllipsisVerticalIcon } from '@heroicons/react/24/outline'
import ConversationDetailsResponse from '../../data/models/response/ConversationDetailsResponse'
import UserSummaryBaseResponse from '../../data/models/response/UserSummaryBaseResponse'

interface ChatHeaderProps {
  conversation: ConversationDetailsResponse | null
  members?: UserSummaryBaseResponse[]
}

const ChatHeader: React.FC<ChatHeaderProps> = ({ conversation, members }) => {
  if (!conversation) {
    return (
      <div className='flex justify-between items-center p-4 border-b bg-white'>
        <div>
          <h2 className='text-lg font-semibold text-gray-400'>No conversation selected</h2>
        </div>
      </div>
    )
  }

  const memberCount = members?.length ?? 0
  return (
    <div className='flex justify-between items-center p-4 border-b bg-white'>
      <div className='flex items-center gap-3'>
        {conversation.ChatPictureUrl && (
          <img
            src={conversation.ChatPictureUrl}
            alt={conversation.ChatName}
            className='w-10 h-10 rounded-full object-cover border'
          />
        )}
        <div>
          <h2 className='text-lg font-semibold'>{conversation.ChatName}</h2>
          <p className='text-sm text-gray-500'>
            {memberCount} member{memberCount !== 1 ? 's' : ''}
          </p>
        </div>
      </div>
      <div className='flex items-center space-x-4 text-gray-500'>
        <MagnifyingGlassIcon className='w-5 h-5 cursor-pointer' />
        <PhoneIcon className='w-5 h-5 cursor-pointer' />
        <EllipsisVerticalIcon className='w-5 h-5 cursor-pointer' />
      </div>
    </div>
  )
}

export default ChatHeader
