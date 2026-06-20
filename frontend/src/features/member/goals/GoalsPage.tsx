import m from '../shared/member.module.css'
import styles from './GoalsPage.module.css'
import { goalHistory, teamGoal } from './mockData'

/** Member team goals (Cíle). Dummy data until the backend (#3). */
export function GoalsPage() {
  return (
    <div className={m.pageNarrow} style={{ maxWidth: 820 }}>
      <div className={m.eyebrow}>Společně</div>
      <h1 className={m.h1}>Týmové cíle</h1>

      <div className={styles.bigCard}>
        <span className={styles.pill}>Aktuální cíl</span>
        <h2 className={styles.bigTitle}>{teamGoal.title}</h2>
        <div className={styles.row}>
          <span className={styles.current}>{teamGoal.currentFmt}</span>
          <span className={styles.target}>/ {teamGoal.targetFmt} b</span>
        </div>
        <div className={styles.progress}>
          <div className={styles.progressFill} style={{ width: `${teamGoal.pct}%` }} />
        </div>
        <div className={styles.note}>
          {teamGoal.pct}% splněno · ještě {teamGoal.remainFmt} bodů do odměny
        </div>
      </div>

      <h2 className={m.sectionTitle} style={{ marginTop: 40 }}>
        Splněné cíle
      </h2>
      <div className={styles.histList}>
        {goalHistory.map((g) => (
          <div key={g.id} className={styles.histRow}>
            <span className={styles.check}>✓</span>
            <div className={styles.histMain}>
              <div className={styles.histTitle}>{g.title}</div>
              <div className={styles.histTarget}>Cíl {g.target} bodů</div>
            </div>
            <span className={styles.histDone}>Splněno</span>
          </div>
        ))}
      </div>
    </div>
  )
}
