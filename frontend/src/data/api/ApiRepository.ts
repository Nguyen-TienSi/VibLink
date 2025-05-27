import ApiProvider from './ApiProvider'
import ApiResponse from './ApiResponse'

export default class ApiRepository {
  constructor(private apiProvider: ApiProvider) {
    if (!apiProvider) {
      throw new Error('ApiProvider is required')
    }
  }

  async get<T>(
    endpoint: string,
    params?: Record<string, unknown>,
    headers?: Headers,
    callback?: (data: T) => T
  ): Promise<T> {
    const apiResponse = (await this.apiProvider.get<T>(endpoint, params, headers)) as ApiResponse<T>
    apiResponse
      .onSuccess((data: T) => (callback ? callback(data) : data))
      .onError((error: string) => {
        console.error('API request failed:', error)
        throw new Error(error)
      })
    return apiResponse.data as T
  }

  async post<T>(endpoint: string, headers?: Headers, body?: unknown, callback?: (data: T) => T): Promise<T> {
    const apiResponse = (await this.apiProvider.post<T>(endpoint, headers, body)) as ApiResponse<T>
    apiResponse
      .onSuccess((data: T) => (callback ? callback(data) : data))
      .onError((error: string) => {
        console.error('API request failed:', error)
        throw new Error(error)
      })
    return apiResponse.data as T
  }

  async put<T>(endpoint: string, headers?: Headers, body?: unknown, callback?: (data: T) => T): Promise<T> {
    const apiResponse = (await this.apiProvider.put<T>(endpoint, headers, body)) as ApiResponse<T>
    apiResponse
      .onSuccess((data: T) => (callback ? callback(data) : data))
      .onError((error: string) => {
        console.error('API request failed:', error)
        throw new Error(error)
      })
    return apiResponse.data as T
  }

  async patch<T>(endpoint: string, headers?: Headers, body?: unknown, callback?: (data: T) => T): Promise<T> {
    const apiResponse = (await this.apiProvider.patch<T>(endpoint, headers, body)) as ApiResponse<T>
    apiResponse
      .onSuccess((data: T) => (callback ? callback(data) : data))
      .onError((error: string) => {
        console.error('API request failed:', error)
        throw new Error(error)
      })
    return apiResponse.data as T
  }

  async delete<T>(endpoint: string, headers?: Headers, callback?: (data: T) => T): Promise<T> {
    const apiResponse = (await this.apiProvider.delete<T>(endpoint, headers)) as ApiResponse<T>
    apiResponse
      .onSuccess((data: T) => (callback ? callback(data) : data))
      .onError((error: string) => {
        console.error('API request failed:', error)
        throw new Error(error)
      })
    return apiResponse.data as T
  }

  async head<T>(endpoint: string, headers?: Headers): Promise<T> {
    const apiResponse = (await this.apiProvider.head<T>(endpoint, headers)) as ApiResponse<T>
    apiResponse
      .onSuccess((data: T) => data)
      .onError((error: string) => {
        console.error('API request failed:', error)
        throw new Error(error)
      })
    return apiResponse.data as T
  }

  async options<T>(endpoint: string, headers?: Headers): Promise<T> {
    const apiResponse = (await this.apiProvider.options<T>(endpoint, headers)) as ApiResponse<T>
    apiResponse
      .onSuccess((data: T) => data)
      .onError((error: string) => {
        console.error('API request failed:', error)
        throw new Error(error)
      })
    return apiResponse.data as T
  }
}
