export default class LoginRequest {
  constructor(
    public readonly Email: string,
    public readonly Password: string
  ) {}

  toJson(): object {
    return {
      email: this.Email,
      password: this.Password
    }
  }

  toString(): string {
    return JSON.stringify(this.toJson())
  }
}
