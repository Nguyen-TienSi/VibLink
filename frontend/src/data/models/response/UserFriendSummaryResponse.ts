import UserSummaryBaseResponse from './UserSummaryBaseResponse'

export default class UserFriendSummaryResponse extends UserSummaryBaseResponse {
  constructor(
    public readonly Id: string,
    public readonly FirstName: string,
    public readonly LastName: string,
    public readonly Email: string,
    public readonly PictureUrl: string
  ) {
    super(Id, FirstName, LastName, Email, PictureUrl)
  }

  static fromJson(json: Record<string, unknown>): UserFriendSummaryResponse {
    return new UserFriendSummaryResponse(
      json['id'] as string,
      json['firstName'] as string,
      json['lastName'] as string,
      json['email'] as string,
      json['pictureUrl'] as string
    )
  }

  toJson(): object {
    return {
      id: this.Id,
      firstName: this.FirstName,
      lastName: this.LastName,
      email: this.Email,
      pictureUrl: this.PictureUrl
    }
  }

  toString(): string {
    return JSON.stringify(this.toJson())
  }
}

export const friends: UserFriendSummaryResponse[] = [
  new UserFriendSummaryResponse('1', 'Osman', 'Campos', '', ''),
  new UserFriendSummaryResponse('2', 'Jasmin', 'Lowery', '', ''),
  new UserFriendSummaryResponse('3', 'Anthony', 'Cordanes', '', '')
]
