import { Badge } from 'flowbite-react'

const BetaBanner = () => {
  return (
    <>
      <div className='mb-3 flex items-center'>
        <Badge color='warning'>Beta</Badge>
      </div>
      <div className='mb-3 text-sm text-cyan-900 dark:text-gray-400'>
        Сервис находится на этапе бета-тестирования. Мы будем рады, если вы
        сообщите об ошибке, приложив ее подробное описание.
      </div>
      <a className='text-sm text-cyan-900 underline' href='#'>
        Сообщить об ошибке
      </a>
    </>
  )
}

export default BetaBanner
