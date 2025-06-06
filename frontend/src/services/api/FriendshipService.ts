import ApiRepository from '../../data/api/ApiRepository'
import FriendshipDetailsResponse from '../../data/models/response/FriendshipDetailsResponse'

export default class FriendshipService {
  constructor(private readonly apiRepository: ApiRepository) {}

  async getFriendships(): Promise<FriendshipDetailsResponse[]> {
    return await this.apiRepository.get<FriendshipDetailsResponse[]>('/friendships')
  }
}
