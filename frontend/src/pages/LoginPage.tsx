import { useState } from 'react'
import { useAuth } from '../context/Context'
import { LoginInputs } from '../models/AuthInputsModels'
import { Button, Card, Label, Spinner, TextInput } from 'flowbite-react'
import { HiMail } from 'react-icons/hi'
import { Link } from 'react-router-dom'

const LoginPage = () => {
  const { login: login } = useAuth()
  const [isLoading, setIsLoading] = useState<boolean>(false)
  const [formData, setFormData] = useState<LoginInputs>({
    email: ''
  })

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target
    setFormData((prev) => ({
      ...prev,
      [name]: value
    }))
  }

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault()
    setIsLoading(true)
    console.log('данные с формы переданы')
    login(formData)
    setIsLoading(false)
  }

  return (
    <div className='w-screen h-screen flex flex-col'>
      <div className='w-full h-full flex flex-row justify-center items-center'>
        <Card className='w-1/5'>
          <form className='flex flex-col justify-start' onSubmit={handleSubmit}>
            <div className='mb-2'>
              <Label htmlFor='email'>Почта</Label>
              <TextInput
                icon={HiMail}
                required={true}
                name='email'
                type='email'
                value={formData.email}
                onChange={handleChange}
                placeholder='ivanivanov@gmail.ru'
              />
            </div>

            <div className='flex flex-row justify-between items-center'>
              <Button type='submit' disabled={isLoading}>
                {isLoading ? <Spinner /> : 'Войти'}{' '}
              </Button>
              <div>
                Нет аккаунта?{' '}
                <Link
                  to={'/register'}
                  className='font-medium text-blue-600 underline hover:no-underline text-sm'
                >
                  Зарегистрироваться
                </Link>
              </div>
            </div>
          </form>
        </Card>
      </div>
    </div>
  )
}

export default LoginPage
