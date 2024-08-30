import { Alert } from 'flowbite-react'
import { HiInformationCircle } from 'react-icons/hi'

export const TeacherInfoAlert = () => {
  return (
    <Alert color='success' className='mb-4' rounded icon={HiInformationCircle}>
      Обращаем ваше внимание на то, что все созданные аккаунты проходят
      модерацию. Подробнее про роль преподаватель можно прочитать{' '}
      <a className='text-sm text-green-900 underline' href='#'>
        здесь.
      </a>
    </Alert>
  )
}
