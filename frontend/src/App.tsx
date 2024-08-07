import { Outlet } from 'react-router-dom'
import { UserProvider } from './context/Context'

const App = () => {
  return (
    <>
      <UserProvider>
        <Outlet />
      </UserProvider>
    </>
  )
}

export default App
