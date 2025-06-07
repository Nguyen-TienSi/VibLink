import FriendshipRequestStatus from '../shared/FriendshipRequestStatus'
import AuditMetadataResponse from './AuditMetadataResponse'
import BaseResponse from './BaseResponse'
import UserDetailsResponse from './UserDetailsResponse'

export default class FriendshipDetailsResponse extends BaseResponse {
  constructor(
    public readonly AuditMetadataResponse: AuditMetadataResponse,
    public readonly Requester: UserDetailsResponse | null,
    public readonly Addressee: UserDetailsResponse | null,
    public readonly FriendRequestStatus: FriendshipRequestStatus
  ) {
    super(AuditMetadataResponse)
  }

  static fromJson(json: Record<string, unknown>): FriendshipDetailsResponse {
    return new FriendshipDetailsResponse(
      AuditMetadataResponse.fromJson(json['auditMetadataResponse'] as Record<string, unknown>),
      json['requester'] ? UserDetailsResponse.fromJson(json['requester'] as Record<string, unknown>) : null,
      json['addressee'] ? UserDetailsResponse.fromJson(json['addressee'] as Record<string, unknown>) : null,
      json['friendRequestStatus'] as FriendshipRequestStatus
    )
  }

  toJson(): object {
    return {
      AuditMetadataResponse: this.AuditMetadataResponse.toJson(),
      Requester: this.Requester ? this.Requester.toJson() : null,
      Addressee: this.Addressee ? this.Addressee.toJson() : null,
      FriendRequestStatus: this.FriendRequestStatus
    }
  }

  toString(): string {
    return JSON.stringify(this.toJson())
  }
}
