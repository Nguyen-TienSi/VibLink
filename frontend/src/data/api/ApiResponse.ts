class ApiResponse<T> {
  data: T | null
  error: string | null
  success: boolean

  constructor(response: Record<string, unknown>) {
    if ('data' in response && response.data !== undefined && typeof response.data === 'object') {
      this.data = response.data as T
      this.error = null
    } else {
      this.data = null
      this.error = (response.error as string | null) ?? 'An unknown error occurred'
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
