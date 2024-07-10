export type RegisterInputs = {
  name: string
  surname: string
  patronymic: string | undefined
  email: string
  password: string
  isTeacher: boolean
}

export type LoginInputs = {
  email: string
  password: string
}
