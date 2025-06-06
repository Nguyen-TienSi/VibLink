import FriendRequestStatus from '../shared/FriendRequestStatus'
import AuditMetadataResponse from './AuditMetadataResponse'
import BaseResponse from './BaseResponse'
import UserDetailsResponse from './UserDetailsResponse'

export default class FriendshipDetailsResponse extends BaseResponse {
  constructor(
    public readonly AuditMetadataResponse: AuditMetadataResponse,
    public readonly Requester: UserDetailsResponse,
    public readonly Addressee: UserDetailsResponse,
    public readonly FriendRequestStatus: FriendRequestStatus
  ) {
    super(AuditMetadataResponse)
  }

  static fromJson(json: Record<string, unknown>): FriendshipDetailsResponse {
    return new FriendshipDetailsResponse(
      AuditMetadataResponse.fromJson(json['auditMetadataResponse'] as Record<string, unknown>),
      UserDetailsResponse.fromJson(json['requester'] as Record<string, unknown>),
      UserDetailsResponse.fromJson(json['addressee'] as Record<string, unknown>),
      json['friendRequestStatus'] as FriendRequestStatus
    )
  }

  toJson(): object {
    return {
      AuditMetadataResponse: this.AuditMetadataResponse.toJson(),
      Requester: this.Requester.toJson(),
      Addressee: this.Addressee.toJson(),
      FriendRequestStatus: this.FriendRequestStatus
    }
  }

  toString(): string {
    return JSON.stringify(this.toJson())
  }
}
