export default class AuthTokenResponse {
  constructor(
    public readonly accessToken: string,
    public readonly refreshToken: string
  ) {}

  static fromJson(json: Record<string, unknown>): AuthTokenResponse {
    return new AuthTokenResponse(json['accessToken'] as string, json['refreshToken'] as string)
  }

  toJson(): object {
    return {
      accessToken: this.accessToken,
      refreshToken: this.refreshToken
    }
  }

  toString(): string {
    return JSON.stringify(this.toJson())
  }
}
