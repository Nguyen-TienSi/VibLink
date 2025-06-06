import FriendItem from './FriendItem'
import { friends } from '../../data/models/response/UserFriendSummaryResponse'
import SearchBar from './SearchBar'

const Sidebar: React.FC = () => {
  return (
    <div className='p-4'>
      <SearchBar />
      {friends.map((f) => (
        <FriendItem key={f.Id} friend={f} />
      ))}
    </div>
  )
}

export default Sidebar
