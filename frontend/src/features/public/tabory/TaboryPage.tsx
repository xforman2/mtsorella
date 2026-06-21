import { useState } from 'react'
import ui from '../shared/ui.module.css'
import styles from './TaboryPage.module.css'
import {
  computeCampStatus,
  pastCamps,
  taborPrihlaska,
  upcomingCamp,
  type CampOverride,
} from './mockData'

/**
 * Public Tábory (summer camps) page. The application is date-driven (FE-16):
 * locked with a countdown until the open date, then a working form that submits
 * to a local confirmation (no backend yet, #3). `override` mirrors the prototype's
 * `taborPrihlaska` tweak — pass 'open' / 'locked' to force a state.
 */
export function TaboryPage({ override = taborPrihlaska }: { override?: CampOverride }) {
  const [sent, setSent] = useState(false)
  const status = computeCampStatus(upcomingCamp, override)

  return (
    <div>
      {/* hero */}
      <section className={`${ui.greenStripes} ${styles.hero}`}>
        <div className={ui.greenOverlay} />
        <div className={styles.heroInner}>
          <p className={ui.kickerLight}>Příměstské tábory</p>
          <h1 className={ui.headingLight}>Letní tábory MT Sorella</h1>
          <p className={ui.leadLight}>
            Každé léto pořádáme příměstský tábor plný tance, her a nových kamarádství. Podívej se na
            minulé ročníky a přihlas se na ten nadcházející.
          </p>
        </div>
      </section>

      {/* upcoming camp + application */}
      <section className={styles.grid}>
        <article className={styles.upcoming}>
          <div className={styles.photo}>
            <span className={styles.badge}>Nadcházející tábor</span>
            <span className={styles.photoLabel}>Přetáhni sem fotku z tábora</span>
          </div>
          <div className={styles.upcomingBody}>
            <h2 className={styles.campName}>{upcomingCamp.name}</h2>
            <p className={styles.campDesc}>{upcomingCamp.desc}</p>
            <div className={styles.details}>
              <div className={styles.detail}>
                <span className={styles.detailLabel}>Termín</span>
                <span className={styles.detailValue}>{upcomingCamp.dates}</span>
              </div>
              <div className={styles.detail}>
                <span className={styles.detailLabel}>Místo</span>
                <span className={styles.detailValue}>{upcomingCamp.place}</span>
              </div>
              <div className={styles.detail}>
                <span className={styles.detailLabel}>Věk</span>
                <span className={styles.detailValue}>{upcomingCamp.age}</span>
              </div>
              <div className={styles.detail}>
                <span className={styles.detailLabel}>Cena</span>
                <span className={styles.detailValue}>{upcomingCamp.price}</span>
              </div>
            </div>
          </div>
        </article>

        <article className={styles.apply}>
          {status.locked ? (
            <div className={styles.locked}>
              <span className={styles.lockIcon} aria-hidden="true">
                🔒
              </span>
              <h2 className={styles.lockedTitle}>Přihlášky se zatím nepřijímají</h2>
              <p className={styles.lockedText}>
                Přihlášku na <strong>{upcomingCamp.name}</strong> otevřeme {upcomingCamp.openLabel}.
                Zatím si termín poznač do kalendáře — místa bývají rychle obsazená.
              </p>
              <div className={styles.countdown}>
                <span className={styles.countNum}>{status.daysTxt}</span>
                <span className={styles.countText}>
                  dní do
                  <br />
                  otevření přihlášek
                </span>
              </div>
              <button type="button" className={styles.lockedBtn} disabled>
                🔒 Přihláška uzamčena
              </button>
            </div>
          ) : sent ? (
            <div className={styles.confirm}>
              <span className={styles.confirmIcon} aria-hidden="true">
                ✓
              </span>
              <h2 className={styles.confirmTitle}>Přihláška odeslána!</h2>
              <p className={styles.confirmText}>
                Děkujeme za přihlášku na {upcomingCamp.name}. Ozveme se vám na uvedený e-mail s
                pokyny a platebními údaji.
              </p>
              <button type="button" className={styles.resetBtn} onClick={() => setSent(false)}>
                Přihlásit další dítě
              </button>
            </div>
          ) : (
            <>
              <div className={styles.statusBar}>
                <span className={styles.statusLabel}>Online přihláška</span>
                <span className={styles.openBadge}>Otevřeno</span>
              </div>
              <h2 className={styles.formTitle}>Přihlásit na tábor</h2>
              <form
                className={styles.form}
                onSubmit={(e) => {
                  e.preventDefault()
                  setSent(true)
                }}
              >
                <input className={ui.input} placeholder="Jméno a příjmení dítěte" />
                <div className={ui.fieldRow}>
                  <input className={ui.input} placeholder="Datum narození" />
                  <input className={ui.input} placeholder="Telefon rodiče" />
                </div>
                <input className={ui.input} placeholder="Jméno rodiče / zákonného zástupce" />
                <input className={ui.input} type="email" placeholder="E-mail rodiče" />
                <textarea
                  className={ui.textarea}
                  rows={2}
                  placeholder="Poznámka (alergie, zdravotní omezení…)"
                />
                <label className={ui.checkRow}>
                  <input type="checkbox" className={ui.checkbox} />
                  Souhlasím se zpracováním osobních údajů za účelem přihlášení na tábor.
                </label>
                <button type="submit" className={`${ui.btnSolid} ${styles.submitBtn}`}>
                  Odeslat přihlášku
                </button>
              </form>
            </>
          )}
        </article>
      </section>

      {/* past camps */}
      <section className={styles.pastSection}>
        <p className={ui.kicker}>Galerie táborů</p>
        <h2 className={ui.heading}>Minulé ročníky</h2>
        <div className={styles.pastGrid}>
          {pastCamps.map((c) => (
            <article key={c.id} className={styles.pastCard}>
              <div className={styles.pastPhoto}>
                <span className={styles.yearBadge}>{c.year}</span>
                <span className={styles.photoLabel}>Přetáhni sem fotku z tábora</span>
              </div>
              <div className={styles.pastBody}>
                <h3 className={styles.pastName}>{c.name}</h3>
                <div className={styles.pastDates}>{c.dates}</div>
                <p className={styles.pastDesc}>{c.desc}</p>
                <div className={styles.pastFooter}>
                  <span className={styles.pastPlace}>{c.place}</span>
                  <span className={styles.pastKids}>{c.kids} dětí</span>
                </div>
              </div>
            </article>
          ))}
        </div>
      </section>
    </div>
  )
}
