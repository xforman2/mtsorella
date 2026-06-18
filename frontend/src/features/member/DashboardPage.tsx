import styles from '../page.module.css'

export function DashboardPage() {
  return (
    <div className={styles.page}>
      <p className={styles.kicker}>Členská zóna</p>
      <h1 className={styles.heading}>Přehled</h1>
      <p className={styles.text}>
        Zástupný obsah pro členský dashboard. Zde přibude přehled bodů, tréninky, výzvy a žebříček
        (FR-M*).
      </p>
    </div>
  )
}
