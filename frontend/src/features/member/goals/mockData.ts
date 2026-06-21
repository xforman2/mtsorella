/** Member team-goals dummy data — swap for API calls later (#3). */
export interface CompletedGoal {
  id: number
  title: string
  target: string
}

export const teamGoal = {
  title: 'Společný výlet do aquaparku',
  currentFmt: '7 240',
  targetFmt: '10 000',
  pct: 72,
  remainFmt: '2 760',
}

export const goalHistory: CompletedGoal[] = [
  { id: 1, title: 'Týmová trička', target: '5 000' },
  { id: 2, title: 'Pizza večer', target: '3 000' },
  { id: 3, title: 'Výlet do kina', target: '2 000' },
]
