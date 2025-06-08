import React from 'react'
import ConversationDetailsResponse from '../../data/models/response/ConversationDetailsResponse'

interface ConversationItemProps {
  conversation: ConversationDetailsResponse
  onClick?: (conversation: ConversationDetailsResponse) => void
  selected?: boolean
}

const ConversationItem: React.FC<ConversationItemProps> = ({ conversation, onClick, selected }) => {
  return (
    <div
      className={`flex items-center p-3 rounded cursor-pointer hover:bg-purple-100 transition ${selected ? 'bg-purple-200' : ''}`}
      onClick={() => onClick?.(conversation)}
    >
      <img
        src={conversation.ChatPictureUrl || '/default-group.png'}
        alt={conversation.ChatName}
        className='w-12 h-12 rounded-full object-cover mr-3 border'
      />
      {/* <div className='flex-1 min-w-0'>
        <div className='font-semibold truncate'>{conversation.ChatName}</div>
        <div className='text-xs text-gray-500 truncate'>
          {conversation.Participants.map((p) => p.FirstName + ' ' + p.LastName).join(', ')}
        </div>
      </div> */}
    </div>
  )
}

export default ConversationItem
