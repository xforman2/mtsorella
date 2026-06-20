import styles from './LeaderboardPage.module.css'
import { lbCategories, lbList, podium } from './mockData'

/** Member leaderboard (Rebříček). Season/category toggles inert until the backend (#3). */
export function LeaderboardPage() {
  return (
    <div>
      <section className={styles.hero}>
        <div className={styles.heroInner}>
          <div className={styles.eyebrow}>Rebříček</div>
          <h1 className={styles.h1}>Síň slávy</h1>
          <div className={styles.toggle}>
            <button
              type="button"
              className={`${styles.toggleBtn} ${styles.toggleBtnActive}`}
              disabled
            >
              Sezónní
            </button>
            <button type="button" className={styles.toggleBtn} disabled>
              Celoživotní
            </button>
          </div>

          <div className={styles.podium}>
            {podium.map((p) => (
              <div key={p.rank} className={styles.podiumItem}>
                <div className={styles.podiumMedal}>{p.medal}</div>
                <div className={styles.podiumAvatar} style={{ border: `3px solid ${p.ring}` }}>
                  {p.initials}
                </div>
                <div className={styles.podiumNick}>{p.nick}</div>
                <div className={styles.podiumCat}>{p.cat}</div>
                <div className={styles.podiumBar} style={{ height: p.h }}>
                  <div className={styles.podiumPts}>{p.points}</div>
                  <div className={styles.podiumPtsLabel}>bodů</div>
                </div>
              </div>
            ))}
          </div>
        </div>
      </section>

      <section className={styles.body}>
        <div className={styles.chips}>
          {lbCategories.map((c, i) => (
            <button
              key={c}
              type="button"
              className={i === 0 ? `${styles.chip} ${styles.chipActive}` : styles.chip}
              disabled
            >
              {c}
            </button>
          ))}
        </div>

        <div className={styles.list}>
          {lbList.map((m) => {
            const rowClass = [
              styles.row,
              m.isMe ? styles.rowMe : '',
              m.rank <= 3 ? styles.rowTop3 : '',
            ]
              .filter(Boolean)
              .join(' ')
            return (
              <div key={m.rank} className={rowClass}>
                <span className={styles.rank}>{m.rank}</span>
                <span className={styles.avatar}>{m.initials}</span>
                <div className={styles.main}>
                  <div className={styles.nickRow}>
                    <span className={styles.nick}>{m.nick}</span>
                    {m.isMe && <span className={styles.meBadge}>TY</span>}
                  </div>
                  <div className={styles.metaRow}>
                    <span className={styles.metaDot} style={{ background: m.dot }} />
                    <span className={styles.meta}>
                      {m.levelName} · {m.cat}
                    </span>
                  </div>
                </div>
                <span className={styles.points}>{m.points}</span>
              </div>
            )
          })}
        </div>
      </section>
    </div>
  )
}
