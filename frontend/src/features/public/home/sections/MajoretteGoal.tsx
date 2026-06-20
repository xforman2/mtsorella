import styles from '../home.module.css'
import type { Majorette, TeamGoal } from '../types'

type Props = { majorette: Majorette; goal: TeamGoal }

const cs = new Intl.NumberFormat('cs-CZ')

export function MajoretteGoal({ majorette, goal }: Props) {
  const pct = Math.min(100, Math.round((goal.current / goal.target) * 100))
  const remaining = Math.max(0, goal.target - goal.current)

  return (
    <section className={`${styles.center} ${styles.duo}`}>
      {/* Mažoretka měsíce */}
      <div className={styles.motm}>
        <div className={styles.motmHead}>
          <span className={styles.pillDark}>Mažoretka měsíce</span>
        </div>
        <div className={styles.motmBody}>
          <div className={styles.motmPhoto} aria-hidden="true" />
          <div>
            <div className={styles.motmName}>{majorette.name}</div>
            <div className={styles.motmMeta}>
              „{majorette.nickname}" · {majorette.category}
            </div>
            <p className={styles.motmReason}>{majorette.reason}</p>
          </div>
        </div>
      </div>

      {/* Týmový cíl */}
      <div className={styles.goal}>
        <span className={styles.pillSoft}>Týmový cíl</span>
        <h3 className={styles.goalTitle}>{goal.title}</h3>
        <p className={styles.goalDesc}>
          Když společně nasbíráme {cs.format(goal.target)} bodů, čeká nás týmová odměna.
        </p>
        <div className={styles.goalFoot}>
          <div className={styles.goalRow}>
            <span className={styles.goalCurrent}>{cs.format(goal.current)}</span>
            <span className={styles.goalTarget}>/ {cs.format(goal.target)} b</span>
          </div>
          <div
            className={styles.progress}
            role="progressbar"
            aria-valuenow={pct}
            aria-valuemin={0}
            aria-valuemax={100}
          >
            <div className={styles.progressFill} style={{ width: `${pct}%` }} />
          </div>
          <p className={styles.goalNote}>
            {pct} % splněno · ještě {cs.format(remaining)} bodů
          </p>
        </div>
      </div>
    </section>
  )
}
