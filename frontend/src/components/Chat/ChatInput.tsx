import React, { useState } from 'react'

interface ChatInputProps {
  onSend?: (message: string) => void
  disabled?: boolean
}

const ChatInput: React.FC<ChatInputProps> = ({ onSend, disabled }) => {
  const [value, setValue] = useState('')

  const handleSend = (e: React.FormEvent) => {
    e.preventDefault()
    if (onSend && value.trim()) {
      onSend(value)
      setValue('')
    }
  }

  return (
    <form className='p-4 border-t bg-white' onSubmit={handleSend}>
      <div className='flex items-center'>
        <input
          type='text'
          placeholder='Your message'
          className='flex-1 p-2 rounded bg-gray-100'
          value={value}
          onChange={(e) => setValue(e.target.value)}
          disabled={disabled}
        />
        <button className='ml-3 text-purple-600 font-bold' type='submit' disabled={disabled || !value.trim()}>
          Send
        </button>
      </div>
    </form>
  )
}

export default ChatInput
