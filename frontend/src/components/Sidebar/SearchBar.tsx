import React, { useState } from 'react'

interface SearchBarProps {
  onSearch: (query: string) => void | Promise<void>
  loading?: boolean
}

const SearchBar: React.FC<SearchBarProps> = ({ onSearch, loading }) => {
  const [query, setQuery] = useState('')

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setQuery(e.target.value)
    onSearch(e.target.value)
  }

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault()
    onSearch(query)
  }

  return (
    <form onSubmit={handleSubmit} className='flex mb-2'>
      <input
        type='text'
        placeholder='Search by email'
        className='flex-1 p-2 rounded bg-gray-100 border border-black'
        value={query}
        onChange={handleChange}
      />
      <button className='ml-2 px-4 py-2 bg-purple-600 text-white rounded' type='submit' disabled={loading}>
        Search
      </button>
    </form>
  )
}

export default SearchBar
