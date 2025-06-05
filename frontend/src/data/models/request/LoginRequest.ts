export class LoginRequest {
  constructor(
    public email: string,
    public password: string
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
