import { useState } from 'react'
import m from '../shared/member.module.css'
import styles from './ProfilePage.module.css'
import { me } from '../me'
import { myBadges, myHistory, myVideos } from './mockData'

/**
 * Member profile. Dummy data until the backend (#3); the edit modal opens to
 * show its layout but the form is inert (Save disabled).
 */
export function ProfilePage() {
  const [editOpen, setEditOpen] = useState(false)

  return (
    <div className={m.pageNarrow}>
      {/* header card */}
      <div className={styles.header}>
        <span className={styles.avatar}>{me.initials}</span>
        <div className={styles.headerMain}>
          <div className={styles.name}>{me.name}</div>
          <div className={styles.sub}>
            „{me.nick}" · {me.cat}
          </div>
          <div className={styles.chips}>
            <span className={`${styles.chip} ${styles.chipLevel}`}>{me.level} úroveň</span>
            <span className={`${styles.chip} ${styles.chipStreak}`}>{me.streak} dní streak</span>
          </div>
        </div>
        <div className={styles.headerRight}>
          <div className={styles.pointsBig}>{me.pointsFmt}</div>
          <div className={styles.pointsLabel}>bodů celkem</div>
          <button type="button" className={styles.editBtn} onClick={() => setEditOpen(true)}>
            Upravit profil
          </button>
        </div>
      </div>

      {/* level + attendance */}
      <div className={styles.statCards}>
        <div className={styles.statCard}>
          <div className={m.cardEyebrow}>Progres úrovně</div>
          <div className={styles.levelRow}>
            <span>{me.level}</span>
            <span>
              do {me.nextLevel}: {me.toNext} b
            </span>
          </div>
          <div className={m.progress}>
            <div className={m.progressFill} style={{ width: `${me.levelPct}%` }} />
          </div>
        </div>
        <div className={`${styles.statCard} ${styles.statCardCenter}`}>
          <div className={m.cardEyebrow}>Docházka</div>
          <div className={styles.attNum}>{me.attendance}%</div>
          <div className={styles.attSub}>tento rok</div>
        </div>
      </div>

      {/* badges */}
      <h2 className={m.sectionTitle} style={{ marginTop: 36 }}>
        Získané odznaky
      </h2>
      <div className={styles.badgeGrid}>
        {myBadges.map((b) => (
          <div key={b} className={styles.badge}>
            <span className={styles.badgeLabel}>{b}</span>
          </div>
        ))}
      </div>

      {/* history + videos */}
      <div className={styles.twoCol}>
        <div>
          <h2 className={m.sectionTitle}>Historie bodů</h2>
          <div className={styles.col}>
            {myHistory.map((h) => (
              <div key={h.id} className={styles.histRow}>
                <div className={styles.histMain}>
                  <div className={styles.histLabel}>{h.label}</div>
                  <div className={styles.histWhen}>{h.when}</div>
                </div>
                <span className={styles.histPts}>+{h.pts}</span>
              </div>
            ))}
          </div>
        </div>
        <div>
          <h2 className={m.sectionTitle}>Moje videa</h2>
          <div className={styles.col}>
            {myVideos.map((v) => (
              <div key={v.id} className={styles.videoRow}>
                <span className={styles.videoThumb}>▶</span>
                <span className={styles.videoTitle}>{v.title}</span>
                <span className={styles.videoScore}>{v.score}</span>
              </div>
            ))}
          </div>
        </div>
      </div>

      {/* edit-profile modal */}
      {editOpen && (
        <div className={m.overlay} onClick={() => setEditOpen(false)}>
          <div className={m.dialog} onClick={(e) => e.stopPropagation()}>
            <div className={m.dialogHead}>
              <h3 className={m.dialogTitle}>Upravit profil</h3>
              <button
                type="button"
                className={m.close}
                aria-label="Zavřít"
                onClick={() => setEditOpen(false)}
              >
                ×
              </button>
            </div>
            <div className={m.dialogBody}>
              <div className={styles.editPhoto}>
                <span className={styles.editAvatar}>{me.initials}</span>
                <button type="button" className={styles.changePhoto}>
                  Změnit fotku
                </button>
              </div>
              <form className={styles.editForm} onSubmit={(e) => e.preventDefault()}>
                <div className={m.fieldGroup}>
                  <label className={m.label}>Jméno a příjmení</label>
                  <input className={m.field} defaultValue={me.name} />
                </div>
                <div className={m.fieldGroup}>
                  <label className={m.label}>Přezdívka</label>
                  <input className={m.field} defaultValue={me.nick} />
                </div>
                <div className={m.fieldGroup}>
                  <label className={m.label}>E-mail rodiče</label>
                  <input className={m.field} defaultValue="rodic@email.cz" />
                </div>
                <div className={m.modalActions}>
                  <button type="button" className={m.btnGhost} onClick={() => setEditOpen(false)}>
                    Zrušit
                  </button>
                  <button type="submit" className={m.btnSolid} disabled>
                    Uložit změny
                  </button>
                </div>
              </form>
            </div>
          </div>
        </div>
      )}
    </div>
  )
}
