import ApiRepository from '../../data/api/ApiRepository'
import LoginRequest from '../../data/models/request/LoginRequest'
import RefreshTokenRequest from '../../data/models/request/RefreshTokenRequest'
import UserRegisterRequest from '../../data/models/request/UserRegisterRequest'
import AuthTokenResponse from '../../data/models/response/AuthTokenResponse'

export default class AuthService {
  constructor(private readonly apiRepository: ApiRepository) {}

  async login(loginRequest: LoginRequest): Promise<AuthTokenResponse> {
    const data = await this.apiRepository.post<Record<string, unknown>>(
      '/api/auth/login',
      undefined,
      loginRequest.toJson()
    )
    const tokenResponse = AuthTokenResponse.fromJson(data)
    // Save token to sessionStorage
    sessionStorage.setItem('accessToken', tokenResponse.accessToken)
    sessionStorage.setItem('refreshToken', tokenResponse.refreshToken)
    return tokenResponse
  }

  async register(registerRequest: UserRegisterRequest): Promise<AuthTokenResponse> {
    const formData = new FormData()
    formData.append('Email', registerRequest.Email)
    formData.append('Password', registerRequest.Password)
    formData.append('FirstName', registerRequest.FirstName)
    formData.append('LastName', registerRequest.LastName)
    if (registerRequest.Picture) {
      formData.append('Picture', registerRequest.Picture)
    }
    const data = await this.apiRepository.post<Record<string, unknown>>('/api/auth/register', undefined, formData)
    const tokenResponse = AuthTokenResponse.fromJson(data)
    // Save token to sessionStorage
    sessionStorage.setItem('accessToken', tokenResponse.accessToken)
    sessionStorage.setItem('refreshToken', tokenResponse.refreshToken)
    return tokenResponse
  }

  async logout(refreshTokenRequest: RefreshTokenRequest): Promise<void> {
    await this.apiRepository.post<void>('/api/auth/logout', undefined, refreshTokenRequest.toJson())
    // Remove tokens from sessionStorage
    sessionStorage.removeItem('accessToken')
    sessionStorage.removeItem('refreshToken')
  }
}
