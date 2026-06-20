import { Link } from 'react-router-dom'
import styles from '../shared/admin.module.css'
import { adminStatCards, applications, applicationStatus, pendingVideos } from './mockData'

/** Admin dashboard (Přehled). Dummy data until the backend (#3). */
export function AdminDashboardPage() {
  return (
    <div className={styles.page}>
      <div className={styles.statGrid}>
        {adminStatCards.map((c) => (
          <div key={c.label} className={styles.statCard}>
            <div className={styles.statNum}>{c.num}</div>
            <div className={styles.statLabel}>{c.label}</div>
          </div>
        ))}
      </div>

      <div className={styles.cols2}>
        {/* Nové přihlášky */}
        <div className={styles.card}>
          <div className={styles.cardHead}>
            <h3 className={styles.cardTitle}>Nové přihlášky</h3>
            <Link to="/admin/members" className={styles.cardLink}>
              Všechny ›
            </Link>
          </div>
          <div className={styles.list}>
            {applications.map((a) => {
              const s = applicationStatus(a.status)
              return (
                <div key={a.id} className={styles.row}>
                  <div className={styles.rowMain}>
                    <div className={styles.rowTitle}>
                      {a.child} ({a.age} r.)
                    </div>
                    <div className={styles.rowSub}>
                      {a.parent} · {a.cat} · {a.date}
                    </div>
                  </div>
                  <span
                    className={styles.badge}
                    style={{ color: s.color, background: s.background }}
                  >
                    {s.label}
                  </span>
                </div>
              )
            })}
          </div>
        </div>

        {/* Videa k hodnocení */}
        <div className={styles.card}>
          <div className={styles.cardHead}>
            <h3 className={styles.cardTitle}>Videa k hodnocení</h3>
            <Link to="/admin/videos" className={styles.cardLink}>
              Hodnotit ›
            </Link>
          </div>
          <div className={styles.list}>
            {pendingVideos.map((v) => (
              <div key={v.id} className={styles.row}>
                <span className={styles.playIcon}>▶</span>
                <div className={styles.rowMain}>
                  <div className={styles.rowTitle}>
                    {v.nick} · {v.challenge}
                  </div>
                  <div className={styles.rowSub}>{v.when}</div>
                </div>
              </div>
            ))}
          </div>
        </div>
      </div>
    </div>
  )
}
