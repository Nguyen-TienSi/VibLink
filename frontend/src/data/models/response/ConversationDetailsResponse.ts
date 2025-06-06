import ConversationType from '../shared/ConversationType'
import AuditMetadataResponse from './AuditMetadataResponse'
import BaseResponse from './BaseResponse'
import MessageDetailsResponse from './MessageDetailsResponse'
import UserDetailsResponse from './UserDetailsResponse'

export default class ConversationDetailsResponse extends BaseResponse {
  constructor(
    public readonly AuditMetadataResponse: AuditMetadataResponse,
    public readonly Id: string,
    public readonly ChatName: string,
    public readonly ChatPictureUrl: string,
    public readonly ConversationType: ConversationType,
    public readonly Participants: UserDetailsResponse[],
    public readonly Messages: MessageDetailsResponse[]
  ) {
    super(AuditMetadataResponse)
  }

  static fromJson(json: Record<string, unknown>): ConversationDetailsResponse {
    return new ConversationDetailsResponse(
      AuditMetadataResponse.fromJson(json['auditMetadataResponse'] as Record<string, unknown>),
      json['id'] as string,
      json['chatName'] as string,
      json['chatPictureUrl'] as string,
      json['conversationType'] as ConversationType,
      (json['participants'] as Record<string, unknown>[]).map((participant) =>
        UserDetailsResponse.fromJson(participant)
      ),
      (json['messages'] as Record<string, unknown>[]).map((message) => MessageDetailsResponse.fromJson(message))
    )
  }

  toJson(): object {
    return {
      auditMetadataResponse: this.AuditMetadataResponse.toJson(),
      id: this.Id,
      chatName: this.ChatName,
      chatPictureUrl: this.ChatPictureUrl,
      conversationType: this.ConversationType,
      participants: this.Participants.map((participant) => participant.toJson()),
      messages: this.Messages.map((message) => message.toJson())
    }
  }

  toString(): string {
    return JSON.stringify(this.toJson())
  }
}
