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
    <section className={styles.section}>
      <div className={styles.sectionHeader}>
        <div>
          <p className={styles.kicker}>Kalendář</p>
          <h2 className={styles.heading}>Nadcházející vystoupení</h2>
        </div>
        <Link to="/events" className={styles.link}>
          Všechny akce →
        </Link>
      </div>
      <div className={styles.events}>
        {events.map((event) => (
          <article key={event.id} className={styles.event}>
            <div className={styles.eventDate}>
              <span className={styles.eventDay}>{event.day}</span>
              <span className={styles.eventMonth}>{event.month}</span>
            </div>
            <div className={styles.eventBody}>
              <span className={styles.eventTag}>{kindLabel[event.kind]}</span>
              <h3 className={styles.eventTitle}>{event.title}</h3>
              <p className={styles.eventLoc}>{event.location}</p>
            </div>
          </article>
        ))}
      </div>
    </section>
  )
}
