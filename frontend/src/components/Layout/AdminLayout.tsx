import { Link, Outlet } from 'react-router-dom'
import styles from './layout.module.css'

export function AdminLayout() {
  return (
    <div className={styles.shell}>
      <header className={styles.header}>
        <Link to="/admin" className={styles.brand}>
          <span className={styles.mark}>S</span>
          Admin panel
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
