import { useState } from 'react'
import m from '../shared/member.module.css'
import styles from './TeamPage.module.css'
import { memberCats, membersGrid, type TeamMember } from './mockData'

/** Member team roster (Tím). Category filter inert; clicking a card opens the detail modal. */
export function TeamPage() {
  const [selected, setSelected] = useState<TeamMember | null>(null)

  return (
    <div className={m.page}>
      <div className={m.eyebrow}>Členská zóna</div>
      <h1 className={m.h1}>Naše členky</h1>
      <p className={m.lead}>Kompletní přehled týmu. Kliknutím na členku zobrazíš její profil.</p>

      <div className={styles.chips}>
        {memberCats.map((c, i) => (
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

      <div className={styles.grid}>
        {membersGrid.map((member) => (
          <button
            key={member.id}
            type="button"
            className={styles.card}
            onClick={() => setSelected(member)}
          >
            <div className={styles.photo}>
              <span className={styles.photoLabel}>[ foto 4:5 ]</span>
              {member.role && <span className={styles.roleBadge}>{member.role}</span>}
            </div>
            <div className={styles.cardBody}>
              <div className={styles.name}>{member.name}</div>
              <div className={styles.sub}>
                „{member.nick}" · {member.cat}
              </div>
              <div className={styles.metaRow}>
                <span className={styles.dot} style={{ background: member.dot }} />
                <span className={styles.level}>{member.levelName}</span>
                <span className={styles.years}>{member.years}. rok</span>
              </div>
            </div>
          </button>
        ))}
      </div>

      {/* member detail modal */}
      {selected && (
        <div className={m.overlay} onClick={() => setSelected(null)}>
          <div className={styles.dialog} onClick={(e) => e.stopPropagation()}>
            <div className={styles.modalPhoto}>
              <span className={styles.modalPhotoLabel}>[ foto členky ]</span>
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
              <div className={styles.modalTop}>
                <div>
                  <div className={styles.modalName}>{selected.name}</div>
                  <div className={styles.modalSub}>
                    „{selected.nick}" · {selected.role ?? 'Členka'}
                  </div>
                </div>
                <span className={styles.levelChip}>
                  <span className={styles.dot} style={{ background: selected.dot }} />
                  {selected.levelName}
                </span>
              </div>
              <p className={styles.modalBio}>{selected.bio}</p>
              <div className={styles.modalStats}>
                <div>
                  <div className={styles.statNum}>{selected.years}</div>
                  <div className={styles.statLabel}>roků v týmu</div>
                </div>
                <div>
                  <div className={styles.statNum}>{selected.cat}</div>
                  <div className={styles.statLabel}>kategorie</div>
                </div>
              </div>
            </div>
          </div>
        </div>
      )}
    </div>
  )
}
