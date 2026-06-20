export interface Coach {
  id: number
  name: string
  role: string
  years: number
  bio: string
}

/** Dummy data until the backend (#3). Member profiles stay private; only staff are public. */
export const coaches: Coach[] = [
  {
    id: 1,
    name: 'Lucie Procházková',
    role: 'Hlavní trenérka',
    years: 12,
    bio: 'Zakladatelka týmu a držitelka trenérské licence. Vede seniorské formace a celkovou koncepci tréninku.',
  },
  {
    id: 2,
    name: 'Tereza Marková',
    role: 'Asistentka trenérky',
    years: 6,
    bio: 'Bývalá reprezentantka. Stará se o kadetky a techniku práce s náčiním.',
  },
  {
    id: 3,
    name: 'Karolína Dvořáková',
    role: 'Choreografka',
    years: 4,
    bio: 'Tvoří choreografie pro soutěžní i vystoupení a doprovází tým na akcích.',
  },
]
