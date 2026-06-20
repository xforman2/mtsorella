import { Link } from 'react-router-dom'
import ui from '../shared/ui.module.css'
import styles from './LoginPage.module.css'

/**
 * Public login page. Static / inert until the backend (#3) exists — the form
 * does not authenticate. Styling is 1:1 with the prototype.
 */
export function LoginPage() {
  return (
    <div className={styles.split}>
      <div className={`${ui.greenStripes} ${styles.welcome}`}>
        <div className={styles.welcomeOverlay} aria-hidden="true" />
        <div className={styles.welcomeContent}>
          <span className={ui.kickerLight}>Členská zóna</span>
          <h2 className={styles.welcomeTitle}>
            Vítej zpět,
            <br />
            Sorello
          </h2>
          <p className={styles.welcomeText}>
            Tvůj dashboard, tréninky, výzvy a žebříček čekají. Přihlaš se přes e-mail rodiče.
          </p>
        </div>
      </div>

      <div className={styles.formCol}>
        <div className={styles.formInner}>
          <h1 className={styles.title}>Přihlášení</h1>
          <p className={styles.subtitle}>Vstup jen pro členky týmu.</p>

          <form className={styles.form} onSubmit={(e) => e.preventDefault()}>
            <div className={ui.field}>
              <label className={ui.label} htmlFor="login-email">
                E-mail rodiče
              </label>
              <input
                id="login-email"
                type="email"
                className={ui.input}
                placeholder="rodic@email.cz"
                autoComplete="email"
              />
            </div>
            <div className={ui.field}>
              <label className={ui.label} htmlFor="login-password">
                Heslo
              </label>
              <input
                id="login-password"
                type="password"
                className={ui.input}
                placeholder="••••••"
                autoComplete="current-password"
              />
            </div>
            <button type="submit" className={ui.btnSolid} disabled>
              Přihlásit se
            </button>
            <button type="button" className={styles.forgot}>
              Zapomněla jsi heslo?
            </button>
          </form>

          <div className={`${ui.infoBox} ${ui.infoBoxAccent}`}>
            Registrace není veřejná — účty vytváří trenér. Přihlášení bude funkční po nasazení
            backendu.
          </div>

          <div className={styles.adminRow}>
            <Link to="/admin" className={styles.adminLink}>
              Vstup pro trenérky → Admin panel
            </Link>
          </div>
        </div>
      </div>
    </div>
  )
}
