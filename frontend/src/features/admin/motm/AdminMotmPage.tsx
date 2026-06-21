import admin from '../shared/admin.module.css'
import styles from './AdminMotmPage.module.css'
import { currentMajorette, memberOptions, teamGoal } from './mockData'

/** Admin "majorette of the month & team goal". Forms inert until the backend (#3). */
export function AdminMotmPage() {
  return (
    <div className={admin.twoCol}>
      {/* Mažoretka měsíce */}
      <div className={admin.panel}>
        <h3 className={admin.panelTitle}>Mažoretka měsíce</h3>
        <p className={admin.hint} style={{ marginTop: 8 }}>
          Vyber členku ke zveřejnění na domovské stránce.
        </p>
        <div className={styles.current}>
          <span className={styles.currentAvatar}>{currentMajorette.initials}</span>
          <div style={{ flex: 1 }}>
            <div className={styles.currentName}>{currentMajorette.name}</div>
            <div className={styles.currentSub}>{currentMajorette.sub}</div>
          </div>
        </div>
        <select className={admin.field} style={{ marginTop: 14 }} defaultValue="">
          <option value="" disabled>
            Změnit výběr členky…
          </option>
          {memberOptions.map((m) => (
            <option key={m}>{m}</option>
          ))}
        </select>
        <button
          type="button"
          className={admin.btnPrimary}
          style={{ marginTop: 12, width: '100%' }}
          disabled
        >
          Zveřejnit výběr
        </button>
      </div>

      {/* Týmový cíl */}
      <div className={admin.panel}>
        <h3 className={admin.panelTitle}>Týmový cíl</h3>
        <div className={styles.goalTitle}>{teamGoal.title}</div>
        <div className={styles.goalRow}>
          <span className={styles.goalCurrent}>{teamGoal.currentFmt}</span>
          <span className={styles.goalTarget}>/ {teamGoal.targetFmt} b</span>
        </div>
        <div className={admin.progress}>
          <div className={admin.progressFill} style={{ width: `${teamGoal.pct}%` }} />
        </div>
        <form className={styles.goalForm} onSubmit={(e) => e.preventDefault()}>
          <input className={admin.field} placeholder="Název nového cíle" />
          <input className={admin.field} placeholder="Cílový počet bodů" />
          <button type="submit" className={admin.btnPrimary} disabled>
            Nastavit cíl
          </button>
        </form>
      </div>
    </div>
  )
}
