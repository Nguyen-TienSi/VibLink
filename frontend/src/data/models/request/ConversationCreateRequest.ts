import ConversationType from '../shared/ConversationType'

export default class ConversationCreateRequest {
  constructor(
    public readonly ChatName: string,
    public readonly ChatPicture: File | null,
    public readonly ConversationType: ConversationType,
    public readonly ParticipantIds: string[]
  ) {}

  static fromJson(json: Record<string, unknown>): ConversationCreateRequest {
    return new ConversationCreateRequest(
      json['chatName'] as string,
      json['chatPicture'] as File | null,
      json['conversationType'] as ConversationType,
      json['participantIds'] as string[]
    )
  }

  toJson(): object {
    return {
      chatName: this.ChatName,
      chatPicture: this.ChatPicture,
      conversationType: this.ConversationType,
      participantIds: this.ParticipantIds
    }
  }

  toFormData(): FormData {
    const formData = new FormData()
    formData.append('chatName', this.ChatName)
    if (this.ChatPicture) {
      formData.append('chatPicture', this.ChatPicture)
    }
    formData.append('conversationType', this.ConversationType.toString())
    this.ParticipantIds.forEach((id) => formData.append('participantIds', id))
    return formData
  }

  toString(): string {
    return JSON.stringify(this.toJson())
  }
}
