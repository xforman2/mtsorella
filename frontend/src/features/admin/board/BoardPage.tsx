import admin from '../shared/admin.module.css'
import styles from './BoardPage.module.css'
import { boardList } from './mockData'

/** Admin board (announcements) view. Form inert until the backend (#3). */
export function BoardPage() {
  return (
    <div className={admin.twoCol}>
      <div className={admin.panel}>
        <h3 className={admin.panelTitle}>Nový oznam</h3>
        <form
          className={admin.formCol}
          style={{ marginTop: 18 }}
          onSubmit={(e) => e.preventDefault()}
        >
          <input className={admin.field} placeholder="Nadpis oznamu" />
          <textarea
            className={`${admin.field} ${admin.textarea}`}
            rows={4}
            placeholder="Text oznamu"
          />
          <label className={admin.check}>
            <input type="checkbox" className={admin.checkbox} />
            Připnout jako důležité
          </label>
          <button type="submit" className={admin.btnPrimary} disabled>
            Publikovat oznam
          </button>
        </form>
      </div>

      <div>
        <h3 className={admin.subTitle}>Publikované oznamy</h3>
        <div className={admin.listCol}>
          {boardList.map((a) => (
            <div key={a.id} className={styles.announce}>
              <div className={styles.row}>
                {a.pinned && <span className={styles.pin}>Připnuté</span>}
                <span className={styles.title}>{a.title}</span>
                <span className={styles.date}>{a.date}</span>
              </div>
            </div>
          ))}
        </div>
      </div>
    </div>
  )
}
