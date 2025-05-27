export default interface ApiProvider {
  get<T>(endpoint: string, params?: Record<string, unknown>, headers?: Headers): Promise<T>
  post<T>(endpoint: string, headers?: Headers, body?: unknown): Promise<T>
  put<T>(endpoint: string, headers?: Headers, body?: unknown): Promise<T>
  patch<T>(endpoint: string, headers?: Headers, body?: unknown): Promise<T>
  delete<T>(endpoint: string, headers?: Headers): Promise<T>
  head<T>(endpoint: string, headers?: Headers): Promise<T>
  options<T>(endpoint: string, headers?: Headers): Promise<T>
}
