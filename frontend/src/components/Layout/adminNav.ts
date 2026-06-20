/** Admin nav — order matches the design's `adminDefs`. */
export const adminNav: { to: string; label: string; end?: boolean }[] = [
  { to: '/admin', label: 'Přehled', end: true },
  { to: '/admin/members', label: 'Členové' },
  { to: '/admin/trainers', label: 'Trenéři' },
  { to: '/admin/videos', label: 'Hodnocení videí' },
  { to: '/admin/challenges', label: 'Výzvy' },
  { to: '/admin/trainings', label: 'Tréninky' },
  { to: '/admin/events', label: 'Vystoupení' },
  { to: '/admin/board', label: 'Nástěnka' },
  { to: '/admin/achievements', label: 'Úspěchy' },
  { to: '/admin/motm', label: 'Mažoretka & cíle' },
  { to: '/admin/sponsors', label: 'Sponzoři' },
  { to: '/admin/stats', label: 'Statistiky' },
]
