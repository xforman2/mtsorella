import admin from '../shared/admin.module.css'
import styles from './AdminSponsorsPage.module.css'
import { sponsorsFull } from '../../public/sponsors/mockData'

/** Admin sponsors management. Actions inert until the backend (#3). */
export function AdminSponsorsPage() {
  return (
    <div className={admin.page}>
      <div className={admin.headerRow}>
        <p className={admin.lead}>Správa partnerů a sponzorů zobrazených na webu.</p>
        <button type="button" className={admin.btnPrimary} disabled>
          + Přidat sponzora
        </button>
      </div>

      <div className={styles.grid}>
        {sponsorsFull.map((sp) => (
          <div key={sp.name} className={styles.card}>
            <div className={styles.logo}>{sp.name}</div>
            <p className={styles.desc}>{sp.desc}</p>
            <div className={`${styles.actions} ${admin.actionRow}`}>
              <span className={admin.actionEdit}>Upravit</span>
              <span className={admin.actionDanger}>Odstranit</span>
            </div>
          </div>
        ))}
      </div>
    </div>
  )
}
