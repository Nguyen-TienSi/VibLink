import React, { useState, useEffect } from 'react'
import FriendItem from './FriendItem'
import BlockedItem from './BlockedItem'
import SearchBar from './SearchBar'
import SearchItem from './SearchItem'
import FriendshipRequestItem from './FriendshipRequestItem'
import ConversationItem from './ConversationItem'
import UserSummaryBaseResponse from '../../data/models/response/UserSummaryBaseResponse'
import UserService from '../../services/api/UserService'
import ApiRepository from '../../data/api/ApiRepository'
import HttpApiProvider from '../../data/api/HttpApiProvider'
import FriendshipService from '../../services/api/FriendshipService'
import BlockedUserSummaryResponse from '../../data/models/response/BlockedUserSummaryResponse'
import UserFriendSummaryResponse from '../../data/models/response/UserFriendSummaryResponse'
import FriendshipDetailsResponse from '../../data/models/response/FriendshipDetailsResponse'
import FriendshipRequestStatus from '../../data/models/shared/FriendshipRequestStatus'
import ConversationService from '../../services/api/ConversationService'
import ConversationCreateRequest from '../../data/models/request/ConversationCreateRequest'
import ConversationType from '../../data/models/shared/ConversationType'
import ConversationDetailsResponse from '../../data/models/response/ConversationDetailsResponse'
import JwtService from '../../services/api/JwtService'

const apiRepository = new ApiRepository(new HttpApiProvider())
const userService = new UserService(apiRepository)
const friendshipService = new FriendshipService(apiRepository)
const conversationService = new ConversationService(apiRepository)

interface SidebarProps {
  activeTab: 'chats' | 'friends' | 'requests'
  onSelectConversation?: (conversation: ConversationDetailsResponse) => void
  selectedConversationId?: string | null
}

