const ChatInput: React.FC = () => {
  return (
    <div className='p-4 border-t bg-white'>
      <div className='flex items-center'>
        <input type='text' placeholder='Your message' className='flex-1 p-2 rounded bg-gray-100' />
        <button className='ml-3 text-purple-600 font-bold'>Send</button>
      </div>
    </div>
  )
}

export default ChatInput
