export type DataToStore = {
  token: string
  id: string
  email: string
}

export type TokenPayload = {
  aud: string
  email: string
  exp: number
  iat: number
  iss: string
  nbf: number
  role: string
}

export enum Role {
  Student = 'Student',
  Teacher = 'Teacher',
  Admin = 'Admin'
}
