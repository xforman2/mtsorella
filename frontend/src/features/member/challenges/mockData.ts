/** Member challenges dummy data — swap for API calls later (#3). */
export interface ActiveChallenge {
  id: number
  cat: string
  sLabel: string
  sColor: string
  sBg: string
  title: string
  desc: string
  deadline: string
  points: number
}

export interface DoneChallenge {
  id: number
  title: string
  cat: string
  scoreTxt: string
  sLabel: string
  sColor: string
  sBg: string
}

const active = { sLabel: 'Aktivní', sColor: '#047b46', sBg: 'rgba(10,157,90,.16)' }

export const challengesActive: ActiveChallenge[] = [
  {
    id: 1,
    cat: 'Kadetky',
    ...active,
    title: 'Piruety – série',
    desc: 'Zvládni sérii pěti čistých piruet za sebou. Natoč video z profilu a nahraj ho.',
    deadline: '30. 6.',
    points: 30,
  },
  {
    id: 2,
    cat: 'Seniorky',
    ...active,
    title: 'Choreo blok B',
    desc: 'Nacvič choreografický blok B podle instruktážního videa a předveď ho v celku.',
    deadline: '5. 7.',
    points: 50,
  },
]

export const challengesDone: DoneChallenge[] = [
  {
    id: 3,
    title: 'Hod a chyt',
    cat: 'Kadetky',
    scoreTxt: '+26 b',
    sLabel: 'Splněno',
    sColor: '#047b46',
    sBg: 'rgba(4,123,70,.1)',
  },
  {
    id: 4,
    title: 'Choreo blok A',
    cat: 'Seniorky',
    scoreTxt: '+28 b',
    sLabel: 'Splněno',
    sColor: '#047b46',
    sBg: 'rgba(4,123,70,.1)',
  },
  {
    id: 5,
    title: 'Strečink výzva',
    cat: 'Juniorky',
    scoreTxt: '',
    sLabel: 'Promeškáno',
    sColor: '#9aa89f',
    sBg: 'rgba(154,168,159,.16)',
  },
]
