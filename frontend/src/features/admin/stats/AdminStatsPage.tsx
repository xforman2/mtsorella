import admin from '../shared/admin.module.css'
import styles from './AdminStatsPage.module.css'
import { adminBars, adminKpis } from './mockData'

/** Admin statistics. Dummy data until the backend (#3). */
export function AdminStatsPage() {
  return (
    <div className={admin.page}>
      <div className={styles.kpiGrid}>
        {adminKpis.map((k) => (
          <div key={k.label} className={styles.kpiCard}>
            <div className={styles.kpiVal}>{k.val}</div>
            <div className={styles.kpiLabel}>{k.label}</div>
          </div>
        ))}
      </div>

      <div className={styles.chartCard}>
        <h3 className={styles.chartTitle}>Členky podle kategorie</h3>
        <div className={styles.bars}>
          {adminBars.map((b) => (
            <div key={b.label}>
              <div className={styles.barHead}>
                <span className={styles.barLabel}>{b.label}</span>
                <span className={styles.barCount}>{b.nTxt} členek</span>
              </div>
              <div className={admin.progress}>
                <div className={admin.progressFill} style={{ width: `${b.pct}%` }} />
              </div>
            </div>
          ))}
        </div>
      </div>
    </div>
  )
}
