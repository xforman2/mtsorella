import styles from '../page.module.css'

export function AdminHomePage() {
  return (
    <div className={styles.page}>
      <p className={styles.kicker}>Admin panel</p>
      <h1 className={styles.heading}>Administrace</h1>
      <p className={styles.text}>
        Zástupný obsah pro admin panel. Zde přibude správa členek, hodnocení videí, výzvy a
        statistiky (FR-A*).
      </p>
    </div>
  )
}