const Sidebar: React.FC<SidebarProps> = ({ activeTab, onSelectConversation }) => {
  const [searchResult, setSearchResult] = useState<UserSummaryBaseResponse | null>(null)
  const [searchQuery, setSearchQuery] = useState('')
  const [searchError, setSearchError] = useState<string | null>(null)
  const [loading, setLoading] = useState(false)
  const [friendRequestLoading, setFriendRequestLoading] = useState(false)
  const [friendRequestSent, setFriendRequestSent] = useState(false)

  const [sidebarTab, setSidebarTab] = useState<'friends' | 'blocked'>('friends')
  const [friendsList, setFriendsList] = useState<UserFriendSummaryResponse[]>([])
  const [blockedUsers, setBlockedUsers] = useState<BlockedUserSummaryResponse[]>([])
  const [blockedLoading, setBlockedLoading] = useState(false)
  const [friendsLoading, setFriendsLoading] = useState(false)

  // Friendship requests state
  const [invites, setInvites] = useState<FriendshipDetailsResponse[]>([])
  const [invited, setInvited] = useState<FriendshipDetailsResponse[]>([])
  const [requestsLoading, setRequestsLoading] = useState(false)

  // Conversation state
  const [conversations, setConversations] = useState<ConversationDetailsResponse[]>([])
  const [selectedConversationId, setSelectedConversationId] = useState<string | null>(null)

  // Create conversation state
  const [showCreateModal, setShowCreateModal] = useState(false)
  const [chatName, setChatName] = useState('')
  const [participants, setParticipants] = useState<string>('') // comma-separated user IDs or emails
  const [creating, setCreating] = useState(false)
  const [error, setError] = useState<string | null>(null)
  const [selectedFriends, setSelectedFriends] = useState<UserFriendSummaryResponse[]>([])
  const [chatPicture, setChatPicture] = useState<File | null>(null)

  useEffect(() => {
    if (sidebarTab === 'friends') {
      setFriendsLoading(true)
      userService
        .getUserFriends()
        .then(setFriendsList)
        .catch(() => setFriendsList([]))
        .finally(() => setFriendsLoading(false))
    } else if (sidebarTab === 'blocked') {
      setBlockedLoading(true)
      userService
        .getBlockedUsers()
        .then(setBlockedUsers)
        .catch(() => setBlockedUsers([]))
        .finally(() => setBlockedLoading(false))
    }
  }, [sidebarTab])

  useEffect(() => {
    if (activeTab === 'requests') {
      setRequestsLoading(true)
      friendshipService
        .getInvites()
        .then((allInvites) => {
          const userId = JwtService.getUserId()
          if (!userId) {
            setInvites([])
            setInvited([])
            return
          }
          // Received: user is addressee
          setInvites(allInvites.filter((inv) => inv.Addressee?.Id === userId))
          // Sent: user is requester
          setInvited(allInvites.filter((inv) => inv.Requester?.Id === userId))
        })
        .catch(() => {
          setInvites([])
          setInvited([])
        })
        .finally(() => setRequestsLoading(false))
    }
  }, [activeTab])

  useEffect(() => {
    if (activeTab === 'chats') {
      conversationService
        .getByParticipant()
        .then(setConversations)
        .catch(() => setConversations([]))
    }
  }, [activeTab, showCreateModal])

  const handleSearch = async (query: string) => {
    setSearchQuery(query)
    setSearchError(null)
    setSearchResult(null)
    setFriendRequestSent(false)
    if (!query) return

    setLoading(true)
    try {
      const result = await userService.getByEmail(query)
      setSearchResult(result)
    } catch {
      setSearchResult(null)
      setSearchError('No users found.')
    } finally {
      setLoading(false)
    }
  }

  const handleAddFriend = async (user: UserSummaryBaseResponse) => {
    setFriendRequestLoading(true)
    try {
      await friendshipService.createFriendshipRequest(user.Id)
      setFriendRequestSent(true)
    } catch (error) {
      if (error instanceof Error) {
        alert('Failed to send friend request. ' + error.message)
      }
    } finally {
      setFriendRequestLoading(false)
    }
  }

  // Accept, reject, cancel handlers for requests
  const handleAccept = async (request: FriendshipDetailsResponse) => {
    await friendshipService.updateByAddressee(request.Requester.Id, FriendshipRequestStatus.ACCEPTED)
    setInvites(invites.filter((r) => r.Id !== request.Id))
  }
  const handleReject = async (request: FriendshipDetailsResponse) => {
    await friendshipService.updateByAddressee(request.Requester.Id, FriendshipRequestStatus.REJECTED)
    setInvites(invites.filter((r) => r.Id !== request.Id))
  }
  const handleCancel = async (request: FriendshipDetailsResponse) => {
    if (!request.Addressee || !request.Addressee.Id) {
      alert('Invalid request data')
      return
    }
    await friendshipService.updateByRequester(request.Addressee.Id, FriendshipRequestStatus.CANCELED)
    setInvited(invited.filter((r) => r.Id !== request.Id))
  }

  const handleParticipantInput = (e: React.ChangeEvent<HTMLInputElement>) => {
    setParticipants(e.target.value)
  }

  const filteredFriends = friendsList.filter(
    (f) =>
      `${f.FirstName} ${f.LastName}`.toLowerCase().includes(participants.toLowerCase()) &&
      !selectedFriends.some((sf) => sf.Id === f.Id)
  )

  const handleAddParticipant = (friend: UserFriendSummaryResponse) => {
    setSelectedFriends((prev) => [...prev, friend])
    setParticipants('') // clear input after adding
  }

  const handleRemoveParticipant = (id: string) => {
    setSelectedFriends((prev) => prev.filter((f) => f.Id !== id))
  }

  const handleChatPictureChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    if (e.target.files && e.target.files.length > 0) {
      setChatPicture(e.target.files[0])
    } else {
      setChatPicture(null)
    }
  }

  const handleCreateConversation = async (e: React.FormEvent) => {
    e.preventDefault()
    setCreating(true)
    setError(null)
    try {
      const participantList = selectedFriends.map((f) => f.Id)
      const req = new ConversationCreateRequest(chatName, chatPicture, ConversationType.GROUP, participantList)
      await conversationService.createConversation(req)
      setShowCreateModal(false)
      setChatName('')
      setParticipants('')
      setSelectedFriends([])
      setChatPicture(null)
    } catch (err) {
      if (err instanceof Error) {
        setError(err.message)
      } else {
        setError('Failed to create conversation')
      }
    } finally {
      setCreating(false)
    }
  }

  // Handler for clicking a friend to start a conversation
  const handleFriendClick = (friend: UserFriendSummaryResponse) => {
    setShowCreateModal(true)
    setSelectedFriends([friend])
    setParticipants('')
    setChatName(`${friend.FirstName} ${friend.LastName}`)
  }

  // Handler for selecting a conversation
  const handleConversationClick = (conversation: ConversationDetailsResponse) => {
    setSelectedConversationId(conversation.Id)
    if (onSelectConversation) onSelectConversation(conversation)
  }

  return (
    <div className='p-4 h-full'>
      {activeTab === 'chats' ? (
        <>
          <button
            className='mb-4 px-4 py-2 bg-purple-600 text-white rounded hover:bg-purple-700'
            onClick={() => setShowCreateModal(true)}
          >
            + New Conversation
          </button>
          {/* Conversation List */}
          <div className='mb-4'>
            {conversations.length === 0 ? (
              <div className='text-gray-500 p-4'>No conversations.</div>
            ) : (
              conversations.map((conv) => (
                <ConversationItem
                  key={conv.Id}
                  conversation={conv}
                  onClick={handleConversationClick}
                  selected={selectedConversationId === conv.Id}
                />
              ))
            )}
          </div>
          {/* Modal for creating conversation */}
          {showCreateModal && (
            <div className='fixed inset-0 bg-black bg-opacity-30 flex items-center justify-center z-50'>
              <form className='bg-white p-6 rounded shadow-md w-full max-w-sm' onSubmit={handleCreateConversation}>
                <h2 className='text-lg font-bold mb-4'>Create Conversation</h2>
                {error && <div className='text-red-500 mb-2'>{error}</div>}
                <div className='mb-2'>
                  <label className='block mb-1 font-medium'>Chat Name</label>
                  <input
                    className='w-full border p-2 rounded'
                    value={chatName}
                    onChange={(e) => setChatName(e.target.value)}
                    required
                  />
                </div>
                <div className='mb-2'>
                  <label className='block mb-1 font-medium'>Chat Picture</label>
                  <input
                    type='file'
                    accept='image/*'
                    onChange={handleChatPictureChange}
                    className='file:bg-blue-600 file:text-white file:rounded file:px-3 file:py-1 file:border-none file:mr-2'
                  />
                  {chatPicture && (
                    <div className='mt-2'>
                      <img
                        src={URL.createObjectURL(chatPicture)}
                        alt='Preview'
                        className='w-16 h-16 object-cover rounded border-2'
                      />
                    </div>
                  )}
                </div>
                <div className='mb-2'>
                  <label className='block mb-1 font-medium'>Add Participants</label>
                  <input
                    className='w-full border p-2 rounded'
                    value={participants}
                    onChange={handleParticipantInput}
                    placeholder='Type to search friends...'
                  />
                  {/* Show selected participants */}
                  <div className='flex flex-wrap gap-2 mt-2'>
                    {selectedFriends.map((f) => (
                      <span key={f.Id} className='bg-purple-200 px-2 py-1 rounded flex items-center'>
                        {f.FirstName} {f.LastName}
                        <button
                          type='button'
                          className='ml-1 text-red-500'
                          onClick={() => handleRemoveParticipant(f.Id)}
                        >
                          ×
                        </button>
                      </span>
                    ))}
                  </div>
                  {/* Show filtered friends to add */}
                  {participants && filteredFriends.length > 0 && (
                    <ul className='border rounded mt-2 max-h-32 overflow-y-auto bg-white z-10'>
                      {filteredFriends.map((f) => (
                        <li
                          key={f.Id}
                          className='p-2 hover:bg-purple-100 cursor-pointer'
                          onClick={() => handleAddParticipant(f)}
                        >
                          {f.FirstName} {f.LastName} ({f.Email})
                        </li>
                      ))}
                    </ul>
                  )}
                </div>
                <div className='flex gap-2 mt-4'>
                  <button
                    type='submit'
                    className='bg-purple-600 text-white px-4 py-2 rounded'
                    disabled={creating || selectedFriends.length === 0}
                  >
                    {creating ? 'Creating...' : 'Create'}
                  </button>
                  <button
                    type='button'
                    className='bg-gray-300 px-4 py-2 rounded'
                    onClick={() => {
                      setShowCreateModal(false)
                      setSelectedFriends([])
                      setParticipants('')
                      setChatName('')
                      setError(null)
                      setChatPicture(null)
                    }}
                  >
                    Cancel
                  </button>
                </div>
              </form>
            </div>
          )}
        </>
      ) : activeTab === 'friends' ? (
        <>
          <SearchBar onSearch={handleSearch} loading={loading} />
          <div className='flex mb-4'>
            <button
              className={`flex-1 py-2 rounded-l ${sidebarTab === 'friends' ? 'bg-purple-600 text-white' : 'bg-gray-200 text-gray-700'}`}
              onClick={() => setSidebarTab('friends')}
            >
              Friends
            </button>
            <button
              className={`flex-1 py-2 rounded-r ${sidebarTab === 'blocked' ? 'bg-purple-600 text-white' : 'bg-gray-200 text-gray-700'}`}
              onClick={() => setSidebarTab('blocked')}
            >
              Blocked
            </button>
          </div>
          {sidebarTab === 'friends' ? (
            searchQuery ? (
              searchResult ? (
                <SearchItem
                  user={searchResult}
                  onAddFriend={handleAddFriend}
                  friendRequestSent={friendRequestSent}
                  friendRequestLoading={friendRequestLoading}
                />
              ) : searchError ? (
                <div className='text-gray-500 p-4'>{searchError}</div>
              ) : null
            ) : friendsLoading ? (
              <div className='text-gray-500 p-4'>Loading friends...</div>
            ) : (
              friendsList.map((f) => <FriendItem key={f.Id} friend={f} onClick={handleFriendClick} />)
            )
          ) : blockedLoading ? (
            <div className='text-gray-500 p-4'>Loading blocked users...</div>
          ) : blockedUsers.length === 0 ? (
            <div className='text-gray-500 p-4'>No blocked users.</div>
          ) : (
            <ul>
              {blockedUsers.map((user) => (
                <li key={user.Id} className='p-2 border-b'>
                  <BlockedItem user={user} />
                </li>
              ))}
            </ul>
          )}
        </>
      ) : activeTab === 'requests' ? (
        <div>
          <h3 className='font-bold mb-2'>Invites</h3>
          {requestsLoading ? (
            <div className='text-gray-500 p-4'>Loading...</div>
          ) : invites.length === 0 ? (
            <div className='text-gray-500 p-4'>No invites.</div>
          ) : (
            invites.map((req) => (
              <FriendshipRequestItem
                key={req.Id}
                request={req}
                type='invites'
                onAccept={handleAccept}
                onReject={handleReject}
              />
            ))
          )}
          <h3 className='font-bold mt-4 mb-2'>Invited</h3>
          {requestsLoading ? (
            <div className='text-gray-500 p-4'>Loading...</div>
          ) : invited.length === 0 ? (
            <div className='text-gray-500 p-4'>No sent requests.</div>
          ) : (
            invited.map((req) => (
              <FriendshipRequestItem key={req.Id} request={req} type='invited' onCancel={handleCancel} />
            ))
          )}
        </div>
      ) : (
        <div className='text-gray-500 p-4'>Chats will be shown here.</div>
      )}
    </div>
  )
}

export default Sidebar
