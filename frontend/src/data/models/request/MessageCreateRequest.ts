import MessageType from '../shared/MessageType'

export default class MessageCreateRequest {
  constructor(
    public readonly Content: string,
    public readonly MessageType: MessageType,
    public readonly ConversationId: string
  ) {}

  static fromJson(json: Record<string, unknown>): MessageCreateRequest {
    return new MessageCreateRequest(
      json['content'] as string,
      json['messageType'] as MessageType,
      json['conversationId'] as string
    )
  }

  toJson(): object {
    return {
      content: this.Content,
      messageType: this.MessageType,
      conversationId: this.ConversationId
    }
  }

  toString(): string {
    return JSON.stringify(this.toJson())
  }
}
