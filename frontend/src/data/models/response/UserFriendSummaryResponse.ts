import UserSummaryBaseResponse from './UserSummaryBaseResponse'

export default class UserFriendSummaryResponse extends UserSummaryBaseResponse {
  constructor(
    id: string,
    firstName: string,
    lastName: string,
    pictureUrl: string,
    public message: string,
    public time: string
  ) {
    super(id, firstName, lastName, pictureUrl)
  }

  static fromJson(json: Record<string, unknown>): UserFriendSummaryResponse {
    return new UserFriendSummaryResponse(
      json['id'] as string,
      json['firstName'] as string,
      json['lastName'] as string,
      json['pictureUrl'] as string,
      json['message'] as string,
      json['time'] as string
    )
  }

  toJson(): string {
    return JSON.stringify({
      id: this.id,
      firstName: this.firstName,
      lastName: this.lastName,
      pictureUrl: this.pictureUrl,
      message: this.message,
      time: this.time
    })
  }

  toString(): string {
    return this.toJson()
  }
}

export const friends: UserFriendSummaryResponse[] = [
  new UserFriendSummaryResponse('1', 'Osman', 'Campos', '', 'You: Hey! We are ready...', '20m'),
  new UserFriendSummaryResponse('2', 'Jasmin', 'Lowery', '', 'You: Let’s discuss...', '1h'),
  new UserFriendSummaryResponse('3', 'Anthony', 'Cordanes', '', 'What do you think?', '1d')
]
