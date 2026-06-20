import { Link } from 'react-router-dom'
import m from '../shared/member.module.css'
import styles from './MemberDashboardPage.module.css'
import { me } from '../me'
import { dashAnnouncements, dashChallenges, dashStats, nextTraining, teamGoal } from './mockData'

/** Member dashboard (Přehled). Dummy data; RSVP buttons inert until the backend (#3). */
export function MemberDashboardPage() {
  return (
    <div className={m.page}>
      <div className={m.headerRow}>
        <div>
          <div className={m.eyebrow}>Členská zóna</div>
          <h1 className={m.h1}>Ahoj, {me.nick}</h1>
          <p className={m.lead}>Tu je tvůj dnešní přehled — co tě čeká a co je třeba vyřídit.</p>
        </div>
        <Link to="/app/profile" className={styles.profileBtn}>
          <span className={styles.profileBtnAvatar}>{me.initials}</span>
          Můj profil ›
        </Link>
      </div>

      <div className={styles.statStrip}>
        {dashStats.map((s) => (
          <div key={s.label} className={styles.statCard}>
            <div style={{ minWidth: 0 }}>
              <div className={styles.statVal}>{s.val}</div>
              <div className={styles.statLabel}>
                {s.label} · {s.sub}
              </div>
            </div>
          </div>
        ))}
      </div>

      <div className={styles.cardsGrid}>
        {/* Next training */}
        <div className={m.card}>
          <div className={m.cardHead}>
            <span className={m.cardEyebrow}>Nejbližší trénink</span>
          </div>
          <div className={styles.ntRow}>
            <div className={styles.ntMain}>
              <div className={styles.ntDate}>
                {nextTraining.date} · {nextTraining.time}
              </div>
              <div className={styles.ntPlace}>{nextTraining.place}</div>
              <div className={styles.ntBring}>Přines: {nextTraining.bring}</div>
            </div>
            <div className={styles.rsvpRow}>
              <button type="button" className={styles.rsvpYes} disabled>
                Přijdu
              </button>
              <button type="button" className={styles.rsvpNo} disabled>
                Nepřijdu
              </button>
            </div>
          </div>
        </div>

        {/* Active challenges */}
        <div className={m.card}>
          <div className={m.cardHead}>
            <span className={m.cardEyebrow}>Aktivní výzvy</span>
            <Link to="/app/challenges" className={m.cardLink}>
              Všechny ›
            </Link>
          </div>
          <div className={styles.challengeList}>
            {dashChallenges.map((c) => (
              <Link key={c.id} to="/app/challenges" className={styles.challenge}>
                <div className={styles.challengeMain}>
                  <div className={styles.challengeTitle}>{c.title}</div>
                  <div className={styles.challengeDeadline}>Termín: {c.deadline}</div>
                </div>
                <span className={m.badgePts}>+{c.points} b</span>
              </Link>
            ))}
          </div>
        </div>

        {/* Team goal */}
        <div className={m.card}>
          <div className={m.cardHead}>
            <span className={m.cardEyebrow}>Týmový cíl</span>
          </div>
          <div className={styles.goalTitle}>{teamGoal.title}</div>
          <div className={styles.goalRow}>
            <span className={styles.goalCurrent}>{teamGoal.currentFmt}</span>
            <span className={styles.goalTarget}>/ {teamGoal.targetFmt} b</span>
          </div>
          <div className={m.progress}>
            <div className={m.progressFill} style={{ width: `${teamGoal.pct}%` }} />
            <div className={m.progressMarker} style={{ left: `calc(${teamGoal.pct}% - 3px)` }} />
          </div>
        </div>

        {/* Announcements */}
        <div className={m.card}>
          <div className={m.cardHead}>
            <span className={m.cardEyebrow}>Nástěnka</span>
            <Link to="/app/board" className={m.cardLink}>
              Otevřít ›
            </Link>
          </div>
          <div className={styles.annList}>
            {dashAnnouncements.map((a) => (
              <div key={a.id} className={styles.ann}>
                <div className={styles.annTagRow}>
                  <span
                    className={styles.annTag}
                    style={{ color: a.tagColor, background: a.tagBg }}
                  >
                    {a.tag}
                  </span>
                  <span className={styles.annDate}>{a.date}</span>
                </div>
                <div className={styles.annTitle}>{a.title}</div>
              </div>
            ))}
          </div>
        </div>
      </div>
    </div>
  )
}
