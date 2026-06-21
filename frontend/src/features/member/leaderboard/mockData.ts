/** Member leaderboard dummy data — swap for API calls later (#3). */
export interface PodiumEntry {
  rank: number
  medal: string
  initials: string
  nick: string
  cat: string
  points: string
  ring: string
  h: number
}

export interface LeaderRow {
  rank: number
  initials: string
  nick: string
  levelName: string
  cat: string
  points: string
  dot: string
  isMe: boolean
}

export const lbCategories = ['Vše', 'Juniorky', 'Kadetky', 'Seniorky'] as const

const dot = { Zlatá: '#f4c84a', Stříbrná: '#cdd3cf', Bronzová: '#d8a25e' } as const

/** Visual order: 2nd, 1st, 3rd (1st in the middle, tallest). */
export const podium: PodiumEntry[] = [
  {
    rank: 2,
    medal: '2.',
    initials: 'SH',
    nick: 'Sofi',
    cat: 'Seniorky',
    points: '1 980',
    ring: '#cdd3cf',
    h: 100,
  },
  {
    rank: 1,
    medal: '1.',
    initials: 'AM',
    nick: 'Adél',
    cat: 'Seniorky',
    points: '2 140',
    ring: '#f4c84a',
    h: 132,
  },
  {
    rank: 3,
    medal: '3.',
    initials: 'NB',
    nick: 'Ninka',
    cat: 'Kadetky',
    points: '1 240',
    ring: '#d8a25e',
    h: 84,
  },
]

export const lbList: LeaderRow[] = [
  {
    rank: 1,
    initials: 'AM',
    nick: 'Adél',
    levelName: 'Zlatá',
    cat: 'Seniorky',
    points: '2 140',
    dot: dot['Zlatá'],
    isMe: false,
  },
  {
    rank: 2,
    initials: 'SH',
    nick: 'Sofi',
    levelName: 'Zlatá',
    cat: 'Seniorky',
    points: '1 980',
    dot: dot['Zlatá'],
    isMe: true,
  },
  {
    rank: 3,
    initials: 'NB',
    nick: 'Ninka',
    levelName: 'Zlatá',
    cat: 'Kadetky',
    points: '1 240',
    dot: dot['Zlatá'],
    isMe: false,
  },
  {
    rank: 4,
    initials: 'EN',
    nick: 'Emka',
    levelName: 'Stříbrná',
    cat: 'Kadetky',
    points: '860',
    dot: dot['Stříbrná'],
    isMe: false,
  },
  {
    rank: 5,
    initials: 'NK',
    nick: 'Naty',
    levelName: 'Bronzová',
    cat: 'Juniorky',
    points: '540',
    dot: dot['Bronzová'],
    isMe: false,
  },
  {
    rank: 6,
    initials: 'KV',
    nick: 'Klárka',
    levelName: 'Bronzová',
    cat: 'Juniorky',
    points: '320',
    dot: dot['Bronzová'],
    isMe: false,
  },
]
