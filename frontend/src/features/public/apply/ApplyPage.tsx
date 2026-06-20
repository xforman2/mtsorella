import ui from '../shared/ui.module.css'
import styles from './ApplyPage.module.css'

const benefits = [
  { title: 'Profesionální vedení', text: 'Zkušené trenérky a systematický trénink.' },
  { title: 'Soutěže a vystoupení', text: 'Domácí i mezinárodní soutěže.' },
  { title: 'Skvělá parta', text: 'Přátelství a týmový duch na celý život.' },
]

/**
 * Online registration page. Static / inert until the backend (#3) exists — the
 * form does not submit. Styling is 1:1 with the prototype.
 */
export function ApplyPage() {
  return (
    <div>
      <section className={`${ui.greenStripes} ${styles.hero}`}>
        <div className={ui.greenOverlay} aria-hidden="true" />
        <div className={styles.heroInner}>
          <span className={ui.kickerLight}>Přidej se k nám</span>
          <h1 className={ui.headingLight}>Online přihláška</h1>
          <p className={ui.leadLight}>
            Vyplň nezávaznou přihlášku a my se ti ozveme s dalšími kroky. Přijímáme dívky od 6 let.
          </p>
        </div>
      </section>

      <section className={styles.content}>
        <div>
          <h2 className={styles.infoTitle}>Co tě čeká</h2>
          <div className={styles.bullets}>
            {benefits.map((b) => (
              <div key={b.title} className={ui.bullet}>
                <span className={ui.bulletDot} aria-hidden="true" />
                <div>
                  <div className={ui.bulletTitle}>{b.title}</div>
                  <div className={ui.bulletText}>{b.text}</div>
                </div>
              </div>
            ))}
          </div>
          <div className={`${ui.infoBox} ${ui.infoBoxSoft}`}>
            První trénink je vždy nezávazný a zdarma. Stačí přijít a vyzkoušet si to.
          </div>
        </div>

        <div className={ui.formCard}>
          <h3 className={styles.formTitle}>Přihláška dítěte</h3>
          <form className={styles.form} onSubmit={(e) => e.preventDefault()}>
            <input className={ui.input} placeholder="Jméno a příjmení dítěte" />
            <div className={ui.fieldRow}>
              <input className={ui.input} placeholder="Datum narození" />
              <select className={ui.select} defaultValue="">
                <option value="" disabled>
                  Kategorie zájmu
                </option>
                <option>Juniorky (6–10)</option>
                <option>Kadetky (11–14)</option>
                <option>Seniorky (15+)</option>
                <option>Nevím, poraďte</option>
              </select>
            </div>
            <div className={ui.formDivider} aria-hidden="true" />
            <input className={ui.input} placeholder="Jméno rodiče / zákonného zástupce" />
            <div className={ui.fieldRow}>
              <input className={ui.input} placeholder="E-mail rodiče" />
              <input className={ui.input} placeholder="Telefon" />
            </div>
            <textarea
              className={ui.textarea}
              rows={3}
              placeholder="Předchozí zkušenosti s tancem (nepovinné)"
            />
            <label className={ui.checkRow}>
              <input type="checkbox" className={ui.checkbox} />
              Souhlasím se zpracováním osobních údajů za účelem vyřízení přihlášky.
            </label>
            <button type="submit" className={ui.btnSolid} disabled>
              Odeslat přihlášku
            </button>
          </form>
        </div>
      </section>
    </div>
  )
}
