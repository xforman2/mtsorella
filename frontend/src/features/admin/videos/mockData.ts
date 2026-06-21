/** Admin video-grading dummy data — swap for an API call later (#3). */
export interface PendingVideo {
  id: number
  nick: string
  initials: string
  challenge: string
  cat: string
  when: string
  sel: number
}

export interface GradedVideo {
  id: number
  nick: string
  challenge: string
  quality: number
  total: number
}

export const pendingVideos: PendingVideo[] = [
  {
    id: 1,
    nick: 'Ninka',
    initials: 'NI',
    challenge: 'Piruety – série',
    cat: 'Kadetky',
    when: 'před 2 h',
    sel: 10,
  },
  {
    id: 2,
    nick: 'Emka',
    initials: 'EM',
    challenge: 'Hod a chyt',
    cat: 'Kadetky',
    when: 'včera',
    sel: 10,
  },
  {
    id: 3,
    nick: 'Sofi',
    initials: 'SO',
    challenge: 'Choreo blok B',
    cat: 'Seniorky',
    when: 'před 3 dny',
    sel: 10,
  },
]

export const gradedVideos: GradedVideo[] = [
  { id: 1, nick: 'Naty', challenge: 'Piruety – série', quality: 16, total: 26 },
  { id: 2, nick: 'Adél', challenge: 'Choreo blok A', quality: 18, total: 28 },
]
