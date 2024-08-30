import { createBrowserRouter } from 'react-router-dom'
import App from '../App'
import RegisterPage from '../pages/RegisterPage'
import LoginPage from '../pages/LoginPage'
import ConfrimEmailPage from '../pages/ConfrimEmailPage'
import PermissionDeniedPage from '../pages/PermissionDeniedPage'
import ProtectedRoute from './ProtectedRoute'
import { Role } from '../models/UserModels'
import StudentDashboardPage from '../pages/StudentDashboardPage'
import TeacherDashboardPage from '../pages/TeacherDashboardPage'
import CreatePublicationPage from '../pages/CreatePublicationPage'
import PublicationsPage from '../pages/PublicationsPage'

export const router = createBrowserRouter([
  {
    path: '/',
    element: <App />,
    children: [
      {
        path: '/register',
        element: <RegisterPage />
      },
      {
        path: '/login',
        element: <LoginPage />
      },
      {
        path: '/confirm-email',
        element: <ConfrimEmailPage />
      },
      {
        path: '/permission-denied',
        element: <PermissionDeniedPage />
      },
      {
        path: '/student-dashboard',
        element: <StudentDashboardPage />,
        children: [
          {
            path: '/student-dashboard/create-publication',
            element: <CreatePublicationPage />
          },
          {
            path: '/student-dashboard/publications',
            element: <PublicationsPage />
          }
        ]
        /* element: (
          <ProtectedRoute role={Role.Student}>
            <StudentDashboard />
          </ProtectedRoute>
        ) */
      },
      {
        path: '/teacher-dashboard',
        element: <TeacherDashboardPage />
        /* element: (
          <ProtectedRoute role={Role.Teacher}>
            <TeacherDashboard />
          </ProtectedRoute>
        ) */
      }
    ]
  }
])
