export interface FriendData {
  id: number
  name: string
  message: string
  time: string
}

export const friends: FriendData[] = [
  { id: 1, name: 'Osman Campos', message: 'You: Hey! We are ready...', time: '20m' },
  { id: 2, name: 'Jasmin Lowery', message: 'You: Let’s discuss...', time: '1h' },
  { id: 3, name: 'Anthony Cordanes', message: 'What do you think?', time: '1d' }
]
