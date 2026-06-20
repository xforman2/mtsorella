import { Link } from 'react-router-dom'
import styles from '../home.module.css'
import type { Achievement, Medal } from '../types'

const medalIcon: Record<Medal, string> = {
  gold: '🥇',
  silver: '🥈',
  bronze: '🥉',
}

type Props = { achievements: Achievement[] }

export function AchievementsPreview({ achievements }: Props) {
  return (
    <section className={`${styles.band} ${styles.bandWhite}`}>
      <div className={styles.bandInner}>
        <div className={styles.headerRow}>
          <div>
            <p className={styles.kicker}>Ocenění</p>
            <h2 className={styles.heading}>Nejnovější úspěchy</h2>
          </div>
          <Link to="/achievements" className={styles.link}>
            Celá časová osa ›
          </Link>
        </div>
        <div className={styles.cards}>
          {achievements.map((a) => (
            <article key={`${a.year}-${a.competition}`} className={styles.card}>
              <div className={styles.cardTop}>
                <span className={styles.medal} data-medal={a.medal}>
                  {medalIcon[a.medal]}
                </span>
                <span className={styles.cardYear}>{a.year}</span>
              </div>
              <h3 className={styles.cardTitle}>{a.competition}</h3>
              <p className={styles.cardPlace}>{a.placement}</p>
              <p className={styles.cardDesc}>{a.description}</p>
            </article>
          ))}
        </div>
      </div>
    </section>
  )
}
