import Sidebar from '../Sidebar/Sidebar'
import ChatWindow from './ChatWindow'

const ChatLayout: React.FC = () => {
  return (
    <div className='flex h-screen bg-gray-100'>
      <div className='w-[30%] bg-white border-r overflow-y-auto'>
        <Sidebar />
      </div>
      <div className='flex-1 bg-gray-50'>
        <ChatWindow />
      </div>
    </div>
  )
}

export default ChatLayout
