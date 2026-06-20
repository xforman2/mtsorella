import admin from '../shared/admin.module.css'
import styles from './AdminAchievementsPage.module.css'
import { achievementTimeline } from '../../public/achievements/mockData'

/** Admin achievements management. Form inert until the backend (#3). */
export function AdminAchievementsPage() {
  return (
    <div className={admin.twoCol}>
      <div className={admin.panel}>
        <h3 className={admin.panelTitle}>Přidat úspěch</h3>
        <p className={admin.hint} style={{ marginTop: 6 }}>
          Zobrazí se na veřejné časové ose úspěchů.
        </p>
        <form
          className={admin.formCol}
          style={{ marginTop: 18 }}
          onSubmit={(e) => e.preventDefault()}
        >
          <input className={admin.field} placeholder="Název soutěže" />
          <div style={{ display: 'flex', gap: 12 }}>
            <input
              className={admin.field}
              style={{ width: 100, flexShrink: 0 }}
              placeholder="Rok"
            />
            <select className={admin.field} style={{ flex: 1, minWidth: 0 }} defaultValue="">
              <option value="" disabled>
                Typ soutěže
              </option>
              <option>Národní</option>
              <option>Mezinárodní</option>
              <option>Regionální</option>
            </select>
          </div>
          <div style={{ display: 'flex', gap: 12 }}>
            <input
              className={admin.field}
              style={{ flex: 1, minWidth: 0 }}
              placeholder="Umístění (např. 1. místo)"
            />
            <select className={admin.field} style={{ width: 120, flexShrink: 0 }} defaultValue="">
              <option value="" disabled>
                Medaile
              </option>
              <option>Zlato</option>
              <option>Stříbro</option>
              <option>Bronz</option>
            </select>
          </div>
          <textarea
            className={`${admin.field} ${admin.textarea}`}
            rows={3}
            placeholder="Krátký popis úspěchu"
          />
          <button type="submit" className={admin.btnPrimary} disabled>
            Přidat úspěch
          </button>
        </form>
      </div>

      <div>
        <h3 className={admin.subTitle}>Existující úspěchy</h3>
        <div className={admin.listCol}>
          {achievementTimeline.map((a) => (
            <div key={a.id} className={admin.itemCard}>
              <span className={styles.year}>{a.year}</span>
              <div className={admin.itemMain}>
                <div className={admin.itemTitle}>{a.comp}</div>
                <div className={styles.place}>{a.place}</div>
              </div>
              <div className={admin.actionRow}>
                <span className={admin.actionEdit}>Upravit</span>
                <span className={admin.actionDanger}>Smazat</span>
              </div>
            </div>
          ))}
        </div>
      </div>
    </div>
  )
}
