import ui from '../shared/ui.module.css'
import styles from './EventsPage.module.css'
import { eventKindLabel, eventsFull } from './mockData'

/**
 * Public events/calendar page. The ".ics" export buttons are inert until the
 * backend (#3) exists. Styling is 1:1 with the prototype.
 */
export function EventsPage() {
  return (
    <div>
      <section className={styles.header}>
        <div className={styles.headerInner}>
          <p className={ui.kicker}>Kalendář</p>
          <h1 className={ui.heading}>Vystoupení a akce</h1>
          <button type="button" className={styles.outlineSm} disabled>
            Export všech do kalendáře (.ics)
          </button>
        </div>
      </section>

      <section className={styles.list}>
        {eventsFull.map((e) => (
          <article key={e.id} className={styles.event}>
            <div className={styles.date}>
              <div className={styles.day}>{e.day}</div>
              <div className={styles.month}>{e.month}</div>
            </div>
            <div className={styles.body}>
              <span className={styles.tag} data-kind={e.kind}>
                {eventKindLabel[e.kind]}
              </span>
              <div className={styles.title}>{e.title}</div>
              <div className={styles.where}>{e.where}</div>
            </div>
            <button type="button" className={styles.addBtn} disabled>
              + Do kalendáře
            </button>
          </article>
        ))}
      </section>
    </div>
  )
}
