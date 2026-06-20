/** Admin members dummy data — swap for an API call later (#3). */

export interface Member {
  id: number
  name: string
  nick: string
  cat: string
  years: number
  points: string
  level: string
}

export const members: Member[] = [
  {
    id: 1,
    name: 'Nina Balážová',
    nick: 'Ninka',
    cat: 'Kadetky',
    years: 4,
    points: '1 240',
    level: 'Zlatá',
  },
  {
    id: 2,
    name: 'Sofia Horváthová',
    nick: 'Sofi',
    cat: 'Seniorky',
    years: 6,
    points: '1 980',
    level: 'Zlatá',
  },
  {
    id: 3,
    name: 'Ema Nagyová',
    nick: 'Emka',
    cat: 'Kadetky',
    years: 3,
    points: '860',
    level: 'Stříbrná',
  },
  {
    id: 4,
    name: 'Natálie Kovářová',
    nick: 'Naty',
    cat: 'Juniorky',
    years: 2,
    points: '540',
    level: 'Bronzová',
  },
  {
    id: 5,
    name: 'Adéla Marková',
    nick: 'Adél',
    cat: 'Seniorky',
    years: 5,
    points: '1 620',
    level: 'Zlatá',
  },
  {
    id: 6,
    name: 'Klára Veselá',
    nick: 'Klárka',
    cat: 'Juniorky',
    years: 1,
    points: '320',
    level: 'Bronzová',
  },
]

export function initials(name: string): string {
  return name
    .split(' ')
    .slice(0, 2)
    .map((w) => w[0])
    .join('')
    .toUpperCase()
}
