import ApiRepository from '../../data/api/ApiRepository'
import FriendshipDetailsResponse from '../../data/models/response/FriendshipDetailsResponse'

export default class FriendshipService {
  constructor(private readonly apiRepository: ApiRepository) {}

  async getFriendshipRequests(): Promise<FriendshipDetailsResponse[]> {
    return await this.apiRepository.get<FriendshipDetailsResponse[]>('/friendships')
  }

  async createFriendshipRequest(addresseeId: string): Promise<FriendshipDetailsResponse> {
    const data = await this.apiRepository.post<Record<string, unknown>>(`/api/Friendship/${addresseeId}`)
    return FriendshipDetailsResponse.fromJson(data)
  }
}
