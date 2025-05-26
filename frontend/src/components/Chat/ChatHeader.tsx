import { MagnifyingGlassIcon, PhoneIcon, EllipsisVerticalIcon } from '@heroicons/react/24/outline'

const ChatHeader: React.FC = () => {
  return (
    <div className='flex justify-between items-center p-4 border-b bg-white'>
      <div>
        <h2 className='text-lg font-semibold'>Design chat</h2>
        <p className='text-sm text-gray-500'>23 members, 10 online</p>
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
