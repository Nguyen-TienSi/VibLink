export default class UserRegisterRequest {
  constructor(
    public readonly Email: string,
    public readonly Password: string,
    public readonly FirstName: string,
    public readonly LastName: string,
    public readonly Picture: File | null
  ) {}

  toJson(): object {
    return {
      email: this.Email,
      password: this.Password,
      firstName: this.FirstName,
      lastName: this.LastName
    }
  }

  toString(): string {
    return JSON.stringify(this.toJson())
  }
}
