import { Link } from 'react-router-dom'
import styles from '../home.module.css'
import type { HeroStat } from '../types'

type HeroProps = { stats: HeroStat[] }

export function Hero({ stats }: HeroProps) {
  return (
    <section className={styles.hero}>
      <div>
        <h1 className={styles.heroTitle}>Elegance v každém pohybu</h1>
        <p className={styles.heroSlogan}>
          Soutěžní mažoretkový tým MT&nbsp;Sorella. Disciplína, ladnost a vítězný týmový duch — od
          juniorek až po seniorky.
        </p>
      </div>
      <div className={styles.heroCtas}>
        <Link to="/coaches" className={styles.btnPrimary}>
          Poznejte náš tým
        </Link>
        <Link to="/gallery" className={styles.btnGhost}>
          Galerie
        </Link>
      </div>
      <div className={styles.statStrip}>
        {stats.map((s) => (
          <div key={s.label} className={styles.stat}>
            <div className={styles.statValue}>{s.value}</div>
            <div className={styles.statLabel}>{s.label}</div>
          </div>
        ))}
      </div>
    </section>
  )
}
