import ApiProvider from './ApiProvider'

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
    const data = await this.apiProvider.get<T>(endpoint, params, headers)
    return callback ? callback(data) : data
  }

  async post<T>(endpoint: string, headers?: Headers, body?: unknown, callback?: (data: T) => T): Promise<T> {
    const data = await this.apiProvider.post<T>(endpoint, headers, body)
    return callback ? callback(data) : data
  }

  async put<T>(endpoint: string, headers?: Headers, body?: unknown, callback?: (data: T) => T): Promise<T> {
    const data = await this.apiProvider.put<T>(endpoint, headers, body)
    return callback ? callback(data) : data
  }

  async patch<T>(endpoint: string, headers?: Headers, body?: unknown, callback?: (data: T) => T): Promise<T> {
    const data = await this.apiProvider.patch<T>(endpoint, headers, body)
    return callback ? callback(data) : data
  }

  async delete<T>(endpoint: string, headers?: Headers, callback?: (data: T) => T): Promise<T> {
    const data = await this.apiProvider.delete<T>(endpoint, headers)
    return callback ? callback(data) : data
  }

  async head<T>(endpoint: string, headers?: Headers): Promise<T> {
    return this.apiProvider.head<T>(endpoint, headers)
  }

  async options<T>(endpoint: string, headers?: Headers): Promise<T> {
    return this.apiProvider.options<T>(endpoint, headers)
  }
}
