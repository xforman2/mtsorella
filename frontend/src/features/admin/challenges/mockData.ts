/** Admin challenges dummy data — swap for an API call later (#3). */
export interface Challenge {
  id: number
  title: string
  deadline: string
  points: number
}

export const challengesActive: Challenge[] = [
  { id: 1, title: 'Piruety – série', deadline: '30. 6.', points: 30 },
  { id: 2, title: 'Choreo blok B', deadline: '5. 7.', points: 50 },
]
