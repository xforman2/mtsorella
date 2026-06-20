import { useState } from 'react'
import admin from '../shared/admin.module.css'
import styles from './MembersPage.module.css'
import { initials, members } from './mockData'

/**
 * Admin members view + create-account modal. Dummy data until the backend (#3);
 * the modal opens to show its layout but the form is inert (submit disabled).
 */
export function MembersPage() {
  const [createOpen, setCreateOpen] = useState(false)

  return (
    <div className={admin.page}>
      <div className={admin.headerRow}>
        <p className={admin.lead}>
          Účty členek vytváří výhradně admin. Přihlašování probíhá přes e-mail rodiče.
        </p>
        <button type="button" className={admin.btnPrimary} onClick={() => setCreateOpen(true)}>
          + Vytvořit účet
        </button>
      </div>

      <div className={admin.table}>
        <div className={admin.tableHead}>
          <span className={styles.cMember}>Členka</span>
          <span className={styles.cCat}>Kategorie</span>
          <span className={styles.cYears}>Roky</span>
          <span className={styles.cPoints}>Body</span>
          <span className={styles.cAction} />
        </div>
        {members.map((m) => (
          <div key={m.id} className={admin.tableRow}>
            <div className={`${styles.cMember} ${styles.member}`}>
              <span className={admin.avatar}>{initials(m.name)}</span>
              <div style={{ minWidth: 0 }}>
                <div className={styles.memberName}>{m.name}</div>
                <div className={styles.memberSub}>
                  „{m.nick}" · {m.level}
                </div>
              </div>
            </div>
            <span className={`${styles.cCat} ${styles.cat}`}>{m.cat}</span>
            <span className={`${styles.cYears} ${styles.years}`}>{m.years}</span>
            <span className={`${styles.cPoints} ${styles.points}`}>{m.points}</span>
            <span className={`${styles.cAction} ${styles.action}`}>Upravit</span>
          </div>
        ))}
      </div>

      {createOpen && (
        <div className={styles.overlay} onClick={() => setCreateOpen(false)}>
          <div className={styles.dialog} onClick={(e) => e.stopPropagation()}>
            <div className={styles.dialogHead}>
              <h3 className={styles.dialogTitle}>Vytvořit účet členky</h3>
              <button
                type="button"
                className={styles.close}
                aria-label="Zavřít"
                onClick={() => setCreateOpen(false)}
              >
                ×
              </button>
            </div>
            <div className={styles.dialogBody}>
              <form className={admin.formCol} onSubmit={(e) => e.preventDefault()}>
                <div className={admin.fieldRow}>
                  <input className={admin.field} placeholder="Jméno" />
                  <input className={admin.field} placeholder="Příjmení" />
                </div>
                <input className={admin.field} placeholder="Přezdívka" />
                <select className={admin.field} defaultValue="">
                  <option value="" disabled>
                    Kategorie
                  </option>
                  <option>Juniorky</option>
                  <option>Kadetky</option>
                  <option>Seniorky</option>
                </select>
                <input className={admin.field} placeholder="E-mail rodiče (přihlašovací jméno)" />
                <div className={admin.hint}>
                  Dočasné heslo se vygeneruje automaticky a odešle na e-mail rodiče.
                </div>
                <div className={styles.modalActions}>
                  <button
                    type="button"
                    className={styles.btnCancel}
                    onClick={() => setCreateOpen(false)}
                  >
                    Zrušit
                  </button>
                  <button type="submit" className={`${admin.btnPrimary} ${styles.submit}`} disabled>
                    Vytvořit účet
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
