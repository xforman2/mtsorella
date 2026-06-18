import { Link } from 'react-router-dom'
import styles from '../home.module.css'
import type { Sponsor } from '../types'

type Props = { sponsors: Sponsor[] }

export function Sponsors({ sponsors }: Props) {
  return (
    <section className={styles.sponsorsSection}>
      <p className={styles.kicker}>Děkujeme</p>
      <h2 className={styles.heading}>Naši partneři a sponzoři</h2>
      <div className={styles.sponsors}>
        {sponsors.map((sponsor) => (
          <div key={sponsor.name} className={styles.sponsor}>
            <p className={styles.sponsorName}>{sponsor.name}</p>
            <p className={styles.sponsorDesc}>{sponsor.description}</p>
          </div>
        ))}
      </div>
      <Link to="/partnership" className={styles.btnGhost}>
        Staňte se partnerem
      </Link>
    </section>
  )
}
