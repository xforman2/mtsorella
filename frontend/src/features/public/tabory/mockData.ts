/**
 * Tábory (summer camps) dummy data + date-driven unlock.
 * Swap for API calls later (#3). Shared by the public /tabory page and the
 * admin /admin/camps view (FR-P34–P38, FR-A15, FE-16).
 */

export interface UpcomingCamp {
  name: string
  dates: string
  place: string
  age: string
  price: string
  capacity: string
  /** Human-readable registration-open date (e.g. "1. července 2026"). */
  openLabel: string
  /** ISO date the application unlocks on (FE-16). */
  openISO: string
  desc: string
}

export interface PastCamp {
  id: number
  year: string
  name: string
  dates: string
  place: string
  kids: number
  desc: string
}

/** The single upcoming camp (FR-P35). */
export const upcomingCamp: UpcomingCamp = {
  name: 'Letní příměstský tábor 2026',
  dates: '13.–17. července 2026',
  place: 'Sportovní hala Brno, Brno-střed',
  age: '6–14 let',
  price: '3 200 Kč',
  capacity: '24 dětí',
  openLabel: '1. července 2026',
  openISO: '2026-07-01',
  desc: 'Pět dní plných mažoretkového tréninku, her a kamarádství. Děti se naučí základy práce s hůlkou i pom-pony, připraví si krátkou choreografii a na závěr týdne vystoupí pro rodiče. Strava a pitný režim po celý den zajištěny.',
}

/** Past camps shown as a grid (FR-P38). */
export const pastCamps: PastCamp[] = [
  {
    id: 1,
    year: '2025',
    name: 'Letní tábor 2025',
    dates: '14.–18. července 2025',
    place: 'Sportovní hala Brno',
    kids: 28,
    desc: 'Rekordní účast a slavnostní galavystoupení pro rodiče na závěr týdne.',
  },
  {
    id: 2,
    year: '2024',
    name: 'Příměstský tábor 2024',
    dates: '15.–19. července 2024',
    place: 'ZŠ Komenského',
    kids: 22,
    desc: 'Ročník s hostující mezinárodní lektorkou a workshopem twirlingu.',
  },
  {
    id: 3,
    year: '2023',
    name: 'Taneční léto 2023',
    dates: '17.–21. července 2023',
    place: 'ZŠ Komenského',
    kids: 19,
    desc: 'Týden zaměřený na základy choreografie, rytmiku a týmového ducha.',
  },
]

/**
 * Application unlock override — the prototype's `taborPrihlaska` tweak (FE-16).
 * 'auto' compares today with `openISO`; 'open' / 'locked' force the state.
 * Flip to 'open' to preview the application form before the open date.
 */
export type CampOverride = 'auto' | 'open' | 'locked'
export const taborPrihlaska: CampOverride = 'auto'

export interface CampStatus {
  open: boolean
  locked: boolean
  daysToOpen: number
  daysTxt: string
  statusLabel: string
  statusColor: string
  statusBg: string
}

const DAY_MS = 86_400_000

/**
 * Port of the prototype's camp-status logic (`renderVals()`): decides whether the
 * application is open vs locked and how many days remain until it unlocks.
 */
export function computeCampStatus(
  camp: UpcomingCamp,
  override: CampOverride = taborPrihlaska,
  now: Date = new Date(),
): CampStatus {
  const openDate = new Date(camp.openISO + 'T00:00:00')
  const open = override === 'open' ? true : override === 'locked' ? false : now >= openDate
  const daysToOpen = Math.max(0, Math.ceil((openDate.getTime() - now.getTime()) / DAY_MS))
  return {
    open,
    locked: !open,
    daysToOpen,
    daysTxt: String(daysToOpen),
    statusLabel: open ? 'Přihlášky otevřené' : 'Přihlášky uzamčené',
    statusColor: open ? '#047b46' : '#9a6b1f',
    statusBg: open ? 'rgba(4, 123, 70, 0.12)' : 'rgba(176, 128, 40, 0.14)',
  }
}
