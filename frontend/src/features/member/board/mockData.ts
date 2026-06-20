/** Member board (announcements) dummy data — swap for API calls later (#3). */
export interface Announcement {
  id: number
  pinned: boolean
  tag: string
  tagColor: string
  tagBg: string
  date: string
  title: string
  body: string
  likeN: number
  heartN: number
}

export const boardFilters = ['Vše', 'Důležité'] as const

export const boardList: Announcement[] = [
  {
    id: 1,
    pinned: true,
    tag: 'Důležité',
    tagColor: '#047b46',
    tagBg: 'rgba(10,157,90,.16)',
    date: '18. 6.',
    title: 'Letní soustředění – odevzdejte přihlášky',
    body: 'Přihlášky na letní soustředění odevzdejte trenérkám nejpozději do pátku. Počet míst je omezený.',
    likeN: 14,
    heartN: 8,
  },
  {
    id: 2,
    pinned: false,
    tag: 'Trénink',
    tagColor: '#3a6ea5',
    tagBg: 'rgba(58,110,165,.12)',
    date: '15. 6.',
    title: 'Páteční trénink se přesouvá na 18:00',
    body: 'Kvůli obsazené hale začínáme tento pátek až v 18:00. Sraz jako obvykle 15 minut předem.',
    likeN: 9,
    heartN: 3,
  },
  {
    id: 3,
    pinned: false,
    tag: 'Info',
    tagColor: '#5a6b62',
    tagBg: 'rgba(90,107,98,.12)',
    date: '10. 6.',
    title: 'Nové dresy – objednávky',
    body: 'Otevíráme objednávky nových dresů. Velikosti si vyzkoušejte na příštím tréninku u Adély.',
    likeN: 21,
    heartN: 12,
  },
]
