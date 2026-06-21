import admin from '../shared/admin.module.css'
import styles from './CampsPage.module.css'
import { computeCampStatus, pastCamps, upcomingCamp } from '../../public/tabory/mockData'

/**
 * Admin camps management (FR-A15). The upcoming-camp editor + past-camps list are
 * inert (dummy data until the backend, #3), but the status banner is computed live
 * from the shared `computeCampStatus` helper — the same logic that gates the public
 * application (FE-16).
 */
export function CampsPage() {
  const status = computeCampStatus(upcomingCamp)

  return (
    <div className={admin.page}>
      <div className={admin.headerRow}>
        <p className={admin.lead}>
          Spravuj nadcházející tábor i jeho přihlášku a archiv minulých ročníků. Změny se projeví v
          sekci Tábory na webu.
        </p>
      </div>

      <div className={admin.cols2}>
        {/* upcoming camp editor */}
        <div className={admin.card}>
          <h2 className={admin.cardTitle}>Nadcházející tábor</h2>
          <p className={styles.hint}>
            Nastav údaje tábora a datum otevření přihlášek. Přihláška se na webu odemkne
            automaticky.
          </p>

          <div className={styles.statusBanner} style={{ background: status.statusBg }}>
            <span className={styles.statusDot} style={{ background: status.statusColor }} />
            <div>
              <div className={styles.statusLabel} style={{ color: status.statusColor }}>
                {status.statusLabel}
              </div>
              <div className={styles.statusSub}>Otevření přihlášek: {upcomingCamp.openLabel}</div>
            </div>
          </div>

          <form className={styles.form} onSubmit={(e) => e.preventDefault()}>
            <input
              className={admin.field}
              defaultValue={upcomingCamp.name}
              placeholder="Název tábora"
            />
            <div className={admin.fieldRow}>
              <input
                className={admin.field}
                defaultValue={upcomingCamp.dates}
                placeholder="Termín"
              />
              <input className={admin.field} defaultValue={upcomingCamp.price} placeholder="Cena" />
            </div>
            <input className={admin.field} defaultValue={upcomingCamp.place} placeholder="Místo" />
            <div className={admin.fieldRow}>
              <input
                className={admin.field}
                defaultValue={upcomingCamp.age}
                placeholder="Věk (např. 6–14 let)"
              />
              <input
                className={admin.field}
                defaultValue={upcomingCamp.capacity}
                placeholder="Kapacita"
              />
            </div>
            <label className={styles.dateLabel}>
              Datum otevření přihlášek
              <input
                className={admin.field}
                defaultValue={upcomingCamp.openLabel}
                placeholder="1. července 2026"
              />
            </label>
            <textarea
              className={`${admin.field} ${styles.textarea}`}
              rows={3}
              defaultValue={upcomingCamp.desc}
              placeholder="Popis tábora"
            />
            <label className={styles.override}>
              <input type="checkbox" className={styles.checkbox} />
              Otevřít přihlášky ihned (přepsat datum)
            </label>
            <button type="submit" className={admin.btnPrimary} disabled>
              Uložit tábor
            </button>
          </form>
        </div>

        {/* past camps list */}
        <div className={admin.card}>
          <div className={admin.cardHead}>
            <h2 className={admin.cardTitle}>Minulé tábory</h2>
            <button type="button" className={styles.addBtn} disabled>
              + Přidat tábor
            </button>
          </div>
          <div className={admin.list}>
            {pastCamps.map((c) => (
              <div key={c.id} className={admin.row}>
                <span className={styles.yearBadge}>{c.year}</span>
                <div className={admin.rowMain}>
                  <div className={admin.rowTitle}>{c.name}</div>
                  <div className={admin.rowSub}>
                    {c.dates} · {c.kids} dětí
                  </div>
                </div>
                <div className={styles.actions}>
                  <button type="button" className={styles.editBtn} disabled>
                    Upravit
                  </button>
                  <button type="button" className={styles.deleteBtn} disabled>
                    Smazat
                  </button>
                </div>
              </div>
            ))}
          </div>
        </div>
      </div>
    </div>
  )
}
