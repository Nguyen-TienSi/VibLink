import React, { useState } from 'react'
import Sidebar from '../components/Sidebar/Sidebar'
import SidebarNav from '../components/Sidebar/SidebarNav'
import ChatWindow from '../components/Chat/ChatWindow'
import ConversationDetailsResponse from '../data/models/response/ConversationDetailsResponse'

const MainLayout: React.FC = () => {
  const [activeTab, setActiveTab] = useState<'chats' | 'friends' | 'requests'>('chats')
  const [selectedConversation, setSelectedConversation] = useState<ConversationDetailsResponse | null>(null)

  return (
    <div className='flex h-screen bg-gray-100'>
      {/* SidebarNav on the far left */}
      <SidebarNav activeTab={activeTab} onTabChange={setActiveTab} />
      {/* Sidebar content */}
      <div className='w-[30%] bg-white border-r overflow-y-auto'>
        <Sidebar
          activeTab={activeTab}
          onSelectConversation={setSelectedConversation}
          selectedConversationId={selectedConversation?.Id}
        />
      </div>
      <div className='flex-1 bg-gray-50'>
        <ChatWindow conversation={selectedConversation} />
      </div>
    </div>
  )
}

export default MainLayout
