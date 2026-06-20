import m from '../shared/member.module.css'
import styles from './TrainingsPage.module.css'
import { buildCalendar, calMonthName, calWeekdays, trainingsList } from './mockData'

const calendar = buildCalendar()

/** Member trainings (Tréninky). RSVP + export inert until the backend (#3). */
export function TrainingsPage() {
  return (
    <div className={m.pageNarrow}>
      <div className={m.eyebrow}>Rozpis</div>
      <h1 className={m.h1}>Tréninky</h1>
      <p className={m.lead}>Potvrď účast na nadcházejících trénincích. Za účast získáš +2 body.</p>

      {/* calendar */}
      <div className={styles.calCard}>
        <div className={styles.calHead}>
          <div className={styles.calMonth}>{calMonthName}</div>
          <div className={styles.legend}>
            <span className={styles.legendItem}>
              <span className={styles.dotTraining} />
              Trénink
            </span>
            <span className={styles.legendItem}>
              <span className={styles.dotEvent} />
              Vystoupení
            </span>
            <button type="button" className={styles.exportBtn} disabled>
              Export do kalendáře (.ics)
            </button>
          </div>
        </div>
        <div className={styles.grid}>
          {calWeekdays.map((w) => (
            <div key={w} className={styles.weekday}>
              {w}
            </div>
          ))}
          {calendar.map((c, i) =>
            c.day === null ? (
              <div key={`e${i}`} />
            ) : (
              <div
                key={c.day}
                className={c.highlight ? `${styles.cell} ${styles.cellHighlight}` : styles.cell}
              >
                <span className={styles.cellDay}>{c.day}</span>
                <div className={styles.cellDots}>
                  {c.hasTraining && <span className={styles.dotTraining} />}
                  {c.hasEvent && <span className={styles.dotEvent} />}
                </div>
              </div>
            ),
          )}
        </div>
      </div>

      {/* training list */}
      <div className={styles.list}>
        {trainingsList.map((t) => (
          <div key={t.id} className={styles.training}>
            <div className={styles.date}>
              <div className={styles.dateDay}>{t.day}</div>
              <div className={styles.dateDate}>{t.date}</div>
            </div>
            <div className={styles.main}>
              <div className={styles.titleRow}>
                <span className={styles.time}>{t.time}</span>
                <span className={styles.catChip}>{t.cat}</span>
              </div>
              <div className={styles.place}>{t.place}</div>
              <div className={styles.bring}>Přines: {t.bring}</div>
            </div>
            <div className={styles.actions}>
              <button type="button" className={styles.rsvpYes} disabled>
                Přijdu
              </button>
              <button type="button" className={styles.rsvpNo} disabled>
                Nepřijdu
              </button>
              <button type="button" className={styles.icsBtn} disabled>
                + Kalendář
              </button>
            </div>
          </div>
        ))}
      </div>
    </div>
  )
}
