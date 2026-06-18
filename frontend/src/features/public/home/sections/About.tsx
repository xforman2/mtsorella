import { Link } from 'react-router-dom'
import styles from '../home.module.css'
import type { AboutSection } from '../types'

type AboutProps = { about: AboutSection }

export function About({ about }: AboutProps) {
  return (
    <section className={styles.about}>
      <div>
        <p className={styles.kicker}>Kdo jsme</p>
        <h2 className={styles.heading}>{about.heading}</h2>
        {about.paragraphs.map((paragraph) => (
          <p key={paragraph.slice(0, 24)} className={styles.lead}>
            {paragraph}
          </p>
        ))}
        <Link to="/achievements" className={styles.inlineLink}>
          Naše úspěchy →
        </Link>
      </div>
      <div className={styles.aboutPhoto} aria-hidden="true" />
    </section>
  )
}
