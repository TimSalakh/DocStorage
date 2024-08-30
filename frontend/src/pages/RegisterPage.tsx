import React, { useState } from 'react'
import { RegisterInputs } from '../models/AuthInputsModels'
import { useAuth } from '../context/Context'
import {
  Button,
  Card,
  Checkbox,
  Label,
  Spinner,
  TextInput
} from 'flowbite-react'
import { HiMail } from 'react-icons/hi'
import { Link } from 'react-router-dom'
import { TeacherInfoAlert } from '../components/TeacherInfoAlert'

const RegisterPage = () => {
  const [error, setError] = useState<string | null>(null)
  const { register: register } = useAuth()
  const [isLoading, setIsLoading] = useState<boolean>(false)
  const [formData, setFormData] = useState<RegisterInputs>({
    name: '',
    surname: '',
    patronymic: '',
    email: '',
    isTeacher: false
  })

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value, type, checked } = e.target
    setFormData((prev) => ({
      ...prev,
      [name]: type === 'checkbox' ? checked : value
    }))
  }

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault()
    setIsLoading(true)
    const responseText = await register(formData)
    setIsLoading(false)
  }

  return (
    <div className='w-screen h-screen'>
      <div className='w-full h-full flex flex-col justify-center items-center'>
        <div className='w-1/5 h-full flex flex-col justify-center items-center'>
          <TeacherInfoAlert />
          <Card className='w-full h-auto'>
            <form
              className='flex flex-col justify-start'
              onSubmit={handleSubmit}
            >
              <div className='mb-3'>
                <Label htmlFor='name'>Имя</Label>
                <TextInput
                  required={true}
                  name='name'
                  value={formData.name}
                  onChange={handleChange}
                  placeholder='Иван'
                />
              </div>

              <div className='mb-3'>
                <Label htmlFor='surname'>Фамилия</Label>
                <TextInput
                  required={true}
                  name='surname'
                  value={formData.surname}
                  onChange={handleChange}
                  placeholder='Иванов'
                />
              </div>

              <div className='mb-4'>
                <Label htmlFor='email'>Почта</Label>
                <TextInput
                  icon={HiMail}
                  required={true}
                  name='email'
                  type='email'
                  value={formData.email}
                  onChange={handleChange}
                  placeholder='ivanivanov@mail.ru'
                />
              </div>

              <div className='flex items-start mb-3'>
                <div className='flex items-center h-5'>
                  <Checkbox
                    name='isTeacher'
                    checked={formData.isTeacher}
                    onChange={handleChange}
                    className='mr-2'
                  />
                </div>

                <div className='flex flex-row mb-2'>
                  <Label htmlFor='isTeacher' className='mr-1'>
                    Я преподаватель.
                  </Label>
                </div>
              </div>

              <div className='flex flex-row justify-between items-center'>
                <Button type='submit' disabled={isLoading}>
                  {isLoading ? <Spinner /> : 'Зарегистрироваться'}{' '}
                </Button>
                <div>
                  Уже есть аккаунт?{' '}
                  <Link
                    to={'/login'}
                    className='font-medium text-blue-600 underline dark:text-blue-500 hover:no-underline text-sm'
                  >
                    Войти
                  </Link>
                </div>
              </div>
            </form>
            {error ? error : ''}
          </Card>
        </div>
      </div>
    </div>
  )
}

export default RegisterPage
