import ApiRepository from '../../data/api/ApiRepository'
import ConversationCreateRequest from '../../data/models/request/ConversationCreateRequest'
import ConversationDetailsResponse from '../../data/models/response/ConversationDetailsResponse'

export default class ConversationService {
  constructor(private readonly apiRepository: ApiRepository) {}

  async getConversations(): Promise<ConversationDetailsResponse[]> {
    return await this.apiRepository.get<ConversationDetailsResponse[]>('/conversations')
  }

  async createConversation(conversationRequest: ConversationCreateRequest): Promise<ConversationDetailsResponse> {
    return await this.apiRepository.post<ConversationDetailsResponse>(
      '/conversations',
      undefined,
      conversationRequest.toJson()
    )
  }

  async deleteConversation(conversationId: string): Promise<void> {
    await this.apiRepository.delete<void>(`/conversations/${conversationId}`)
  }
}
