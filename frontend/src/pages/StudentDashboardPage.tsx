import { Outlet } from 'react-router-dom'
import StudentSidebar from '../components/StudentSidebar'

const StudentDashboardPage = () => {
  return (
    <div className='flex flex-row h-screen w-screen overflow-hidden'>
      <div className='flex flex-row justify-end w-1/3 h-full bg-slate-50 border-r border-gray-200'>
        <StudentSidebar />
      </div>
      <div className='w-1/3 h-full bg-slate-50'>
        <Outlet />
      </div>
      <div className='w-1/3 h-full border-l border-gray-200 bg-slate-50'></div>
    </div>
  )
}

export default StudentDashboardPage
