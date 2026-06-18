/**
 * Home-page data shapes. These mirror what will eventually live in
 * `src/api/types.ts` once the backend (#3) exposes the data — keeping them here
 * for now means the swap is localized to `useHomeData`.
 */

export interface HeroStat {
  value: string
  label: string
}

export interface AboutSection {
  heading: string
  paragraphs: string[]
}

export type Medal = 'gold' | 'silver' | 'bronze'

export interface Achievement {
  year: string
  competition: string
  placement: string
  medal: Medal
  description: string
}

export interface Majorette {
  name: string
  nickname: string
  category: string
  reason: string
}

export interface TeamGoal {
  title: string
  current: number
  target: number
}

export interface GalleryItem {
  id: number
  label: string
  category: string
  year: string
  span: 1 | 2
}

export type EventKind = 'show' | 'competition' | 'camp'

export interface EventItem {
  id: number
  day: string
  month: string
  title: string
  location: string
  kind: EventKind
}

export interface Sponsor {
  name: string
  description: string
}

export interface HomeData {
  heroStats: HeroStat[]
  about: AboutSection
  achievements: Achievement[]
  majorette: Majorette
  goal: TeamGoal
  gallery: GalleryItem[]
  events: EventItem[]
  sponsors: Sponsor[]
}
