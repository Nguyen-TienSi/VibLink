export class LoginRequest {
  constructor(
    public readonly email: string,
    public readonly password: string
  ) {}

  toJson(): string {
    return JSON.stringify({
      email: this.email,
      password: this.password
    })
  }

  toString(): string {
    return this.toJson()
  }
}
