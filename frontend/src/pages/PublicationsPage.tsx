import { useEffect, useState } from 'react'
import { PublicationType } from '../models/PublicationModels'
import { fetchPublicationsApiCall } from '../services/PublicationService'
import Publication from '../components/Publication'

const PublicationsPage = () => {
  const [publications, setPublications] = useState<PublicationType[] | null>(
    null
  )

  useEffect(() => {
    const fetchPubs = async () => {
      try {
        const response = await fetchPublicationsApiCall()
        setPublications(response!.data)
      } catch (error) {
        console.error(error)
      }
    }
    fetchPubs()
  }, [])

  return (
    <div className='h-full w-full bg-slate-50 flex flex-col justify-start overflow-y-auto no-scrollbar'>
      {publications?.map((publication) => (
        <Publication key={publication.id} {...publication} />
      ))}
    </div>
  )
}

export default PublicationsPage
