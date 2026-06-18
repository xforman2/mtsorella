import styles from '../page.module.css'

export function DashboardPage() {
  return (
    <div className={styles.page}>
      <p className={styles.kicker}>Členská zóna</p>
      <h1 className={styles.heading}>Prehľad</h1>
      <p className={styles.text}>
        Placeholder pre člensky dashboard. Tu pribudne prehľad bodov, tréningy, výzvy a rebríček
        (FR-M*).
      </p>
    </div>
  )
}
