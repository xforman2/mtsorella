import { useHomeData } from './home/useHomeData'
import { Hero } from './home/sections/Hero'
import { About } from './home/sections/About'
import { AchievementsPreview } from './home/sections/AchievementsPreview'
import { MajoretteOfMonth } from './home/sections/MajoretteOfMonth'
import { TeamGoal } from './home/sections/TeamGoal'
import { GalleryPreview } from './home/sections/GalleryPreview'
import { UpcomingEvents } from './home/sections/UpcomingEvents'
import { Sponsors } from './home/sections/Sponsors'
import styles from './home/home.module.css'

export function HomePage() {
  const data = useHomeData()

  return (
    <div className={styles.page}>
      <Hero stats={data.heroStats} />
      <About about={data.about} />
      <AchievementsPreview achievements={data.achievements} />
      <MajoretteOfMonth majorette={data.majorette} />
      <TeamGoal goal={data.goal} />
      <GalleryPreview items={data.gallery} />
      <UpcomingEvents events={data.events} />
      <Sponsors sponsors={data.sponsors} />
    </div>
  )
}
