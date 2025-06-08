import FriendshipRequestStatus from '../shared/FriendshipRequestStatus'
import AuditMetadataResponse from './AuditMetadataResponse'
import BaseResponse from './BaseResponse'
import UserDetailsResponse from './UserDetailsResponse'

export default class FriendshipDetailsResponse extends BaseResponse {
  constructor(
    public readonly AuditMetadataResponse: AuditMetadataResponse,
    public readonly Id: string,
    public readonly Requester: UserDetailsResponse,
    public readonly Addressee: UserDetailsResponse,
    public readonly FriendRequestStatus: FriendshipRequestStatus
  ) {
    super(AuditMetadataResponse)
  }

  static fromJson(json: Record<string, unknown>): FriendshipDetailsResponse {
    return new FriendshipDetailsResponse(
      AuditMetadataResponse.fromJson(json['auditMetadataResponse'] as Record<string, unknown>),
      json['id'] as string,
      UserDetailsResponse.fromJson(json['requester'] as Record<string, unknown>),
      UserDetailsResponse.fromJson(json['addressee'] as Record<string, unknown>),
      json['friendRequestStatus'] as FriendshipRequestStatus
    )
  }

  toJson(): object {
    return {
      AuditMetadataResponse: this.AuditMetadataResponse.toJson(),
      Id: this.Id,
      Requester: this.Requester.toJson(),
      Addressee: this.Addressee.toJson(),
      FriendRequestStatus: this.FriendRequestStatus
    }
  }

  toString(): string {
    return JSON.stringify(this.toJson())
  }
}
