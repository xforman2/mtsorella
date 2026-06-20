import { useState } from 'react'
import ui from '../shared/ui.module.css'
import styles from './AchievementsPage.module.css'
import { achievementFilters, achievementStats, achievementTimeline, type Medal } from './mockData'

const medalIcon: Record<Medal, string> = {
  gold: '🥇',
  silver: '🥈',
  bronze: '🥉',
}

/** Public achievements timeline. Filtering is client-side over dummy data until the backend (#3). */
export function AchievementsPage() {
  const [active, setActive] = useState<string>('Vše')
  const entries =
    active === 'Vše' ? achievementTimeline : achievementTimeline.filter((e) => e.type === active)

  return (
    <div>
      <section className={styles.header}>
        <div className={styles.headerInner}>
          <p className={ui.kickerLight}>Ocenění</p>
          <h1 className={styles.headerTitle}>Časová osa úspěchů</h1>
          <div className={styles.stats}>
            {achievementStats.map((s) => (
              <div key={s.label} className={styles.statCard}>
                <div className={styles.statNum}>{s.num}</div>
                <div className={styles.statLabel}>{s.label}</div>
              </div>
            ))}
          </div>
        </div>
      </section>

      <section className={styles.body}>
        <div className={styles.filters}>
          {achievementFilters.map((f) => (
            <button
              key={f}
              type="button"
              className={f === active ? `${ui.chip} ${ui.chipActive}` : ui.chip}
              onClick={() => setActive(f)}
            >
              {f}
            </button>
          ))}
        </div>
        <div className={styles.timeline}>
          <div className={styles.timelineLine} aria-hidden="true" />
          <div className={styles.entries}>
            {entries.map((e) => (
              <div key={e.id} className={styles.entry}>
                <span className={styles.dot} aria-hidden="true" />
                <article className={styles.card}>
                  <div className={styles.cardTop}>
                    <span className={styles.medal} data-medal={e.medal}>
                      {medalIcon[e.medal]}
                    </span>
                    <div style={{ flex: 1 }}>
                      <div className={styles.metaRow}>
                        <span className={styles.year}>{e.year}</span>
                        <span className={styles.type}>{e.type}</span>
                      </div>
                      <h3 className={styles.comp}>{e.comp}</h3>
                    </div>
                  </div>
                  <div className={styles.place}>{e.place}</div>
                  <p className={styles.desc}>{e.desc}</p>
                </article>
              </div>
            ))}
          </div>
        </div>
      </section>
    </div>
  )
}
