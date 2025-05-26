import ChatHeader from './ChatHeader'
import ChatInput from './ChatInput'
import ChatMessage from './ChatMessage'

const ChatWindow: React.FC = () => {
  return (
    <div className='flex flex-col h-full'>
      {/* Header */}
      <ChatHeader />

      {/* Messages */}
      <div className='flex-1 p-4 overflow-y-auto space-y-4'>
        <ChatMessage sender='Jasmin Lowery' message='I added new flows...' />
        <ChatMessage message='Jaden, congratulations! 🎉' isOwn />
      </div>

      {/* Input */}
      <ChatInput />
    </div>
  )
}

export default ChatWindow
