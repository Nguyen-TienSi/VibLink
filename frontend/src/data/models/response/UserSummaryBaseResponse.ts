export default class UserSummaryBaseResponse {
  constructor(
    public readonly Id: string,
    public readonly FirstName: string,
    public readonly LastName: string,
    public readonly Email: string,
    public readonly PictureUrl: string
  ) {}

  static fromJson(json: Record<string, unknown>): UserSummaryBaseResponse {
    return new UserSummaryBaseResponse(
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
