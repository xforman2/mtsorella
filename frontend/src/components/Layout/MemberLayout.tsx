import { useState } from 'react'
import { Link, NavLink, Outlet } from 'react-router-dom'
import shell from './layout.module.css'
import styles from './memberLayout.module.css'
import { me } from '../../features/member/me'

const navItems: { to: string; label: string; end?: boolean }[] = [
  { to: '/app', label: 'Přehled', end: true },
  { to: '/app/team', label: 'Tím' },
  { to: '/app/trainings', label: 'Tréninky' },
  { to: '/app/board', label: 'Nástěnka' },
  { to: '/app/challenges', label: 'Výzvy' },
  { to: '/app/leaderboard', label: 'Rebříček' },
  { to: '/app/goals', label: 'Cíle' },
  { to: '/app/profile', label: 'Profil' },
]

const bottomItems: { to: string; label: string; icon: string; end?: boolean }[] = [
  { to: '/app', label: 'Přehled', icon: '🏠', end: true },
  { to: '/app/trainings', label: 'Tréninky', icon: '🗓️' },
  { to: '/app/challenges', label: 'Výzvy', icon: '🎯' },
  { to: '/app/leaderboard', label: 'Rebříček', icon: '🏆' },
  { to: '/app/profile', label: 'Profil', icon: '👤' },
]

export function MemberLayout() {
  const [menuOpen, setMenuOpen] = useState(false)
  const closeMenu = () => setMenuOpen(false)

  return (
    <div className={shell.shell}>
      {/* desktop header */}
      <header className={shell.headerDesktop}>
        <Link to="/app" className={shell.brand}>
          <span className={shell.mark}>S</span>
          <span className={shell.brandName}>MT&nbsp;Sorella</span>
        </Link>
        <nav className={shell.nav}>
          {navItems.map((item) => (
            <NavLink
              key={item.to}
              to={item.to}
              end={item.end}
              className={({ isActive }) =>
                isActive ? `${shell.navLink} ${shell.navLinkActive}` : shell.navLink
              }
            >
              {item.label}
            </NavLink>
          ))}
        </nav>
        <div className={shell.actions}>
          <div className={styles.pointsBadge}>
            <span className={styles.pointsAvatar}>{me.initials}</span>
            <span className={styles.pointsVal}>{me.pointsFmt}</span>
            <span className={styles.pointsUnit}>b</span>
          </div>
          <Link to="/" className={styles.logout}>
            Odhlásit
          </Link>
        </div>
      </header>

      {/* mobile header */}
      <header className={shell.headerMobile}>
        <Link to="/app" className={shell.brand}>
          <span className={shell.markSm}>S</span>
          <span className={shell.brandNameSm}>MT&nbsp;Sorella</span>
        </Link>
        <button
          type="button"
          className={shell.hamburger}
          aria-label="Menu"
          onClick={() => setMenuOpen(true)}
        >
          <span className={shell.hamburgerIcon} />
        </button>
      </header>

      {/* mobile menu overlay */}
      {menuOpen && (
        <div className={shell.overlay}>
          <div className={shell.overlayTop}>
            <span className={shell.overlayBrand}>MT&nbsp;Sorella</span>
            <button
              type="button"
              className={shell.overlayClose}
              aria-label="Zavřít menu"
              onClick={closeMenu}
            >
              ×
            </button>
          </div>
          <nav className={shell.overlayNav}>
            {navItems.map((item) => (
              <NavLink
                key={item.to}
                to={item.to}
                end={item.end}
                onClick={closeMenu}
                className={({ isActive }) =>
                  isActive ? `${shell.overlayLink} ${shell.overlayLinkActive}` : shell.overlayLink
                }
              >
                {({ isActive }) => (
                  <>
                    {item.label}
                    {isActive && <span className={shell.overlayDot} aria-hidden="true" />}
                  </>
                )}
              </NavLink>
            ))}
          </nav>
          <div className={shell.overlayFoot}>
            <Link to="/" onClick={closeMenu} className={shell.overlayLogin}>
              Odhlásit
            </Link>
          </div>
        </div>
      )}

      <main className={styles.main}>
        <Outlet />
      </main>

      {/* mobile bottom nav */}
      <nav className={styles.bottomNav}>
        {bottomItems.map((item) => (
          <NavLink
            key={item.to}
            to={item.to}
            end={item.end}
            className={({ isActive }) =>
              isActive ? `${styles.bottomItem} ${styles.bottomItemActive}` : styles.bottomItem
            }
          >
            <span className={styles.bottomIcon}>{item.icon}</span>
            <span className={styles.bottomLabel}>{item.label}</span>
          </NavLink>
        ))}
      </nav>
    </div>
  )
}
