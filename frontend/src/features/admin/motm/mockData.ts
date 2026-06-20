/** Admin "majorette of the month & goals" dummy data — swap for an API call later (#3). */
export interface CurrentMajorette {
  initials: string
  name: string
  sub: string
}

export interface TeamGoal {
  title: string
  currentFmt: string
  targetFmt: string
  pct: number
}

export const currentMajorette: CurrentMajorette = {
  initials: 'NB',
  name: 'Nina Balážová',
  sub: 'Aktuálně zvolená · Červen 2026',
}

export const memberOptions: string[] = [
  'Natálie Kovářová „Naty"',
  'Sofia Horváthová „Sofi"',
  'Ema Nagyová „Emka"',
]

export const teamGoal: TeamGoal = {
  title: 'Společný výlet do aquaparku',
  currentFmt: '7 240',
  targetFmt: '10 000',
  pct: 72,
}
