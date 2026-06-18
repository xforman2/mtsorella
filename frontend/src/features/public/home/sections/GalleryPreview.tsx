import { Link } from 'react-router-dom'
import styles from '../home.module.css'
import type { GalleryItem } from '../types'

type Props = { items: GalleryItem[] }

export function GalleryPreview({ items }: Props) {
  return (
    <section className={styles.section}>
      <div className={styles.sectionHeader}>
        <div>
          <p className={styles.kicker}>Galerie</p>
          <h2 className={styles.heading}>Z naší galerie</h2>
        </div>
        <Link to="/gallery" className={styles.link}>
          Celá galerie →
        </Link>
      </div>
      <div className={styles.gallery}>
        {items.map((item) => (
          <div key={item.id} className={styles.tile} data-span={item.span}>
            <span className={styles.tileLabel}>
              {item.category} · {item.year}
            </span>
          </div>
        ))}
      </div>
    </section>
  )
}
