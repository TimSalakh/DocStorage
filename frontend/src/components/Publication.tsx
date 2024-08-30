import { PublicationType } from '../models/PublicationModels'
import { Avatar, Badge, Popover } from 'flowbite-react'
import { HiCheck } from 'react-icons/hi'

const Publication = ({
  date,
  author,
  name,
  type,
  description
}: PublicationType) => {
  return (
    <div className='h-auto w-full bg-white border-b border-b-gray-200 p-5'>
      <div className='flex flex-row justify-between mb-3'>
        <div className='flex flex-row justify-start items-start'>
          <Avatar rounded className='mr-2' />
          <div className='flex flex-col justify-start items-start'>
            <h3 className='tracking-tight text-gray-900'>{author}</h3>
            <span className='text-xs tracking-tight text-gray-900'>{date}</span>
          </div>
        </div>

        <div className='flex flex-col justify-start items-end'>
          <Badge color={'success'} className='mb-1' icon={HiCheck}></Badge>
        </div>
      </div>

      <div>
        <h3 className='mb-2 tracking-tight text-gray-900'>{name}</h3>
        <p className='tracking-tight text-gray-900'>{description}</p>
      </div>
    </div>
  )
}

export default Publication
