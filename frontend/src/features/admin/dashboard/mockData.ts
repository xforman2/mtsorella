/** Admin dashboard dummy data — swap each export for an API call later (#3). */

export interface StatCard {
  num: string
  label: string
}

export type ApplicationStatus = 'new' | 'contacted'

export interface Application {
  id: number
  child: string
  age: number
  parent: string
  cat: string
  date: string
  status: ApplicationStatus
}

export interface PendingVideo {
  id: number
  nick: string
  challenge: string
  when: string
}

export const adminStatCards: StatCard[] = [
  { num: '24', label: 'aktivních členek' },
  { num: '3', label: 'videí k hodnocení' },
  { num: '5', label: 'aktivních výzev' },
  { num: '3', label: 'nových přihlášek' },
]

export const applications: Application[] = [
  {
    id: 1,
    child: 'Eliška Nováková',
    age: 7,
    parent: 'Jana Nováková',
    cat: 'Juniorky',
    date: '18. 6.',
    status: 'new',
  },
  {
    id: 2,
    child: 'Tereza Dvořáková',
    age: 12,
    parent: 'Petr Dvořák',
    cat: 'Kadetky',
    date: '16. 6.',
    status: 'new',
  },
  {
    id: 3,
    child: 'Karolína Svobodová',
    age: 15,
    parent: 'Lucie Svobodová',
    cat: 'Seniorky',
    date: '14. 6.',
    status: 'contacted',
  },
]

export const pendingVideos: PendingVideo[] = [
  { id: 1, nick: 'Ninka', challenge: 'Piruety – série', when: 'před 2 h' },
  { id: 2, nick: 'Emka', challenge: 'Hod a chyt', when: 'včera' },
  { id: 3, nick: 'Sofi', challenge: 'Choreo blok B', when: 'před 3 dny' },
]

const statusStyles: Record<
  ApplicationStatus,
  { label: string; color: string; background: string }
> = {
  new: { label: 'Nová', color: '#047b46', background: 'rgba(10,157,90,.16)' },
  contacted: { label: 'Kontaktovaná', color: '#3a6ea5', background: 'rgba(58,110,165,.12)' },
}

export function applicationStatus(status: ApplicationStatus) {
  return statusStyles[status]
}
