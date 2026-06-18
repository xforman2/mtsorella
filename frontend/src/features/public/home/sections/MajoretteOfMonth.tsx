import styles from '../home.module.css'
import type { Majorette } from '../types'

type Props = { majorette: Majorette }

export function MajoretteOfMonth({ majorette }: Props) {
  return (
    <section className={styles.majorette}>
      <div className={styles.majPhoto} aria-hidden="true" />
      <div>
        <p className={styles.kickerLight}>Mažoretka měsíce</p>
        <h2 className={styles.majName}>{majorette.name}</h2>
        <p className={styles.majMeta}>
          „{majorette.nickname}" · {majorette.category}
        </p>
        <p className={styles.majReason}>{majorette.reason}</p>
      </div>
    </section>
  )
}
