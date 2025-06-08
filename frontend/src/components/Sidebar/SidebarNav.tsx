import React from 'react'
import SidebarNavIcon from './SidebarNavIcon'
import { FaComments, FaUserFriends, FaUserPlus, FaSignOutAlt } from 'react-icons/fa'
import AuthService from '../../services/api/AuthService'
import ApiRepository from '../../data/api/ApiRepository'
import HttpApiProvider from '../../data/api/HttpApiProvider'
import RefreshTokenRequest from '../../data/models/request/RefreshTokenRequest'
import { useNavigate } from 'react-router-dom'

interface SidebarNavProps {
  activeTab: 'chats' | 'friends' | 'requests'
  onTabChange: (tab: 'chats' | 'friends' | 'requests') => void
}

const SidebarNav: React.FC<SidebarNavProps> = ({ activeTab, onTabChange }) => {
  const navigate = useNavigate()

  const handleLogout = async () => {
    const apiRepository = new ApiRepository(new HttpApiProvider())
    const authService = new AuthService(apiRepository)
    const refreshToken = sessionStorage.getItem('refreshToken')
    if (refreshToken) {
      try {
        await authService.logout(new RefreshTokenRequest(refreshToken))
      } catch (e) {
        console.error('Logout failed:', e)
      }
    }
    navigate('/login')
  }

  return (
    <div className='flex flex-col items-center bg-gray-100 h-full py-4 w-16 border-r'>
      <SidebarNavIcon
        icon={<FaComments />}
        label='Chats'
        active={activeTab === 'chats'}
        onClick={() => onTabChange('chats')}
      />
      <SidebarNavIcon
        icon={<FaUserFriends />}
        label='Friends'
        active={activeTab === 'friends'}
        onClick={() => onTabChange('friends')}
      />
      <SidebarNavIcon
        icon={<FaUserPlus />}
        label='Requests'
        active={activeTab === 'requests'}
        onClick={() => onTabChange('requests')}
      />
      <div className='flex-1' />
      <SidebarNavIcon icon={<FaSignOutAlt />} label='Logout' onClick={handleLogout} />
    </div>
  )
}

export default SidebarNav
