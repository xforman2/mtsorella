export type EventKind = 'show' | 'competition' | 'camp'

export interface FullEvent {
  id: number
  day: string
  month: string
  kind: EventKind
  title: string
  where: string
}

export const eventKindLabel: Record<EventKind, string> = {
  show: 'Vystoupení',
  competition: 'Soutěž',
  camp: 'Soustředění',
}

/** Dummy data until the backend (#3). */
export const eventsFull: FullEvent[] = [
  {
    id: 1,
    day: '21',
    month: 'Čvn',
    kind: 'show',
    title: 'Den města — slavnostní průvod',
    where: 'Hlavní náměstí, 14:00',
  },
  {
    id: 2,
    day: '05',
    month: 'Čvc',
    kind: 'camp',
    title: 'Letní soustředění týmu',
    where: 'Penzion Limba, Beskydy',
  },
  {
    id: 3,
    day: '14',
    month: 'Zář',
    kind: 'competition',
    title: 'Pohár ČR — podzimní kolo',
    where: 'Sportovní hala, 9:00',
  },
  {
    id: 4,
    day: '12',
    month: 'Říj',
    kind: 'show',
    title: 'Galavečer mažoretek',
    where: 'Městské divadlo, 18:00',
  },
]
