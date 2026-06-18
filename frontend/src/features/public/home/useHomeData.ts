import { homeData } from './mockData'
import type { HomeData } from './types'

/**
 * Home-page data source. Returns local dummy data for now; once the backend
 * (#3) exposes the endpoints, swap this single function to `apiFetch` (and make
 * it async) — components consume `HomeData` either way.
 */
export function useHomeData(): HomeData {
  return homeData
}
