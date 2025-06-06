import ApiRepository from '../../data/api/ApiRepository'
import UserDetailsResponse from '../../data/models/response/UserDetailsResponse'
import UserSummaryBaseResponse from '../../data/models/response/UserSummaryBaseResponse'

export default class UserService {
  constructor(private readonly apiRepository: ApiRepository) {}

  async getUserProfile(): Promise<UserDetailsResponse> {
    return await this.apiRepository.get<UserDetailsResponse>('/api/user/profile')
  }

  async getUserByEmail(email: string): Promise<UserSummaryBaseResponse> {
    const data = await this.apiRepository.get<Record<string, unknown>>(`/api/UserDetails/by-email/${email}`)
    return UserSummaryBaseResponse.fromJson(data)
  }
}
