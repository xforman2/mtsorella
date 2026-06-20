import { Link } from 'react-router-dom'
import styles from '../home.module.css'
import type { EventItem, EventKind } from '../types'

const kindLabel: Record<EventKind, string> = {
  show: 'Vystoupení',
  competition: 'Soutěž',
  camp: 'Soustředění',
}

type Props = { events: EventItem[] }

export function UpcomingEvents({ events }: Props) {
  return (
    <section className={styles.center}>
      <div className={styles.headerRow}>
        <div>
          <p className={styles.kicker}>Kalendář</p>
          <h2 className={styles.heading}>Nadcházející vystoupení</h2>
        </div>
        <Link to="/events" className={styles.link}>
          Všechny akce ›
        </Link>
      </div>
      <div className={styles.events}>
        {events.map((event) => (
          <article key={event.id} className={styles.event}>
            <div className={styles.eventDate}>
              <div className={styles.eventDay}>{event.day}</div>
              <div className={styles.eventMonth}>{event.month}</div>
            </div>
            <div className={styles.eventDivider} aria-hidden="true" />
            <div className={styles.eventBody}>
              <div className={styles.eventTitle}>{event.title}</div>
              <div className={styles.eventLoc}>{event.location}</div>
            </div>
            <span className={styles.eventTag} data-kind={event.kind}>
              {kindLabel[event.kind]}
            </span>
          </article>
        ))}
      </div>
    </section>
  )
}
