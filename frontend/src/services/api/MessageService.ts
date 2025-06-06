import ApiRepository from '../../data/api/ApiRepository'
import MessageDetailsResponse from '../../data/models/response/MessageDetailsResponse'

export default class MessageService {
  constructor(private readonly apiRepository: ApiRepository) {}

  async getMessages(conversationId: string): Promise<MessageDetailsResponse[]> {
    return await this.apiRepository.get<MessageDetailsResponse[]>(`/conversations/${conversationId}/messages`)
  }
}
