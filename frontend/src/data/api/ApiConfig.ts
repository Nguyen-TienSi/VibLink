export default class ApiConfig {
  static readonly apiVersion: string = '1'
  static readonly httpScheme: string = 'http'
  static readonly apiHost: string = 'localhost'
  static readonly apiPort: number = 3000
  static readonly apiPath: string = '/api'

  static readonly TIMEOUT: number = 5000
  static readonly RETRY_COUNT: number = 3
  static readonly RETRY_DELAY: number = 500

  static get baseUrl(): string {
    return `${this.httpScheme}://${this.apiHost}:${this.apiPort}${this.apiPath}`
  }

  static readonly headers: HeadersInit = {
    'Content-Type': 'application/json; charset=utf-8',
    Accept: 'application/json'
  }
}
