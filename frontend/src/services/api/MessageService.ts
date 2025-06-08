import ApiRepository from '../../data/api/ApiRepository'
import MessageCreateRequest from '../../data/models/request/MessageCreateRequest'
import MessageDetailsResponse from '../../data/models/response/MessageDetailsResponse'

export default class MessageService {
  constructor(private readonly apiRepository: ApiRepository) {}

  async getById(messageId: string): Promise<MessageDetailsResponse> {
    const data = await this.apiRepository.get<Record<string, unknown>>(`/api/Message/${messageId}`)
    return MessageDetailsResponse.fromJson(data)
  }

  async getByConversationId(conversationId: string): Promise<MessageDetailsResponse[]> {
    const data = await this.apiRepository.get<Record<string, unknown>[]>(`/api/Message/by-conversation/${conversationId}`)
    return data.map((item) => MessageDetailsResponse.fromJson(item))
  }

  async createMessage(messageCreateRequest: MessageCreateRequest): Promise<MessageDetailsResponse> {
    const data = await this.apiRepository.post<Record<string, unknown>>('/api/Message', undefined, messageCreateRequest)
    return MessageDetailsResponse.fromJson(data)
  }
}
