export default class UserSummaryBaseResponse {
  constructor(
    public id: string,
    public firstName: string,
    public lastName: string,
    public pictureUrl: string
  ) {}

  static fromJson(json: Record<string, unknown>): UserSummaryBaseResponse {
    return new UserSummaryBaseResponse(
      json['id'] as string,
      json['firstName'] as string,
      json['lastName'] as string,
      json['pictureUrl'] as string
    )
  }

  toJson(): string {
    return JSON.stringify({
      id: this.id,
      firstName: this.firstName,
      lastName: this.lastName,
      pictureUrl: this.pictureUrl
    })
  }

  toString(): string {
    return this.toJson()
  }
}
