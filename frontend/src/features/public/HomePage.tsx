import styles from './HomePage.module.css'

export function HomePage() {
  return (
    <section className={styles.hero}>
      <p className={styles.eyebrow}>Majoretky · Est. 2014</p>
      <h1 className={styles.title}>Elegancia v každom pohybe</h1>
      <p className={styles.lead}>
        Frontend pre súťažný majoretkový tím MT&nbsp;Sorella. Toto je východiskový scaffold —
        skutočné stránky pribudnú podľa requirements.md.
      </p>
      <div className={styles.zones}>
        <span className={styles.zone}>Public</span>
        <span className={styles.zone}>Member</span>
        <span className={styles.zone}>Admin</span>
      </div>
    </section>
  )
}
