import FriendItem from './FriendItem'
import { friends } from '../../data/models/response/UserFriendSummaryResponse'

const Sidebar: React.FC = () => {
  return (
    <div className='p-4'>
      <input type='text' placeholder='Search' className='w-full p-2 rounded bg-gray-100 mb-4' />
      {friends.map((f) => (
        <FriendItem key={f.Id} friend={f} />
      ))}
    </div>
  )
}

export default Sidebar
