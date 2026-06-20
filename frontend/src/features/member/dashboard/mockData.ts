/** Member dashboard dummy data — swap for API calls later (#3). */

export interface DashStat {
  val: string
  label: string
  sub: string
}

export interface DashChallenge {
  id: number
  title: string
  deadline: string
  points: number
}

export interface DashAnnouncement {
  id: number
  tag: string
  tagColor: string
  tagBg: string
  date: string
  title: string
}

export const dashStats: DashStat[] = [
  { val: '1 980', label: 'Moje body', sub: 'Zlatá úroveň' },
  { val: '12 dní', label: 'Streak', sub: 'milník 14 dní' },
  { val: '2.', label: 'Pořadí', sub: 'sezóna' },
  { val: '94 %', label: 'Docházka', sub: 'tento rok' },
]

export const nextTraining = {
  date: 'Středa 25. 6.',
  time: '17:00–19:00',
  place: 'Tělocvična ZŠ Komenského',
  bring: 'pom-poms, voda, sálovky',
}

export const dashChallenges: DashChallenge[] = [
  { id: 1, title: 'Piruety – série', deadline: '30. 6.', points: 30 },
  { id: 2, title: 'Choreo blok B', deadline: '5. 7.', points: 50 },
]

export const teamGoal = {
  title: 'Společný výlet do aquaparku',
  currentFmt: '7 240',
  targetFmt: '10 000',
  pct: 72,
}

export const dashAnnouncements: DashAnnouncement[] = [
  {
    id: 1,
    tag: 'Důležité',
    tagColor: '#047b46',
    tagBg: 'rgba(10,157,90,.16)',
    date: '18. 6.',
    title: 'Letní soustředění – odevzdejte přihlášky do pátku',
  },
  {
    id: 2,
    tag: 'Info',
    tagColor: '#3a6ea5',
    tagBg: 'rgba(58,110,165,.12)',
    date: '15. 6.',
    title: 'Páteční trénink se přesouvá na 18:00',
  },
]
