/** Member trainings dummy data — swap for API calls later (#3). */
export interface Training {
  id: number
  day: string
  date: string
  time: string
  cat: string
  place: string
  bring: string
}

export interface CalendarCell {
  day: number | null
  hasTraining: boolean
  hasEvent: boolean
  highlight: boolean
}

export const calMonthName = 'Červen 2026'
export const calWeekdays = ['Po', 'Út', 'St', 'Čt', 'Pá', 'So', 'Ne']

const trainingDays = [3, 5, 9, 11, 16, 18, 23, 25, 27, 30]
const eventDays = [14, 21]
const highlightDay = 25

/** Build the June 2026 grid (Monday-first). */
export function buildCalendar(): CalendarCell[] {
  const first = new Date(2026, 5, 1)
  const start = (first.getDay() + 6) % 7 // 0 = Monday
  const days = new Date(2026, 6, 0).getDate()
  const cells: CalendarCell[] = []
  for (let i = 0; i < start; i++)
    cells.push({ day: null, hasTraining: false, hasEvent: false, highlight: false })
  for (let d = 1; d <= days; d++) {
    cells.push({
      day: d,
      hasTraining: trainingDays.includes(d),
      hasEvent: eventDays.includes(d),
      highlight: d === highlightDay,
    })
  }
  while (cells.length % 7)
    cells.push({ day: null, hasTraining: false, hasEvent: false, highlight: false })
  return cells
}

export const trainingsList: Training[] = [
  {
    id: 1,
    day: 'St',
    date: '25. 6.',
    time: '17:00–19:00',
    cat: 'Seniorky',
    place: 'Tělocvična ZŠ Komenského',
    bring: 'pom-poms, voda',
  },
  {
    id: 2,
    day: 'Pá',
    date: '27. 6.',
    time: '17:00–19:30',
    cat: 'Seniorky',
    place: 'Sportovní hala',
    bring: 'sálovky, dres',
  },
  {
    id: 3,
    day: 'Po',
    date: '30. 6.',
    time: '16:00–17:30',
    cat: 'Seniorky',
    place: 'Tělocvična ZŠ Komenského',
    bring: 'pom-poms',
  },
  {
    id: 4,
    day: 'St',
    date: '2. 7.',
    time: '17:00–19:00',
    cat: 'Seniorky',
    place: 'Tělocvična ZŠ Komenského',
    bring: 'voda, dobrá nálada',
  },
]
