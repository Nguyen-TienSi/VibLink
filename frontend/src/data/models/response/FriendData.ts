export class FriendData {
  constructor(
    public id: number,
    public name: string,
    public message: string,
    public time: string
  ) {}

  static fromJson(json: Record<string, unknown>): FriendData {
    return new FriendData(
      json['id'] as number,
      json['name'] as string,
      json['message'] as string,
      json['time'] as string
    )
  }

  toJson(): string {
    return JSON.stringify({
      id: this.id,
      name: this.name,
      message: this.message,
      time: this.time
    })
  }

  toString(): string {
    return this.toJson()
  }
}

export const friends: FriendData[] = [
  new FriendData(1, 'Osman Campos', 'You: Hey! We are ready...', '20m'),
  new FriendData(2, 'Jasmin Lowery', 'You: Let’s discuss...', '1h'),
  new FriendData(3, 'Anthony Cordanes', 'What do you think?', '1d')
]
