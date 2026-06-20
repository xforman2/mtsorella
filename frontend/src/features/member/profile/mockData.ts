/** Member profile dummy data — swap for API calls later (#3). */
export interface PointsEntry {
  id: number
  label: string
  when: string
  pts: number
}

export interface MyVideo {
  id: number
  title: string
  score: string
}

export const myBadges: string[] = [
  '100 dní streak',
  'MČR zlato 2025',
  '10 splněných výzev',
  'Perfektní docházka',
  'Týmová opora',
]

export const myHistory: PointsEntry[] = [
  { id: 1, label: 'Výzva: Piruety – série', when: 'před 2 dny', pts: 40 },
  { id: 2, label: 'Účast na tréninku', when: 'před 4 dny', pts: 10 },
  { id: 3, label: 'Soutěž MČR – finále', when: 'před 1 týdnem', pts: 120 },
  { id: 4, label: 'Výzva: Hod a chyt', when: 'před 2 týdny', pts: 35 },
]

export const myVideos: MyVideo[] = [
  { id: 1, title: 'Piruety – série', score: '26 b' },
  { id: 2, title: 'Choreo blok A', score: '28 b' },
]
