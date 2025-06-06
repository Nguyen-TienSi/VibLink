import UserRole from '../shared/UserRole'
import AuditMetadataResponse from './AuditMetadataResponse'
import BaseResponse from './BaseResponse'

export default class UserDetailsResponse extends BaseResponse {
  constructor(
    public readonly AuditMetadataResponse: AuditMetadataResponse,
    public readonly Id: string,
    public readonly FirstName: string,
    public readonly LastName: string,
    public readonly Email: string,
    public readonly PictureUrl: string,
    public readonly UserRoles: UserRole[],
    public readonly LastLogin: Date | null
  ) {
    super(AuditMetadataResponse)
  }

  static fromJson(json: Record<string, unknown>): UserDetailsResponse {
    return new UserDetailsResponse(
      AuditMetadataResponse.fromJson(json['auditMetadataResponse'] as Record<string, unknown>),
      json['id'] as string,
      json['firstName'] as string,
      json['lastName'] as string,
      json['email'] as string,
      json['pictureUrl'] as string,
      (json['userRoles'] as UserRole[]) || [],
      json['lastLogin'] ? new Date(json['lastLogin'] as string) : null
    )
  }

  toJson(): object {
    return {
      auditMetadataResponse: this.AuditMetadataResponse.toJson(),
      id: this.Id,
      firstName: this.FirstName,
      lastName: this.LastName,
      email: this.Email,
      pictureUrl: this.PictureUrl,
      userRoles: this.UserRoles,
      lastLogin: this.LastLogin ? this.LastLogin.toISOString() : null
    }
  }

  toString(): string {
    return JSON.stringify(this.toJson())
  }
}
