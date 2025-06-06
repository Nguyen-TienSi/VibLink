import ApiProvider from './ApiProvider'

export default class ApiRepository {
  constructor(private apiProvider: ApiProvider) {
    if (!apiProvider) {
      throw new Error('ApiProvider is required')
    }
  }

  async get<T>(endpoint: string, params?: Record<string, unknown>, headers?: Headers): Promise<T> {
    return await this.apiProvider.get<T>(endpoint, params, headers)
  }

  async post<T>(endpoint: string, headers?: Headers, body?: unknown): Promise<T> {
    return await this.apiProvider.post<T>(endpoint, headers, body)
  }

  async put<T>(endpoint: string, headers?: Headers, body?: unknown): Promise<T> {
    return await this.apiProvider.put<T>(endpoint, headers, body)
  }

  async patch<T>(endpoint: string, headers?: Headers, body?: unknown): Promise<T> {
    return await this.apiProvider.patch<T>(endpoint, headers, body)
  }

  async delete<T>(endpoint: string, headers?: Headers): Promise<T> {
    return await this.apiProvider.delete<T>(endpoint, headers)
  }

  async head<T>(endpoint: string, headers?: Headers): Promise<T> {
    return await this.apiProvider.head<T>(endpoint, headers)
  }

  async options<T>(endpoint: string, headers?: Headers): Promise<T> {
    return await this.apiProvider.options<T>(endpoint, headers)
  }
}
