import { Link, NavLink, Outlet, useLocation } from 'react-router-dom'
import styles from './adminLayout.module.css'
import { adminNav } from './adminNav'

export function AdminLayout() {
  const { pathname } = useLocation()
  const title = adminNav.find((n) => n.to === pathname)?.label ?? 'Admin'

  return (
    <div className={styles.admin}>
      {/* sidebar (desktop) */}
      <aside className={styles.sidebar}>
        <div className={styles.brand}>
          <span className={styles.mark}>S</span>
          <div>
            <div className={styles.brandName}>MT&nbsp;Sorella</div>
            <div className={styles.brandSub}>ADMIN PANEL</div>
          </div>
        </div>
        <nav className={styles.nav}>
          {adminNav.map((item) => (
            <NavLink
              key={item.to}
              to={item.to}
              end={item.end}
              className={({ isActive }) =>
                isActive ? `${styles.navLink} ${styles.navLinkActive}` : styles.navLink
              }
            >
              {item.label}
            </NavLink>
          ))}
        </nav>
        <Link to="/" className={styles.back}>
          ← Zpět na web
        </Link>
      </aside>

      <div className={styles.body}>
        {/* topbar */}
        <header className={styles.topbar}>
          <div>
            <div className={styles.topEyebrow}>Administrace</div>
            <div className={styles.topTitle}>{title}</div>
          </div>
          <div className={styles.userChip}>
            <span className={styles.userAvatar}>AM</span>
            <div>
              <div className={styles.userName}>Andrea Mišíková</div>
              <div className={styles.userRole}>Hlavní trenérka</div>
            </div>
          </div>
        </header>

        {/* mobile chip nav */}
        <div className={styles.mobileNav}>
          {adminNav.map((item) => (
            <NavLink
              key={item.to}
              to={item.to}
              end={item.end}
              className={({ isActive }) =>
                isActive ? `${styles.chip} ${styles.chipActive}` : styles.chip
              }
            >
              {item.label}
            </NavLink>
          ))}
        </div>

        <main className={styles.content}>
          <Outlet />
        </main>
      </div>
    </div>
  )
}
