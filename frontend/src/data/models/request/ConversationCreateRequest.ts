import ConversationType from '../shared/ConversationType'

export default class ConversationCreateRequest {
  constructor(
    public readonly ChatName: string,
    public readonly ChatPicture: File | null,
    public readonly ConversationType: ConversationType,
    public readonly Participants: string[]
  ) {}

  static fromJson(json: Record<string, unknown>): ConversationCreateRequest {
    return new ConversationCreateRequest(
      json['chatName'] as string,
      json['chatPicture'] as File | null,
      json['conversationType'] as ConversationType,
      json['participants'] as string[]
    )
  }

  toJson(): object {
    return {
      chatName: this.ChatName,
      chatPicture: this.ChatPicture,
      conversationType: this.ConversationType,
      participants: this.Participants
    }
  }

  toString(): string {
    return JSON.stringify(this.toJson())
  }
}
