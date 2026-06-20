import { useState } from 'react'
import ui from '../shared/ui.module.css'
import styles from './GalleryPage.module.css'
import { galleryCategories, galleryPhotos } from './mockData'

/** Public gallery. Filtering is client-side over dummy data until the backend (#3). */
export function GalleryPage() {
  const [active, setActive] = useState<string>('Vše')
  const photos = active === 'Vše' ? galleryPhotos : galleryPhotos.filter((p) => p.cat === active)

  return (
    <div>
      <section className={styles.header}>
        <div className={styles.headerInner}>
          <p className={ui.kicker}>Galerie</p>
          <h1 className={ui.heading}>Naše momentky</h1>
          <p className={styles.lead}>
            Fotografie ze soutěží, tréninků a vystoupení. Kliknutím otevřete detail.
          </p>
          <div className={styles.chips}>
            {galleryCategories.map((cat) => (
              <button
                key={cat}
                type="button"
                className={cat === active ? `${ui.chip} ${ui.chipActive}` : ui.chip}
                onClick={() => setActive(cat)}
              >
                {cat}
              </button>
            ))}
          </div>
        </div>
      </section>

      <section className={styles.gridSection}>
        <div className={styles.grid}>
          {photos.map((p) => (
            <div key={p.id} className={styles.tile} data-span={p.span}>
              <span className={styles.tileLabel}>[ {p.label} ]</span>
              <div className={styles.tileMeta}>
                <span className={styles.tileCat}>{p.cat}</span>
                <span className={styles.tileYear}>{p.year}</span>
              </div>
            </div>
          ))}
        </div>
      </section>
    </div>
  )
}
