import { Link } from 'react-router-dom'
import ui from '../shared/ui.module.css'
import styles from './CoachesPage.module.css'
import { coaches } from './mockData'

/** Public coaches/leadership page. Member profiles stay private (members-only zone). */
export function CoachesPage() {
  return (
    <div>
      <section className={styles.header}>
        <div className={styles.headerInner}>
          <p className={ui.kicker}>Náš tým</p>
          <h1 className={ui.heading}>Trenéři a vedení</h1>
          <p className={styles.lead}>
            Za úspěchy MT&nbsp;Sorella stojí zkušené trenérské vedení. Profily našich členek jsou z
            důvodu ochrany soukromí přístupné jen v členské zóně.
          </p>
        </div>
      </section>

      <section className={styles.body}>
        <div className={styles.grid}>
          {coaches.map((c) => (
            <article key={c.id} className={styles.card}>
              <div className={styles.photo}>
                <span className={styles.photoLabel}>[ foto trenérky 3:2 ]</span>
              </div>
              <div className={styles.cardBody}>
                <div className={styles.name}>{c.name}</div>
                <div className={styles.role}>
                  {c.role} · {c.years} r.
                </div>
                <p className={styles.bio}>{c.bio}</p>
              </div>
            </article>
          ))}
        </div>

        <div className={styles.cta}>
          <h2 className={styles.ctaTitle}>Chceš tancovat s námi?</h2>
          <p className={styles.ctaText}>
            Přijímáme nové dívky do juniorské kategorie. Podej nezávaznou online přihlášku.
          </p>
          <Link to="/apply" className={`${ui.btnGhostLight} ${styles.ctaBtn}`}>
            Online přihláška
          </Link>
        </div>
      </section>
    </div>
  )
}
