/** Member team roster dummy data — swap for API calls later (#3). */
export interface TeamMember {
  id: number
  name: string
  nick: string
  cat: string
  role?: string
  levelName: string
  dot: string
  years: number
  bio: string
}

export const memberCats = ['Vše', 'Juniorky', 'Kadetky', 'Seniorky'] as const

const dot = { Zlatá: '#f4c84a', Stříbrná: '#cdd3cf', Bronzová: '#d8a25e' } as const

export const membersGrid: TeamMember[] = [
  {
    id: 1,
    name: 'Adéla Marková',
    nick: 'Adél',
    cat: 'Seniorky',
    role: 'Kapitánka',
    levelName: 'Zlatá',
    dot: dot['Zlatá'],
    years: 5,
    bio: 'Kapitánka týmu a tahoun seniorské formace. Miluje pom-pom choreografie a soutěžní atmosféru.',
  },
  {
    id: 2,
    name: 'Sofia Horváthová',
    nick: 'Sofi',
    cat: 'Seniorky',
    levelName: 'Zlatá',
    dot: dot['Zlatá'],
    years: 6,
    bio: 'V týmu od malička. Specialistka na práci s náčiním a synchron.',
  },
  {
    id: 3,
    name: 'Nina Balážová',
    nick: 'Ninka',
    cat: 'Kadetky',
    levelName: 'Zlatá',
    dot: dot['Zlatá'],
    years: 4,
    bio: 'Mažoretka měsíce. Pomáhá mladším členkám a nikdy nevynechá trénink.',
  },
  {
    id: 4,
    name: 'Ema Nagyová',
    nick: 'Emka',
    cat: 'Kadetky',
    levelName: 'Stříbrná',
    dot: dot['Stříbrná'],
    years: 3,
    bio: 'Energická tanečnice s citem pro rytmus a detail.',
  },
  {
    id: 5,
    name: 'Natálie Kovářová',
    nick: 'Naty',
    cat: 'Juniorky',
    levelName: 'Bronzová',
    dot: dot['Bronzová'],
    years: 2,
    bio: 'Nejmladší naděje juniorek. Rychle se učí nové prvky.',
  },
  {
    id: 6,
    name: 'Klára Veselá',
    nick: 'Klárka',
    cat: 'Juniorky',
    levelName: 'Bronzová',
    dot: dot['Bronzová'],
    years: 1,
    bio: 'Nováček s velkým zápalem a stálým úsměvem.',
  },
  {
    id: 7,
    name: 'Tereza Dvořáková',
    nick: 'Terka',
    cat: 'Kadetky',
    levelName: 'Stříbrná',
    dot: dot['Stříbrná'],
    years: 3,
    bio: 'Pečlivá a spolehlivá, opora kadetské formace.',
  },
  {
    id: 8,
    name: 'Lucie Horáková',
    nick: 'Lucka',
    cat: 'Seniorky',
    levelName: 'Zlatá',
    dot: dot['Zlatá'],
    years: 4,
    bio: 'Choreografická pravá ruka trenérek, vždy s nápadem.',
  },
]
