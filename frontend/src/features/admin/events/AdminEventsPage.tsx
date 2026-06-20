import admin from '../shared/admin.module.css'
import styles from './AdminEventsPage.module.css'
import { eventsFull } from '../../public/events/mockData'

/** Admin events management. Form inert until the backend (#3). */
export function AdminEventsPage() {
  return (
    <div className={admin.twoCol}>
      <div className={admin.panel}>
        <h3 className={admin.panelTitle}>Nové vystoupení / akce</h3>
        <p className={admin.hint} style={{ marginTop: 6 }}>
          Zobrazí se ve veřejném kalendáři vystoupení.
        </p>
        <form
          className={admin.formCol}
          style={{ marginTop: 18 }}
          onSubmit={(e) => e.preventDefault()}
        >
          <input className={admin.field} placeholder="Název vystoupení" />
          <div className={admin.fieldRow}>
            <input className={admin.field} placeholder="Datum" />
            <input className={admin.field} placeholder="Čas" />
          </div>
          <input className={admin.field} placeholder="Místo" />
          <select className={admin.field} defaultValue="">
            <option value="" disabled>
              Typ akce
            </option>
            <option>Vystoupení</option>
            <option>Soutěž</option>
            <option>Soustředění</option>
          </select>
          <button type="submit" className={admin.btnPrimary} disabled>
            Přidat do kalendáře
          </button>
        </form>
      </div>

      <div>
        <h3 className={admin.subTitle}>Naplánovaná vystoupení</h3>
        <div className={admin.listCol}>
          {eventsFull.map((e) => (
            <div key={e.id} className={admin.itemCard}>
              <div className={styles.date}>
                <div className={styles.day}>{e.day}</div>
                <div className={styles.month}>{e.month}</div>
              </div>
              <div className={admin.itemMain}>
                <div className={admin.itemTitle}>{e.title}</div>
                <div className={admin.itemSub}>{e.where}</div>
              </div>
              <div className={admin.actionRow}>
                <span className={admin.actionEdit}>Upravit</span>
                <span className={admin.actionDanger}>Zrušit</span>
              </div>
            </div>
          ))}
        </div>
      </div>
    </div>
  )
}
