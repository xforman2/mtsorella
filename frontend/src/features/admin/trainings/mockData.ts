/** Admin trainings dummy data — swap for an API call later (#3). */
export interface Training {
  id: number
  day: string
  date: string
  time: string
  cat: string
  place: string
}

export const trainingsList: Training[] = [
  {
    id: 1,
    day: 'Po',
    date: '23. 6.',
    time: '16:00–17:30',
    cat: 'Juniorky',
    place: 'Tělocvična ZŠ Komenského',
  },
  {
    id: 2,
    day: 'St',
    date: '25. 6.',
    time: '17:00–19:00',
    cat: 'Kadetky',
    place: 'Tělocvična ZŠ Komenského',
  },
  {
    id: 3,
    day: 'Pá',
    date: '27. 6.',
    time: '17:00–19:30',
    cat: 'Seniorky',
    place: 'Sportovní hala',
  },
  {
    id: 4,
    day: 'Po',
    date: '30. 6.',
    time: '16:00–17:30',
    cat: 'Juniorky',
    place: 'Tělocvična ZŠ Komenského',
  },
]
