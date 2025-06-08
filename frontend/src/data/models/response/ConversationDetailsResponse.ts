import ConversationType from '../shared/ConversationType'
import AuditMetadataResponse from './AuditMetadataResponse'
import BaseResponse from './BaseResponse'

export default class ConversationDetailsResponse extends BaseResponse {
  constructor(
    public readonly AuditMetadataResponse: AuditMetadataResponse,
    public readonly Id: string,
    public readonly ChatName: string,
    public readonly ChatPictureUrl: string,
    public readonly ConversationType: ConversationType
  ) {
    super(AuditMetadataResponse)
  }

  static fromJson(json: Record<string, unknown>): ConversationDetailsResponse {
    return new ConversationDetailsResponse(
      AuditMetadataResponse.fromJson(json['auditMetadataResponse'] as Record<string, unknown>),
      json['id'] as string,
      json['chatName'] as string,
      json['chatPictureUrl'] as string,
      json['conversationType'] as ConversationType
    )
  }

  toJson(): object {
    return {
      auditMetadataResponse: this.AuditMetadataResponse.toJson(),
      id: this.Id,
      chatName: this.ChatName,
      chatPictureUrl: this.ChatPictureUrl,
      conversationType: this.ConversationType
    }
  }

  toString(): string {
    return JSON.stringify(this.toJson())
  }
}
