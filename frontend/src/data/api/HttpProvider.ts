import ApiConfig from './ApiConfig'

export default abstract class HttpProvider {
  isInternetAvailable(): boolean {
    return navigator.onLine
  }

  isServerAvailable(response: Response | null): boolean {
    return response !== null && response.status === 200
  }

  handleError(error: unknown): void {
    if (error instanceof Error) {
      console.error('An error occurred:', error.message)
    } else {
      console.error('An unknown error occurred:', String(error))
    }
  }

  resolveUrl(endpoint: string, params?: Record<string, unknown>): URL {
    return this.buildUrl(endpoint, params)
  }

  mergeHeaders(headers?: HeadersInit): Headers {
    const merged: HeadersInit = {
      ...(ApiConfig.headers || {}),
      ...(headers instanceof Headers ? Object.fromEntries(headers.entries()) : headers || {})
    }
    return new Headers(merged)
  }

  buildUrl(endpoint: string, params?: Record<string, unknown>): URL {
    const urlObj = this.isAbsoluteUrl(endpoint) ? new URL(endpoint) : new URL(endpoint, ApiConfig.baseUrl)
    if (params) {
      const queryString = new URLSearchParams(params as Record<string, string>)
      urlObj.search = queryString.toString()
    }
    return urlObj
  }

  isAbsoluteUrl(url: string): boolean {
    return url.startsWith('http://') || url.startsWith('https://')
  }
}
