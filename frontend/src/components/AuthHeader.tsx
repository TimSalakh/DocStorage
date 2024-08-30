import { Navbar } from 'flowbite-react'
import { LuDatabase } from 'react-icons/lu'

const AuthHeader = () => {
  return (
    <Navbar fluid rounded>
      <Navbar.Brand>
        <LuDatabase size={27} color='blue' className='mr-1' />
        <span className='self-center whitespace-nowrap text-2xl font-bold dark:text-white'>
          DocStorage
        </span>
      </Navbar.Brand>
    </Navbar>
  )
}

export default AuthHeader
