import ui from '../shared/ui.module.css'
import styles from './ContactPage.module.css'

/**
 * Public contact page. The form is static / inert until the backend (#3).
 * Styling is 1:1 with the prototype.
 */
export function ContactPage() {
  return (
    <section className={styles.section}>
      <p className={ui.kicker}>Spojme se</p>
      <h1 className={ui.heading}>Kontakt</h1>

      <div className={styles.grid}>
        <div className={styles.card}>
          <h2 className={styles.cardTitle}>Napište nám</h2>
          <p className={styles.cardLead}>
            Máte zájem o členství nebo spolupráci? Vyplňte formulář.
          </p>
          <form className={styles.form} onSubmit={(e) => e.preventDefault()}>
            <input className={ui.input} placeholder="Jméno a příjmení" />
            <input className={ui.input} placeholder="E-mail" />
            <textarea className={ui.textarea} rows={4} placeholder="Vaše zpráva" />
            <button type="submit" className={ui.btnSolid} disabled>
              Odeslat zprávu
            </button>
          </form>
        </div>

        <div className={styles.infoCol}>
          <div className={styles.infoCard}>
            <h3 className={styles.infoTitle}>Kde nás najdete</h3>
            <div className={styles.infoList}>
              <div>Tělocvična ZŠ Komenského, 949 01 Nitra</div>
              <div>info@mtsorella.sk</div>
              <div>+421 905 123 456</div>
              <div>Tréninky Po / St / Pá</div>
            </div>
          </div>
          <div className={styles.map}>
            <span className={styles.mapLabel}>[ mapa · místo tréninků ]</span>
          </div>
        </div>
      </div>
    </section>
  )
}
