import {
  Button,
  Card,
  FileInput,
  Label,
  Select,
  Textarea,
  TextInput
} from 'flowbite-react'

const lorem =
  'Lorem ipsum dolor sit amet consectetur adipisicing elit. Sapiente, eos?,Lorem ipsum dolor sit amet consectetur adipisicing elit. Sapiente, eos?'

const CreatePublicationPage = () => {
  return (
    <div className='w-full h-full p-5'>
      <form className='flex flex-col justify-start'>
        <div className='mb-2'>
          <Label htmlFor='email'>Название</Label>
          <TextInput required={true} placeholder='Дайте название публикации' />
        </div>

        <div className='mb-2'>
          <Label htmlFor='countries' value='Тип' />
          <Select id='type' required>
            <option>Курсовая работа</option>
            <option>Реферат</option>
            <option>Самостоятельная работа</option>
            <option>Эссе</option>
            <option>Сообщение</option>
          </Select>
        </div>

        <div className='mb-5'>
          <Label htmlFor='description' value='Описание' />
          <Textarea placeholder='Опишите свою публикацию' required rows={3} />
        </div>

        <div className='mb-5'>
          <Label
            htmlFor='dropzone-file'
            className='flex h-64 w-full cursor-pointer flex-col items-center justify-center rounded-lg border-2 border-dashed border-gray-300 bg-gray-50 hover:bg-gray-100'
          >
            <div className='flex flex-col items-center justify-center pb-6 pt-5'>
              <svg
                className='mb-2 h-8 w-8 text-gray-500'
                aria-hidden='true'
                xmlns='http://www.w3.org/2000/svg'
                fill='none'
                viewBox='0 0 20 16'
              >
                <path
                  stroke='currentColor'
                  strokeLinecap='round'
                  strokeLinejoin='round'
                  strokeWidth='2'
                  d='M13 13h3a3 3 0 0 0 0-6h-.025A5.56 5.56 0 0 0 16 6.5 5.5 5.5 0 0 0 5.207 5.021C5.137 5.017 5.071 5 5 5a4 4 0 0 0 0 8h2.167M10 15V6m0 0L8 8m2-2 2 2'
                />
              </svg>
              <p className='mb-3 text-sm text-gray-500 dark:text-gray-400'>
                Нажмите или перетащите файл для загрузки
              </p>
              <p className='text-xs text-gray-500 dark:text-gray-400'>
                DOCX, XLSX, PDF, TXT (MAX. 10MB)
              </p>
            </div>
            <FileInput id='dropzone-file' className='hidden' />
          </Label>
        </div>

        <div>
          <Button>Создать</Button>
        </div>
      </form>
    </div>
  )
}

export default CreatePublicationPage
