import styles from '../page.module.css'

export function AdminHomePage() {
  return (
    <div className={styles.page}>
      <p className={styles.kicker}>Admin panel</p>
      <h1 className={styles.heading}>Administrácia</h1>
      <p className={styles.text}>
        Placeholder pre admin panel. Tu pribudne správa členiek, hodnotenie videí, výzvy a
        štatistiky (FR-A*).
      </p>
    </div>
  )
}
