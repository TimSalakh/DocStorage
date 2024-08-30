import { Sidebar } from 'flowbite-react'
import { HiUser } from 'react-icons/hi'
import { CgFeed } from 'react-icons/cg'
import { MdBugReport, MdChat } from 'react-icons/md'
import { HiChatAlt2 } from 'react-icons/hi'
import { IoIosSettings } from 'react-icons/io'
import { FaSignOutAlt } from 'react-icons/fa'
import BetaBanner from './BetaBanner'

const TeacherSidebar = () => {
  return (
    <Sidebar>
      <Sidebar.Items>
        <Sidebar.ItemGroup>
          <Sidebar.Item href='#' icon={HiUser}>
            Неподтвержденные
          </Sidebar.Item>
          <Sidebar.Item href='#' icon={CgFeed}>
            Лента публикаций
          </Sidebar.Item>
          <Sidebar.Item href='#' icon={HiChatAlt2}>
            Чаты
          </Sidebar.Item>
        </Sidebar.ItemGroup>
        <Sidebar.ItemGroup>
          <Sidebar.Item href='#' icon={IoIosSettings}>
            Настройки
          </Sidebar.Item>
          <Sidebar.Item href='#' icon={FaSignOutAlt}>
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

export default TeacherSidebar
