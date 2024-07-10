import React, { useState } from 'react'
import { RegisterInputs } from '../models/AuthInputsModels'
import { useAuth } from '../context/Context'
import { log } from 'console'

const RegisterPage = () => {
  const { registerUser } = useAuth()
  const [formData, setFormData] = useState<RegisterInputs>({
    name: '',
    surname: '',
    patronymic: '',
    email: '',
    password: '',
    isTeacher: false
  })

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value, type, checked } = e.target
    setFormData((prev) => ({
      ...prev,
      [name]: type === 'checkbox' ? checked : value
    }))
  }

  const handleSubmit = async () => {
    console.log('данные с формы переданы')
    registerUser(formData)
  }

  return (
    <div>
      <form className='flex flex-col justify-start' onSubmit={handleSubmit}>
        <div>
          <label htmlFor='name'>Имя</label>
          <input
            className='border border-black'
            name='name'
            value={formData.name}
            onChange={handleChange}
          />
        </div>
        <div>
          <label htmlFor='surname'>Фамилия</label>
          <input
            className='border border-black'
            name='surname'
            value={formData.surname}
            onChange={handleChange}
          />
        </div>
        <div>
          <label htmlFor='patronymic'>Отчество</label>
          <input
            className='border border-black'
            name='patronymic'
            value={formData.patronymic}
            onChange={handleChange}
          />
        </div>
        <div>
          <label htmlFor='email'>Почта</label>
          <input
            className='border border-black'
            name='email'
            type='email'
            value={formData.email}
            onChange={handleChange}
          />
        </div>
        <div>
          <label htmlFor='password'>Пароль</label>
          <input
            className='border border-black'
            name='password'
            type='password'
            value={formData.password}
            onChange={handleChange}
          />
        </div>
        <div>
          <label htmlFor='isTeacher'>Войти как учитель</label>
          <input
            name='isTeacher'
            type='checkbox'
            checked={formData.isTeacher}
            onChange={handleChange}
          />
        </div>
        <button type='submit'>Зарегистрироваться</button>
      </form>
    </div>
  )
}

export default RegisterPage
