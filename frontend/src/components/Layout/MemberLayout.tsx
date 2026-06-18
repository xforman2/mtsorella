import { Link, Outlet } from 'react-router-dom'
import styles from './layout.module.css'

export function MemberLayout() {
  return (
    <div className={styles.shell}>
      <header className={styles.header}>
        <Link to="/app" className={styles.brand}>
          <span className={styles.mark}>S</span>
          Členská zóna
        </Link>
        <nav className={styles.nav}>
          <Link to="/">← Web</Link>
        </nav>
      </header>
      <main className={styles.main}>
        <Outlet />
      </main>
    </div>
  )
}
