interface ChatMessageProps {
  sender?: string
  message: string
  isOwn?: boolean
}

const ChatMessage: React.FC<ChatMessageProps> = ({ sender, message, isOwn }) => {
  return (
    <div
      className={`p-3 rounded shadow w-fit ${isOwn ? 'ml-auto bg-purple-100 text-gray-800' : 'bg-white text-gray-700'}`}
    >
      {!isOwn && sender && <p className='text-sm font-semibold text-gray-900'>{sender}</p>}
      <p>{message}</p>
    </div>
  )
}

export default ChatMessage
