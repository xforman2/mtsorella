import m from '../shared/member.module.css'
import styles from './BoardPage.module.css'
import { boardFilters, boardList } from './mockData'

/** Member board (Nástěnka). Filters + reactions inert until the backend (#3). */
export function BoardPage() {
  return (
    <div className={m.pageNarrow} style={{ maxWidth: 820 }}>
      <div className={m.eyebrow}>Oznamy</div>
      <h1 className={m.h1}>Nástěnka</h1>

      <div className={styles.chips}>
        {boardFilters.map((f, i) => (
          <button
            key={f}
            type="button"
            className={i === 0 ? `${styles.chip} ${styles.chipActive}` : styles.chip}
            disabled
          >
            {f}
          </button>
        ))}
      </div>

      <div className={styles.list}>
        {boardList.map((a) => (
          <div
            key={a.id}
            className={a.pinned ? `${styles.card} ${styles.cardPinned}` : styles.card}
          >
            <div className={styles.tagRow}>
              {a.pinned && <span className={styles.pin}>Připnuté</span>}
              <span className={styles.tag} style={{ color: a.tagColor, background: a.tagBg }}>
                {a.tag}
              </span>
              <span className={styles.date}>{a.date}</span>
            </div>
            <h3 className={styles.title}>{a.title}</h3>
            <p className={styles.body}>{a.body}</p>
            <div className={styles.reactions}>
              <button type="button" className={styles.reaction} disabled>
                👍 {a.likeN}
              </button>
              <button type="button" className={styles.reaction} disabled>
                ❤️ {a.heartN}
              </button>
            </div>
          </div>
        ))}
      </div>
    </div>
  )
}
