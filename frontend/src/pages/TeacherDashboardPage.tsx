import { Outlet } from 'react-router-dom'
import TeacherSidebar from '../components/TeacherSidebar'

const TeacherDashboardPage = () => {
  return (
    <div className='flex flex-row h-screen w-screen overflow-hidden'>
      <div className='flex flex-row justify-end w-1/3 h-full border-r border-black bg-slate-50'>
        <TeacherSidebar />
      </div>
      <div className='w-1/3 h-full bg-slate-50'>
        <Outlet />
      </div>
      <div className='w-1/3 h-full border-l border-black'></div>
    </div>
  )
}

export default TeacherDashboardPage
