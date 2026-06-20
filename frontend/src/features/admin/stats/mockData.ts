/** Admin statistics dummy data — swap for an API call later (#3). */
export interface Kpi {
  label: string
  val: string
}

export interface CategoryBar {
  label: string
  nTxt: string
  pct: number
}

export const adminKpis: Kpi[] = [
  { label: 'Body týmu (sezóna)', val: '8 560' },
  { label: 'Prům. docházka', val: '91 %' },
  { label: 'Splněné výzvy', val: '42' },
  { label: 'Medaile 2025', val: '6' },
]

export const adminBars: CategoryBar[] = [
  { label: 'Juniorky', nTxt: '8', pct: 67 },
  { label: 'Kadetky', nTxt: '12', pct: 100 },
  { label: 'Seniorky', nTxt: '9', pct: 75 },
]
