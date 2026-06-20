import { useState } from 'react'
import { Link, NavLink, Outlet } from 'react-router-dom'
import styles from './layout.module.css'

const navItems = [
  { to: '/gallery', label: 'Galerie' },
  { to: '/achievements', label: 'Úspěchy' },
  { to: '/coaches', label: 'Trenéři' },
  { to: '/events', label: 'Vystoupení' },
  { to: '/sponsors', label: 'Sponzoři' },
  { to: '/contact', label: 'Kontakt' },
  { to: '/partnership', label: 'Spolupráce' },
]

export function PublicLayout() {
  const [menuOpen, setMenuOpen] = useState(false)
  const closeMenu = () => setMenuOpen(false)

  return (
    <div className={styles.shell}>
      {/* desktop header */}
      <header className={styles.headerDesktop}>
        <Link to="/" className={styles.brand}>
          <span className={styles.mark}>S</span>
          <span className={styles.brandName}>MT&nbsp;Sorella</span>
        </Link>
        <nav className={styles.nav}>
          {navItems.map((item) => (
            <NavLink
              key={item.to}
              to={item.to}
              className={({ isActive }) =>
                isActive ? `${styles.navLink} ${styles.navLinkActive}` : styles.navLink
              }
            >
              {item.label}
            </NavLink>
          ))}
        </nav>
        <div className={styles.actions}>
          <Link to="/login" className={styles.loginBtn}>
            Přihlášení
          </Link>
          <Link to="/apply" className={styles.applyBtn}>
            Přihláška
          </Link>
        </div>
      </header>

      {/* mobile header */}
      <header className={styles.headerMobile}>
        <Link to="/" className={styles.brand}>
          <span className={styles.markSm}>S</span>
          <span className={styles.brandNameSm}>MT&nbsp;Sorella</span>
        </Link>
        <button
          type="button"
          className={styles.hamburger}
          aria-label="Menu"
          onClick={() => setMenuOpen(true)}
        >
          <span className={styles.hamburgerIcon} />
        </button>
      </header>

      {/* mobile menu overlay */}
      {menuOpen && (
        <div className={styles.overlay}>
          <div className={styles.overlayTop}>
            <span className={styles.overlayBrand}>MT&nbsp;Sorella</span>
            <button
              type="button"
              className={styles.overlayClose}
              aria-label="Zavřít menu"
              onClick={closeMenu}
            >
              ×
            </button>
          </div>
          <nav className={styles.overlayNav}>
            {navItems.map((item) => (
              <NavLink
                key={item.to}
                to={item.to}
                onClick={closeMenu}
                className={({ isActive }) =>
                  isActive
                    ? `${styles.overlayLink} ${styles.overlayLinkActive}`
                    : styles.overlayLink
                }
              >
                {({ isActive }) => (
                  <>
                    {item.label}
                    {isActive && <span className={styles.overlayDot} aria-hidden="true" />}
                  </>
                )}
              </NavLink>
            ))}
          </nav>
          <div className={styles.overlayFoot}>
            <Link to="/apply" onClick={closeMenu} className={styles.overlayApply}>
              Podat přihlášku
            </Link>
            <Link to="/login" onClick={closeMenu} className={styles.overlayLogin}>
              Přihlášení
            </Link>
          </div>
        </div>
      )}

      <main className={styles.mainFull}>
        <Outlet />
      </main>
    </div>
  )
}
