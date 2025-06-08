import React, { useEffect, useState } from 'react'
import ChatHeader from './ChatHeader'
import ChatInput from './ChatInput'
import ChatMessage from './ChatMessage'
import MessageService from '../../services/api/MessageService'
import ApiRepository from '../../data/api/ApiRepository'
import HttpApiProvider from '../../data/api/HttpApiProvider'
import MessageDetailsResponse from '../../data/models/response/MessageDetailsResponse'
import MessageCreateRequest from '../../data/models/request/MessageCreateRequest'
import JwtService from '../../services/api/JwtService'
import MessageType from '../../data/models/shared/MessageType'
import ConversationDetailsResponse from '../../data/models/response/ConversationDetailsResponse'
import UserService from '../../services/api/UserService'
import UserSummaryBaseResponse from '../../data/models/response/UserSummaryBaseResponse'

const apiRepository = new ApiRepository(new HttpApiProvider())
const messageService = new MessageService(apiRepository)
const userService = new UserService(apiRepository)

interface ChatWindowProps {
  conversation: ConversationDetailsResponse | null
}

const ChatWindow: React.FC<ChatWindowProps> = ({ conversation }) => {
  const [messages, setMessages] = useState<MessageDetailsResponse[]>([])
  const [sending, setSending] = useState(false)
  const [members, setMembers] = useState<UserSummaryBaseResponse[]>([])
  const currentUserId = JwtService.getUserId?.()

  useEffect(() => {
    if (conversation?.Id) {
      messageService.getByConversationId(conversation.Id).then(setMessages)
      userService.getByConversationId(conversation.Id).then(setMembers)
    } else {
      setMessages([])
      setMembers([])
    }
  }, [conversation?.Id])

  // Handle sending a message
  const handleSendMessage = async (content: string) => {
    if (!conversation?.Id || !content.trim()) return
    setSending(true)
    try {
      const req = new MessageCreateRequest(content, MessageType.TEXT, conversation.Id)
      const newMsg = await messageService.createMessage(req)
      setMessages((prev) => [...prev, newMsg])
    } catch (err) {
      console.error('Failed to send message:', err)
    } finally {
      setSending(false)
    }
  }

  return (
    <div className='flex flex-col h-full'>
      {/* Header */}
      <ChatHeader conversation={conversation} members={members} />

      {/* Messages */}
      <div className='flex-1 p-4 overflow-y-auto space-y-4'>
        {messages.map((msg) => (
          <ChatMessage
            key={msg.Id}
            sender={msg.Sender?.FirstName + ' ' + msg.Sender?.LastName}
            message={msg.Content}
            isOwn={msg.Sender?.Id === currentUserId}
          />
        ))}
      </div>

      {/* Input */}
      <ChatInput onSend={handleSendMessage} disabled={sending || !conversation?.Id} />
    </div>
  )
}

export default ChatWindow
