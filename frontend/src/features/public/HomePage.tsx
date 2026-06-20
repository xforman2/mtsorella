import { useHomeData } from './home/useHomeData'
import { Hero } from './home/sections/Hero'
import { About } from './home/sections/About'
import { AchievementsPreview } from './home/sections/AchievementsPreview'
import { MajoretteGoal } from './home/sections/MajoretteGoal'
import { GalleryPreview } from './home/sections/GalleryPreview'
import { UpcomingEvents } from './home/sections/UpcomingEvents'
import { Sponsors } from './home/sections/Sponsors'

export function HomePage() {
  const data = useHomeData()

  return (
    <>
      <Hero stats={data.heroStats} />
      <About about={data.about} />
      <AchievementsPreview achievements={data.achievements} />
      <MajoretteGoal majorette={data.majorette} goal={data.goal} />
      <GalleryPreview items={data.gallery} />
      <UpcomingEvents events={data.events} />
      <Sponsors sponsors={data.sponsors} />
    </>
  )
}
