import { Link, Outlet } from 'react-router-dom'
import styles from './layout.module.css'

export function PublicLayout() {
  return (
    <div className={styles.shell}>
      <header className={styles.header}>
        <Link to="/" className={styles.brand}>
          <span className={styles.mark}>S</span>
          MT&nbsp;Sorella
        </Link>
        <nav className={styles.nav}>
          <Link to="/">Domov</Link>
          <Link to="/app">Členská zóna</Link>
          <Link to="/admin">Admin</Link>
        </nav>
      </header>
      <main className={styles.main}>
        <Outlet />
      </main>
    </div>
  )
}
