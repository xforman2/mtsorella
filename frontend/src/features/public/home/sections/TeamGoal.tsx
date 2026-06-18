import styles from '../home.module.css'
import type { TeamGoal as TeamGoalData } from '../types'

type Props = { goal: TeamGoalData }

const czk = new Intl.NumberFormat('cs-CZ')

export function TeamGoal({ goal }: Props) {
  const pct = Math.min(100, Math.round((goal.current / goal.target) * 100))
  return (
    <section className={styles.goal}>
      <p className={styles.kicker}>Týmový cíl</p>
      <h2 className={styles.heading}>{goal.title}</h2>
      <div className={styles.goalRow}>
        <span className={styles.goalCurrent}>{czk.format(goal.current)} Kč</span>
        <span className={styles.goalTarget}>z {czk.format(goal.target)} Kč</span>
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
      <p className={styles.goalNote}>Splněno {pct} % — díky všem, kdo nás podporují!</p>
    </section>
  )
}
