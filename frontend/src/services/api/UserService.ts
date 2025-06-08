import ApiRepository from '../../data/api/ApiRepository'
import BlockedUserSummaryResponse from '../../data/models/response/BlockedUserSummaryResponse'
import UserDetailsResponse from '../../data/models/response/UserDetailsResponse'
import UserFriendSummaryResponse from '../../data/models/response/UserFriendSummaryResponse'
import UserSummaryBaseResponse from '../../data/models/response/UserSummaryBaseResponse'

export default class UserService {
  constructor(private readonly apiRepository: ApiRepository) {}

  async getUserDetails(): Promise<UserDetailsResponse> {
    const data = await this.apiRepository.get<Record<string, unknown>>('/api/UserDetails/profile')
    return UserDetailsResponse.fromJson(data)
  }

  async getByEmail(email: string): Promise<UserSummaryBaseResponse> {
    const data = await this.apiRepository.get<Record<string, unknown>>(`/api/UserDetails/by-email/${email}`)
    return UserSummaryBaseResponse.fromJson(data)
  }

  async getUserFriends(): Promise<UserFriendSummaryResponse[]> {
    const data = await this.apiRepository.get<Record<string, unknown>[]>('/api/UserDetails/friends')
    return data.map((item) => UserFriendSummaryResponse.fromJson(item))
  }

  async getBlockedUsers(): Promise<BlockedUserSummaryResponse[]> {
    const data = await this.apiRepository.get<Record<string, unknown>[]>('/api/UserDetails/blocked')
    return data.map((item) => BlockedUserSummaryResponse.fromJson(item))
  }

  async getByConversationId(conversationId: string): Promise<UserSummaryBaseResponse[]> {
    const data = await this.apiRepository.get<Record<string, unknown>[]>(
      `/api/UserDetails/by-conversation/${conversationId}`
    )
    return data.map((item) => UserSummaryBaseResponse.fromJson(item))
  }
}
