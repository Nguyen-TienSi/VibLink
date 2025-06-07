export default class RefreshTokenRequest {
  constructor(public readonly RefreshToken: string) {}

  static fromJson(json: Record<string, unknown>): RefreshTokenRequest {
    return new RefreshTokenRequest(json['refreshToken'] as string)
  }

  toJson(): object {
    return {
      refreshToken: this.RefreshToken
    }
  }

  toString(): string {
    return JSON.stringify(this.toJson())
  }
}
