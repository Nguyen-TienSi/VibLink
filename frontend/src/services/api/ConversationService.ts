import ApiRepository from '../../data/api/ApiRepository'
import ConversationCreateRequest from '../../data/models/request/ConversationCreateRequest'
import ConversationDetailsResponse from '../../data/models/response/ConversationDetailsResponse'

export default class ConversationService {
  constructor(private readonly apiRepository: ApiRepository) {}

  async getById(conversationId: string): Promise<ConversationDetailsResponse> {
    const data = await this.apiRepository.get<Record<string, unknown>>(`/api/Conversation/${conversationId}`)
    return ConversationDetailsResponse.fromJson(data)
  }

  async getByParticipant(): Promise<ConversationDetailsResponse[]> {
    const data = await this.apiRepository.get<Record<string, unknown>[]>('/api/Conversation/by-participant')
    return data.map((item) => ConversationDetailsResponse.fromJson(item))
  }

  async createConversation(conversationRequest: ConversationCreateRequest): Promise<ConversationDetailsResponse> {
    const data = await this.apiRepository.post<Record<string, unknown>>(
      '/api/Conversation',
      undefined,
      conversationRequest.toFormData()
    )
    return ConversationDetailsResponse.fromJson(data)
  }

  async deleteConversation(conversationId: string): Promise<void> {
    await this.apiRepository.delete<void>(`/api/Conversation/${conversationId}`)
  }
}
