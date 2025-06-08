import React from 'react'

interface SidebarNavIconProps {
  icon: React.ReactNode
  label: string
  active?: boolean
  onClick?: () => void
}

const SidebarNavIcon: React.FC<SidebarNavIconProps> = ({ icon, label, active, onClick }) => (
  <button
    className={`flex flex-col items-center p-3 my-2 rounded hover:bg-gray-200 ${active ? 'bg-gray-300' : ''}`}
    title={label}
    onClick={onClick}
    type='button'
  >
    <span className='text-2xl'>{icon}</span>
    <span className='text-xs mt-1'>{label}</span>
  </button>
)

export default SidebarNavIcon
