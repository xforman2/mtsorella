import { Link } from 'react-router-dom'
import styles from '../home.module.css'
import type { Sponsor } from '../types'

type Props = { sponsors: Sponsor[] }

export function Sponsors({ sponsors }: Props) {
  return (
    <section className={`${styles.band} ${styles.bandDark}`}>
      <div className={styles.sponsorsInner}>
        <p className={styles.kickerLight}>Děkujeme</p>
        <h2 className={styles.headingLight}>Naši partneři a sponzoři</h2>
        <div className={styles.sponsors}>
          {sponsors.map((sponsor) => (
            <div key={sponsor.name} className={styles.sponsor} title={sponsor.description}>
              {sponsor.name}
            </div>
          ))}
        </div>
        <Link to="/partnership" className={styles.sponsorBtn}>
          Staňte se partnerem
        </Link>
      </div>
    </section>
  )
}
