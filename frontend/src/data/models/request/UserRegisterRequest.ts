export class UserRegisterRequest {
  constructor(
    public email: string,
    public password: string,
    public firstName: string,
    public lastName: string
  ) {}

  toJson(): string {
    return JSON.stringify({
      email: this.email,
      password: this.password,
      firstName: this.firstName,
      lastName: this.lastName
    })
  }

  toString(): string {
    return this.toJson()
  }
}
