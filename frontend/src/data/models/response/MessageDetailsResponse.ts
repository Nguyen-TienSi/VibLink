import MessageType from '../shared/MessageType'
import ReactionType from '../shared/ReactionType'
import AuditMetadataResponse from './AuditMetadataResponse'
import BaseResponse from './BaseResponse'
import UserDetailsResponse from './UserDetailsResponse'

export default class MessageDetailsResponse extends BaseResponse {
  constructor(
    public readonly AuditMetadataResponse: AuditMetadataResponse,
    public readonly Id: string,
    public readonly Sender: UserDetailsResponse,
    public readonly Content: string,
    public readonly MessageType: MessageType,
    public readonly Recipients: UserDetailsResponse[],
    public readonly SeenBy: Map<UserDetailsResponse, Date>,
    public readonly Reactions: Map<UserDetailsResponse, ReactionType>
  ) {
    super(AuditMetadataResponse)
  }

  static fromJson(json: Record<string, unknown>): MessageDetailsResponse {
    return new MessageDetailsResponse(
      AuditMetadataResponse.fromJson(json['auditMetadataResponse'] as Record<string, unknown>),
      json['id'] as string,
      UserDetailsResponse.fromJson(json['sender'] as Record<string, unknown>),
      json['content'] as string,
      json['messageType'] as MessageType,
      (json['recipients'] as Record<string, unknown>[]).map((recipient) => UserDetailsResponse.fromJson(recipient)),
      json['seenBy'] instanceof Map
        ? new Map(
            Array.from(json['seenBy'] as Map<unknown, unknown>).map(([key, value]) => [
              UserDetailsResponse.fromJson(key as Record<string, unknown>),
              new Date(value as string | Date)
            ])
          )
        : new Map(),
      json['reactions'] instanceof Map
        ? new Map(
            Array.from(json['reactions'] as Map<unknown, unknown>).map(([key, value]) => [
              UserDetailsResponse.fromJson(key as Record<string, unknown>),
              value as ReactionType
            ])
          )
        : new Map()
    )
  }

  toJson(): object {
    return {
      auditMetadataResponse: this.AuditMetadataResponse.toJson(),
      id: this.Id,
      sender: this.Sender.toJson(),
      content: this.Content,
      messageType: this.MessageType,
      recipients: this.Recipients.map((recipient) => recipient.toJson()),
      seenBy: Array.from(this.SeenBy.entries()).reduce(
        (acc, [user, date]) => ({ ...acc, [String((user.toJson() as { id: string }).id)]: date.toISOString() }),
        {}
      ),
      reactions: Array.from(this.Reactions.entries()).reduce(
        (acc, [user, reaction]) => ({ ...acc, [String((user.toJson() as { id: string }).id)]: reaction }),
        {}
      )
    }
  }

  toString(): string {
    return JSON.stringify(this.toJson())
  }
}
