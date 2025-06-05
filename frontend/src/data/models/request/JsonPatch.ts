export type JsonPatchOp = 'add' | 'remove' | 'replace' | 'move' | 'copy' | 'test'

export interface JsonPatchOperation {
  op: JsonPatchOp
  path: string
  value?: unknown
  from?: string
}

export type JsonPatchDocument = JsonPatchOperation[]
