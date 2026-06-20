import { Link } from 'react-router-dom'
import ui from '../shared/ui.module.css'
import styles from './SponsorsPage.module.css'
import { sponsorsFull } from './mockData'

/** Public sponsors page. */
export function SponsorsPage() {
  return (
    <div>
      <section className={styles.header}>
        <div className={styles.headerInner}>
          <p className={ui.kicker}>Děkujeme</p>
          <h1 className={ui.heading}>Partneři a sponzoři</h1>
          <p className={styles.lead}>
            Bez podpory našich partnerů bychom nemohli reprezentovat. Děkujeme.
          </p>
        </div>
      </section>

      <section className={styles.body}>
        <div className={styles.grid}>
          {sponsorsFull.map((s) => (
            <div key={s.name} className={styles.card}>
              <div className={styles.logo}>{s.name}</div>
              <p className={styles.desc}>{s.desc}</p>
            </div>
          ))}
        </div>

        <div className={styles.cta}>
          <h2 className={styles.ctaTitle}>Chcete podpořit MT Sorella?</h2>
          <p className={styles.ctaText}>
            Staňte se partnerem týmu a podpořte mladé talenty. Ozvěte se nám.
          </p>
          <Link to="/partnership" className={`${ui.btnGhostLight} ${styles.ctaBtn}`}>
            Staňte se partnerem
          </Link>
        </div>
      </section>
    </div>
  )
}
