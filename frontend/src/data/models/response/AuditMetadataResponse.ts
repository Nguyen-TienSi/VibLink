export default class AuditMetadataResponse {
  constructor(
    public readonly CreatedAt: Date,
    public readonly UpdatedAt: Date,
    public readonly DeletedAt: Date | null,
    public readonly IsDeleted: boolean,
    public readonly Version: number
  ) {}

  static fromJson(json: Record<string, unknown>): AuditMetadataResponse {
    return new AuditMetadataResponse(
      new Date(json['createdAt'] as string),
      new Date(json['updatedAt'] as string),
      json['deletedAt'] ? new Date(json['deletedAt'] as string) : null,
      json['isDeleted'] as boolean,
      json['version'] as number
    )
  }

  toJson(): object {
    return {
      createdAt: this.CreatedAt.toISOString(),
      updatedAt: this.UpdatedAt.toISOString(),
      deletedAt: this.DeletedAt ? this.DeletedAt.toISOString() : null,
      isDeleted: this.IsDeleted,
      version: this.Version
    }
  }

  toString(): string {
    return JSON.stringify(this.toJson())
  }
}
