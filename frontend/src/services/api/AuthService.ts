import ApiRepository from '../../data/api/ApiRepository'
import { LoginRequest } from '../../data/models/request/LoginRequest'
import { UserRegisterRequest } from '../../data/models/request/UserRegisterRequest'

export default class AuthService {
  constructor(private readonly apiRepository: ApiRepository) {}

  async login(loginRequest: LoginRequest): Promise<unknown> {
    return await this.apiRepository.post<unknown>('/auth/login', undefined, loginRequest.toJson())
  }

  async register(registerRequest: UserRegisterRequest): Promise<unknown> {
    return await this.apiRepository.post<unknown>('/auth/register', undefined, registerRequest.toJson())
  }

  async logout(): Promise<void> {
    await this.apiRepository.post<void>('/auth/logout', undefined)
  }
}
