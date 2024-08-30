export type RegisterInputs = {
  name: string
  surname: string
  patronymic: string | undefined
  email: string
  isTeacher: boolean
}

export type LoginInputs = {
  email: string
}

export type ConfirmEmailInputs = {
  email: string
  code: string
}
