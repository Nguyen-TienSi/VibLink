import ApiRepository from '../../data/api/ApiRepository'
import UserDetailsResponse from '../../data/models/response/UserDetailsResponse'

export default class UserService {
  constructor(private readonly apiRepository: ApiRepository) {}

  async getUserProfile(): Promise<UserDetailsResponse> {
    return await this.apiRepository.get<UserDetailsResponse>('/user/profile')
  }
}
