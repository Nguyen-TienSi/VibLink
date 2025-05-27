class ApiResponse<T> {
  data: T | null = null
  error: string | null = null
  success: boolean = false

  constructor(response: Record<string, unknown>) {
    if ('data' in response && response.data !== undefined && typeof response.data === 'object') {
      this.data = response.data as T
    } else if ('error' in response && response.error !== undefined && typeof response.error === 'string') {
      this.error = response.error as string
    }
    this.success = this.data !== null && this.error === null
  }

  onSuccess(callback: (data: T) => void): ApiResponse<T> {
    if (this.success && this.data !== null) {
      callback(this.data)
    }
    return this
  }

  onError(callback: (error: string) => void): ApiResponse<T> {
    if (!this.success && this.error !== null) {
      callback(this.error)
    }
    return this
  }
}

export default ApiResponse
