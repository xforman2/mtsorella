import admin from '../shared/admin.module.css'
import styles from './AdminTrainersPage.module.css'
import { coaches } from '../../public/coaches/mockData'
import { initials } from '../members/mockData'

/** Admin trainers management. Form inert until the backend (#3). */
export function AdminTrainersPage() {
  return (
    <div className={admin.twoCol}>
      <div className={admin.panel}>
        <h3 className={admin.panelTitle}>Přidat trenéra / vedení</h3>
        <p className={admin.hint} style={{ marginTop: 6 }}>
          Trenéři označení „na webu" se zobrazují na veřejné stránce Trenéři.
        </p>
        <form
          className={admin.formCol}
          style={{ marginTop: 18 }}
          onSubmit={(e) => e.preventDefault()}
        >
          <input className={admin.field} placeholder="Jméno a příjmení" />
          <select className={admin.field} defaultValue="">
            <option value="" disabled>
              Role
            </option>
            <option>Hlavní trenérka</option>
            <option>Asistentka trenérky</option>
            <option>Choreografka</option>
          </select>
          <input className={admin.field} placeholder="Roky v týmu" />
          <textarea
            className={`${admin.field} ${admin.textarea}`}
            rows={3}
            placeholder="Krátký popis / bio"
          />
          <label className={admin.check}>
            <input type="checkbox" className={admin.checkbox} defaultChecked />
            Zobrazit na veřejném webu
          </label>
          <button type="submit" className={admin.btnPrimary} disabled>
            Přidat trenéra
          </button>
        </form>
      </div>

      <div>
        <h3 className={admin.subTitle}>Zobrazení na webu</h3>
        <div className={admin.listCol}>
          {coaches.map((t) => (
            <div key={t.id} className={styles.trainer}>
              <span className={styles.avatar}>{initials(t.name)}</span>
              <div style={{ flex: 1, minWidth: 0 }}>
                <div className={styles.name}>{t.name}</div>
                <div className={styles.role}>
                  {t.role} · {t.years} r.
                </div>
                <p className={styles.bio}>{t.bio}</p>
                <div className={`${styles.actions} ${admin.actionRow}`}>
                  <span className={admin.actionEdit}>Upravit</span>
                  <span className={admin.actionMuted}>Skrýt z webu</span>
                </div>
              </div>
              <span className={styles.badge}>Na webu</span>
            </div>
          ))}
        </div>
      </div>
    </div>
  )
}
