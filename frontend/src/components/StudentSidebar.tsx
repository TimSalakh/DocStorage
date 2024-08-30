import { Sidebar } from 'flowbite-react'
import { HiUser } from 'react-icons/hi'
import { CgFeed } from 'react-icons/cg'
import { MdBugReport, MdChat } from 'react-icons/md'
import { IoIosSettings } from 'react-icons/io'
import { FaSignOutAlt } from 'react-icons/fa'
import BetaBanner from './BetaBanner'
import { MdAddCircle } from 'react-icons/md'
import { HiChatAlt2 } from 'react-icons/hi'
import { HiLogout } from 'react-icons/hi'

const StudentSidebar = () => {
  return (
    <Sidebar>
      <Sidebar.Items>
        <Sidebar.ItemGroup>
          <Sidebar.Item href='#' icon={HiUser}>
            Мои публикации
          </Sidebar.Item>
          <Sidebar.Item href='/student-dashboard/publications' icon={CgFeed}>
            Лента публикаций
          </Sidebar.Item>
          <Sidebar.Item href='#' icon={HiChatAlt2}>
            Чаты
          </Sidebar.Item>
          <Sidebar.Item
            href='/student-dashboard/create-publication'
            icon={MdAddCircle}
          >
            Новая публикация
          </Sidebar.Item>
        </Sidebar.ItemGroup>
        <Sidebar.ItemGroup>
          <Sidebar.Item href='#' icon={IoIosSettings}>
            Настройки
          </Sidebar.Item>
          <Sidebar.Item href='#' icon={HiLogout}>
            Выйти
          </Sidebar.Item>
        </Sidebar.ItemGroup>
        <Sidebar.ItemGroup>
          <Sidebar.Item href='#' icon={MdBugReport}>
            Баг репорт
          </Sidebar.Item>
          <Sidebar.CTA>
            <BetaBanner />
          </Sidebar.CTA>
        </Sidebar.ItemGroup>
      </Sidebar.Items>
    </Sidebar>
  )
}

export default StudentSidebar
