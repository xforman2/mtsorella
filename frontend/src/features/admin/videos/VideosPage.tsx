import admin from '../shared/admin.module.css'
import styles from './VideosPage.module.css'
import { gradedVideos, pendingVideos } from './mockData'

/**
 * Admin video grading. Dummy data until the backend (#3); the slider and the
 * grade button are inert (disabled).
 */
export function VideosPage() {
  return (
    <div className={styles.wrap}>
      <p className={admin.lead}>
        Ohodnoť kvalitu provedení (0–20 bodů). K bodům za kvalitu se automaticky připočítá +10 za
        splnění výzvy.
      </p>

      <div className={styles.grid}>
        {pendingVideos.map((v) => (
          <div key={v.id} className={styles.card}>
            <div className={styles.thumb}>
              <div className={styles.thumbCenter}>
                <span className={styles.play}>▶</span>
              </div>
              <div className={styles.thumbTop}>
                <span className={styles.thumbAvatar}>{v.initials}</span>
                <span className={styles.thumbNick}>{v.nick}</span>
              </div>
            </div>
            <div className={styles.body}>
              <div className={styles.challenge}>{v.challenge}</div>
              <div className={styles.meta}>
                {v.cat} · odesláno {v.when}
              </div>
              <div className={styles.gradeRow}>
                <span className={styles.gradeLabel}>Kvalita provedení</span>
                <span className={styles.gradeVal}>
                  {v.sel}
                  <span className={styles.gradeMax}>/20</span>
                </span>
              </div>
              <input
                type="range"
                min={0}
                max={20}
                defaultValue={v.sel}
                disabled
                className={styles.slider}
              />
              <button type="button" className={styles.gradeBtn} disabled>
                Přidělit body (+{v.sel} a +10)
              </button>
            </div>
          </div>
        ))}
      </div>

      <div>
        <h3 className={admin.subTitle}>Ohodnoceno dnes</h3>
        <div className={admin.listCol}>
          {gradedVideos.map((v) => (
            <div key={v.id} className={styles.gradedRow}>
              <span className={styles.gradedCheck}>✓</span>
              <div className={admin.itemMain}>
                <div className={admin.itemTitle}>
                  {v.nick} · {v.challenge}
                </div>
                <div className={admin.itemSub}>Kvalita {v.quality}/20 + 10 za splnění</div>
              </div>
              <span className={styles.gradedTotal}>+{v.total} b</span>
            </div>
          ))}
        </div>
      </div>
    </div>
  )
}
