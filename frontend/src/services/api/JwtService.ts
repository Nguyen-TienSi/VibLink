import { jwtDecode } from 'jwt-decode'

export interface JwtPayload {
  sub?: string
  exp?: number
  iss?: string
  aud?: string
  'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'?: string
  'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'?: string
  [key: string]: unknown
}

export default class JwtService {
  static decodeToken(token: string): JwtPayload | null {
    try {
      return jwtDecode<JwtPayload>(token)
    } catch {
      return null
    }
  }

  static getTokenExpirationDate(): Date | null {
    const token = sessionStorage.getItem('accessToken')
    if (!token) {
      return null
    }
    const decoded = JwtService.decodeToken(token)

    if (!decoded?.exp) {
      return null
    }

    const date = new Date(0)
    date.setUTCSeconds(decoded.exp)

    return date
  }

  static isTokenExpired(): boolean {
    const expirationDate = JwtService.getTokenExpirationDate()
    if (!expirationDate) {
      return true
    }
    return expirationDate < new Date()
  }

  static getUserId(): string | null {
    const token = sessionStorage.getItem('accessToken')
    if (!token) {
      return null
    }
    const decoded = JwtService.decodeToken(token)
    return decoded?.sub || decoded?.['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'] || null
  }

  static getUserRole(): string | null {
    const token = sessionStorage.getItem('accessToken')
    if (!token) {
      return null
    }
    const decoded = JwtService.decodeToken(token)
    return decoded?.['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] || null
  }
}
