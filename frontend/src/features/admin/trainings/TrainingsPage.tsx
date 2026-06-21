import admin from '../shared/admin.module.css'
import styles from './TrainingsPage.module.css'
import { trainingsList } from './mockData'

/** Admin trainings view. Form inert until the backend (#3). */
export function TrainingsPage() {
  return (
    <div className={admin.twoCol}>
      <div className={admin.panel}>
        <h3 className={admin.panelTitle} style={{ marginBottom: 18 }}>
          Přidat trénink
        </h3>
        <form className={admin.formCol} onSubmit={(e) => e.preventDefault()}>
          <div className={admin.fieldRow}>
            <input className={admin.field} placeholder="Datum" />
            <input className={admin.field} placeholder="Čas" />
          </div>
          <input className={admin.field} placeholder="Místo" />
          <input className={admin.field} placeholder="Kategorie" />
          <label className={admin.check}>
            <input type="checkbox" className={admin.checkbox} />
            Opakující se trénink
          </label>
          <button type="submit" className={admin.btnPrimary} disabled>
            Přidat do rozpisu
          </button>
        </form>
      </div>

      <div>
        <h3 className={admin.subTitle}>Rozpis tréninků</h3>
        <div className={admin.listCol}>
          {trainingsList.map((t) => (
            <div key={t.id} className={admin.itemCard}>
              <div className={styles.date}>
                <div className={styles.day}>{t.day}</div>
                <div className={styles.dayDate}>{t.date}</div>
              </div>
              <div className={admin.itemMain}>
                <div className={admin.itemTitle}>
                  {t.time} · {t.cat}
                </div>
                <div className={admin.itemSub}>{t.place}</div>
              </div>
              <span className={admin.itemAction}>Upravit</span>
            </div>
          ))}
        </div>
      </div>
    </div>
  )
}
