import ApiProvider from './ApiProvider'
import HttpProvider from './HttpProvider'
import HttpMethod from './HttpMethod'

export default class HttpApiProvider extends HttpProvider implements ApiProvider {
  get<T>(endpoint: string, params?: Record<string, unknown>, headers?: Headers): Promise<T> {
    return this.request<T>(endpoint, HttpMethod.GET, params, headers)
  }
  post<T>(endpoint: string, headers?: Headers, body?: unknown): Promise<T> {
    return this.request<T>(endpoint, HttpMethod.POST, undefined, headers, body)
  }
  put<T>(endpoint: string, headers?: Headers, body?: unknown): Promise<T> {
    return this.request<T>(endpoint, HttpMethod.PUT, undefined, headers, body)
  }
  patch<T>(endpoint: string, headers?: Headers, body?: unknown): Promise<T> {
    return this.request<T>(endpoint, HttpMethod.PATCH, undefined, headers, body)
  }
  delete<T>(endpoint: string, headers?: Headers): Promise<T> {
    return this.request<T>(endpoint, HttpMethod.DELETE, undefined, headers)
  }
  head<T>(endpoint: string, headers?: Headers): Promise<T> {
    return this.request<T>(endpoint, HttpMethod.HEAD, undefined, headers)
  }
  options<T>(endpoint: string, headers?: Headers): Promise<T> {
    return this.request<T>(endpoint, HttpMethod.OPTIONS, undefined, headers)
  }

  private async request<T>(
    endpoint: string,
    method: HttpMethod,
    params?: Record<string, unknown>,
    headers?: Headers,
    body?: unknown
  ): Promise<T> {
    try {
      const finalUrl = this.resolveUrl(endpoint, params)
      const finalHeaders = this.mergeHeaders(headers)
      const requestOptions: RequestInit = {
        method,
        headers: finalHeaders,
        body: body ? JSON.stringify(body) : undefined
      }

      const response: Response = await fetch(finalUrl, requestOptions)

      if (!response.ok) {
        let errorDetails = ''
        try {
          const errorBody = await response.text()
          if (errorBody) {
            errorDetails = ` - ${errorBody}`
          }
        } catch {
          // Failed to read error body, proceed without it
        }
        const errorMessage = `HTTP error! status: ${response.status}${errorDetails}`
        console.error('Request failed:', errorMessage)
        throw new Error(errorMessage)
      }
      if (method === HttpMethod.HEAD || method === HttpMethod.OPTIONS) {
        return {} as T
      }
      const data: T = (await response.json()) as T
      return data
    } catch (error) {
      this.handleError(error)
      throw error
    }
  }
}
