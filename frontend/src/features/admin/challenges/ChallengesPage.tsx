import admin from '../shared/admin.module.css'
import { challengesActive } from './mockData'

/** Admin challenges view. Form inert until the backend (#3). */
export function ChallengesPage() {
  return (
    <div className={admin.twoCol}>
      <div className={admin.panel}>
        <h3 className={admin.panelTitle}>Nová výzva</h3>
        <form
          className={admin.formCol}
          style={{ marginTop: 18 }}
          onSubmit={(e) => e.preventDefault()}
        >
          <input className={admin.field} placeholder="Název výzvy" />
          <textarea
            className={`${admin.field} ${admin.textarea}`}
            rows={3}
            placeholder="Popis a instrukce"
          />
          <div style={{ display: 'flex', gap: 12 }}>
            <input className={admin.field} style={{ flex: 1, minWidth: 0 }} placeholder="Termín" />
            <input
              className={admin.field}
              style={{ width: 90, flexShrink: 0 }}
              placeholder="Body"
            />
          </div>
          <div className={admin.upload}>Nahrát instruktážní video</div>
          <button type="submit" className={admin.btnPrimary} disabled>
            Vytvořit výzvu
          </button>
        </form>
      </div>

      <div>
        <h3 className={admin.subTitle}>Aktivní výzvy</h3>
        <div className={admin.listCol}>
          {challengesActive.map((c) => (
            <div key={c.id} className={admin.itemCard}>
              <div className={admin.itemMain}>
                <div className={admin.itemTitle}>{c.title}</div>
                <div className={admin.itemSub}>
                  Termín {c.deadline} · +{c.points} b
                </div>
              </div>
              <span className={admin.itemAction}>Upravit</span>
            </div>
          ))}
        </div>
      </div>
    </div>
  )
}
