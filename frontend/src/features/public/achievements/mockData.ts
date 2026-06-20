export type Medal = 'gold' | 'silver' | 'bronze'

export interface AchievementStat {
  num: string
  label: string
}

export interface TimelineEntry {
  id: number
  year: string
  type: string
  comp: string
  place: string
  desc: string
  medal: Medal
}

/** Filter types; "Vše" (all) is the default. Dummy data until the backend (#3). */
export const achievementFilters = ['Vše', 'Národní', 'Mezinárodní', 'Regionální'] as const

export const achievementStats: AchievementStat[] = [
  { num: '24', label: 'medailí celkem' },
  { num: '12', label: 'zlatých' },
  { num: '38', label: 'soutěží' },
  { num: '12', label: 'let na scéně' },
]

export const achievementTimeline: TimelineEntry[] = [
  {
    id: 1,
    year: '2025',
    type: 'Národní',
    comp: 'Mistrovství ČR v mažoretkovém sportu',
    place: '1. místo — zlato',
    desc: 'Zlato v kategorii pom-pom seniorky s rekordním bodovým hodnocením.',
    medal: 'gold',
  },
  {
    id: 2,
    year: '2025',
    type: 'Mezinárodní',
    comp: 'European Cup Praha',
    place: '2. místo — stříbro',
    desc: 'Stříbrná medaile v silné mezinárodní konkurenci.',
    medal: 'silver',
  },
  {
    id: 3,
    year: '2024',
    type: 'Národní',
    comp: 'Mistrovství ČR',
    place: '1. místo — zlato',
    desc: 'Obhajoba titulu mistryň republiky.',
    medal: 'gold',
  },
  {
    id: 4,
    year: '2024',
    type: 'Regionální',
    comp: 'Krajský pohár',
    place: '3. místo — bronz',
    desc: 'Bronz pro kadetky v premiérové sezóně.',
    medal: 'bronze',
  },
]
