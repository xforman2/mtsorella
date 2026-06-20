import ui from '../shared/ui.module.css'
import styles from './PartnershipPage.module.css'

const reasons = [
  'Viditelnost loga na dresech, webu a sociálních sítích týmu.',
  'Spojení značky s hodnotami sportu, disciplíny a mládeže.',
  'Prezentace na soutěžích a vystoupeních po celé republice.',
]

/**
 * Public partnership page. The form is static / inert until the backend (#3).
 * Styling is 1:1 with the prototype.
 */
export function PartnershipPage() {
  return (
    <div>
      <section className={styles.header}>
        <div className={styles.headerInner}>
          <p className={ui.kickerLight}>Partnerství</p>
          <h1 className={ui.headingLight}>Staňte se partnerem</h1>
          <p className={ui.leadLight}>
            Podpořte mladé talenty a spojte svou značku s úspěšným týmem. Rádi připravíme spolupráci
            na míru.
          </p>
        </div>
      </section>

      <section className={styles.body}>
        <div>
          <h2 className={styles.infoTitle}>Proč nás podpořit</h2>
          <div className={styles.bullets}>
            {reasons.map((r) => (
              <div key={r} className={ui.bullet}>
                <span className={ui.bulletDot} aria-hidden="true" />
                <div className={styles.bText}>{r}</div>
              </div>
            ))}
          </div>
          <div className={styles.stats}>
            <div>
              <div className={styles.statNum}>6</div>
              <div className={styles.statLabel}>současných partnerů</div>
            </div>
            <div>
              <div className={styles.statNum}>38</div>
              <div className={styles.statLabel}>soutěží ročně</div>
            </div>
          </div>
        </div>

        <div className={ui.formCard}>
          <h3 className={styles.formTitle}>Mám zájem o spolupráci</h3>
          <form className={styles.form} onSubmit={(e) => e.preventDefault()}>
            <input className={ui.input} placeholder="Název firmy / jméno" />
            <input className={ui.input} placeholder="Kontaktní osoba" />
            <div className={ui.fieldRow}>
              <input className={ui.input} placeholder="E-mail" />
              <input className={ui.input} placeholder="Telefon" />
            </div>
            <select className={ui.select} defaultValue="">
              <option value="" disabled>
                Forma spolupráce
              </option>
              <option>Finanční podpora</option>
              <option>Materiální podpora</option>
              <option>Mediální partnerství</option>
              <option>Jiná</option>
            </select>
            <textarea className={ui.textarea} rows={4} placeholder="Vaše zpráva" />
            <button type="submit" className={ui.btnSolid} disabled>
              Odeslat dotaz
            </button>
          </form>
        </div>
      </section>
    </div>
  )
}
