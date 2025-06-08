import ApiRepository from '../../data/api/ApiRepository'
import FriendshipDetailsResponse from '../../data/models/response/FriendshipDetailsResponse'
import FriendshipRequestStatus from '../../data/models/shared/FriendshipRequestStatus'

export default class FriendshipService {
  constructor(private readonly apiRepository: ApiRepository) {}

  async getInvites(): Promise<FriendshipDetailsResponse[]> {
    const data = await this.apiRepository.get<Record<string, unknown>[]>('/api/Friendship/invites')
    return data.map((item) => FriendshipDetailsResponse.fromJson(item))
  }

  async createFriendshipRequest(addresseeId: string): Promise<FriendshipDetailsResponse> {
    const data = await this.apiRepository.post<Record<string, unknown>>(`/api/Friendship/${addresseeId}`)
    return FriendshipDetailsResponse.fromJson(data)
  }

  async updateByRequester(addresseeId: string, status: FriendshipRequestStatus): Promise<FriendshipDetailsResponse> {
    const data = await this.apiRepository.put<Record<string, unknown>>(
      `/api/Friendship/requester/${addresseeId}`,
      undefined,
      { status: status }
    )
    return FriendshipDetailsResponse.fromJson(data)
  }

  async updateByAddressee(requesterId: string, status: FriendshipRequestStatus): Promise<FriendshipDetailsResponse> {
    const data = await this.apiRepository.put<Record<string, unknown>>(
      `/api/Friendship/addressee/${requesterId}`,
      undefined,
      { status: status }
    )
    return FriendshipDetailsResponse.fromJson(data)
  }
}
