import React, { useEffect, useState } from 'react'
import { useAuth } from '../context/Context'
import { ConfirmEmailInputs } from '../models/AuthInputsModels'
import { useNavigate } from 'react-router-dom'
import AuthHeader from '../components/AuthHeader'
import { Button, Card, Label, Spinner, TextInput } from 'flowbite-react'

const ConfrimEmailPage = () => {
  const [isLoading, setIsLoading] = useState<boolean>(false)
  const [email, setEmail] = useState<string | null>(null)
  const navigate = useNavigate()
  const { confirmEmail: confirmEmail } = useAuth()
  const [formData, setFormData] = useState<ConfirmEmailInputs>({
    email: '',
    code: ''
  })

  useEffect(() => {
    const userEmail = localStorage.getItem('user-email')
    console.log(userEmail)

    if (!userEmail) {
      navigate('/register')
    } else {
      setEmail(userEmail)
      setFormData((prev) => ({
        ...prev,
        email: userEmail
      }))
    }
  }, [])

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target
    setFormData((prev) => ({
      ...prev,
      [name]: value
    }))
  }

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault()
    confirmEmail(formData)
  }

  return (
    <div className='w-screen h-screen flex flex-col'>
      <AuthHeader />

      <div className='w-full h-full flex flex-row justify-center items-center'>
        <Card className='w-1/5 flex flex-col justify-start'>
          <h6 className='text-lg font-semibold mb-3'>
            Мы отправили 4-х значный код подтверждения по адресу{' '}
            <span className='underline underline-offset-3 decoration-5 decoration-blue-600'>
              {email}
            </span>
          </h6>
          <form
            className='flex flex-row justify-start w-full'
            onSubmit={handleSubmit}
          >
            <div className='flex flex-row justify-between w-full'>
              <TextInput
                className='w-full'
                required={true}
                name='code'
                value={formData.code}
                onChange={handleChange}
                placeholder='Введите код'
              />
              <Button className='ml-3' type='submit' disabled={isLoading}>
                {isLoading ? <Spinner /> : 'Подтвердить'}{' '}
              </Button>
            </div>
          </form>
        </Card>
      </div>
    </div>
  )
}

export default ConfrimEmailPage
