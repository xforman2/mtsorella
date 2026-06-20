import { useState } from 'react'
import m from '../shared/member.module.css'
import styles from './ChallengesPage.module.css'
import { challengesActive, challengesDone, type ActiveChallenge } from './mockData'

/**
 * Member challenges (Výzvy). The detail modal opens to show its layout; the
 * video upload is inert until the backend (#3).
 */
export function ChallengesPage() {
  const [selected, setSelected] = useState<ActiveChallenge | null>(null)

  return (
    <div className={m.pageNarrow} style={{ maxWidth: 1100 }}>
      <div className={m.eyebrow}>Gamifikace</div>
      <h1 className={m.h1}>Tréninkové výzvy</h1>
      <p className={m.lead}>
        Podívej se na instruktážní video, nahraj svoje a získej body. Za splnění +10 b, před
        termínem +5 b.
      </p>

      <h2 className={m.sectionTitle} style={{ marginTop: 34 }}>
        Aktivní výzvy
      </h2>
      <div className={styles.grid}>
        {challengesActive.map((c) => (
          <div key={c.id} className={styles.card}>
            <div className={styles.thumb}>
              <div className={styles.thumbCenter}>
                <span className={styles.play}>▶</span>
              </div>
              <span className={styles.thumbLabel}>[ instruktážní video ]</span>
            </div>
            <div className={styles.body}>
              <div className={styles.chipRow}>
                <span className={styles.statusChip} style={{ color: c.sColor, background: c.sBg }}>
                  {c.sLabel}
                </span>
                <span className={styles.cat}>{c.cat}</span>
              </div>
              <h3 className={styles.title}>{c.title}</h3>
              <p className={styles.desc}>{c.desc}</p>
              <div className={styles.footRow}>
                <div className={styles.deadline}>
                  {c.deadline} · <span className={styles.deadlinePts}>+{c.points} b</span>
                </div>
                <button type="button" className={styles.uploadBtn} onClick={() => setSelected(c)}>
                  Nahrát video
                </button>
              </div>
            </div>
          </div>
        ))}
      </div>

      <h2 className={m.sectionTitle} style={{ marginTop: 44 }}>
        Historie výzev
      </h2>
      <div className={styles.histList}>
        {challengesDone.map((c) => (
          <div key={c.id} className={styles.histRow}>
            <div className={styles.histMain}>
              <div className={styles.histTitle}>{c.title}</div>
              <div className={styles.histCat}>{c.cat}</div>
            </div>
            {c.scoreTxt && <span className={styles.histScore}>{c.scoreTxt}</span>}
            <span className={styles.statusChip} style={{ color: c.sColor, background: c.sBg }}>
              {c.sLabel}
            </span>
          </div>
        ))}
      </div>

      {/* detail modal */}
      {selected && (
        <div className={m.overlay} onClick={() => setSelected(null)}>
          <div className={styles.dialog} onClick={(e) => e.stopPropagation()}>
            <div className={styles.modalThumb}>
              <div className={styles.thumbCenter}>
                <span className={styles.modalPlay}>▶</span>
              </div>
              <span className={styles.thumbLabel}>[ instruktážní video ]</span>
              <button
                type="button"
                className={styles.modalClose}
                aria-label="Zavřít"
                onClick={() => setSelected(null)}
              >
                ×
              </button>
            </div>
            <div className={styles.modalBody}>
              <div className={styles.chipRow}>
                <span
                  className={styles.statusChip}
                  style={{ color: selected.sColor, background: selected.sBg }}
                >
                  {selected.sLabel}
                </span>
                <span className={styles.cat}>
                  Termín: {selected.deadline} · +{selected.points} b
                </span>
              </div>
              <h3 className={styles.modalTitle}>{selected.title}</h3>
              <p className={styles.modalDesc}>{selected.desc}</p>
              <div className={styles.dropzone}>
                <div className={styles.dropTitle}>Nahraj svoje tréninkové video</div>
                <div className={styles.dropSub}>Přetáhni soubor sem nebo klikni na výběr</div>
                <button type="button" className={styles.dropBtn} disabled>
                  Vybrat video
                </button>
              </div>
            </div>
          </div>
        </div>
      )}
    </div>
  )
}
