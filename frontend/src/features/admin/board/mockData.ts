/** Admin board (announcements) dummy data — swap for an API call later (#3). */
export interface Announcement {
  id: number
  title: string
  date: string
  pinned: boolean
}

export const boardList: Announcement[] = [
  { id: 1, title: 'Letní soustředění – přihlášky', date: '18. 6.', pinned: true },
  { id: 2, title: 'Změna času tréninku v pátek', date: '15. 6.', pinned: false },
  { id: 3, title: 'Nové dresy – objednávky', date: '10. 6.', pinned: false },
]
