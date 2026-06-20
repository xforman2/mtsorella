export interface GalleryPhoto {
  id: number
  cat: string
  year: string
  span: 1 | 2
  label: string
}

/** Filter categories; "Vše" (all) is the default. Dummy data until the backend (#3). */
export const galleryCategories = ['Vše', 'Soutěže', 'Tréninky', 'Vystoupení', 'Zákulisí'] as const

export const galleryPhotos: GalleryPhoto[] = [
  { id: 1, cat: 'Soutěže', year: '2025', span: 2, label: 'MČR 2025 — finále' },
  { id: 2, cat: 'Tréninky', year: '2025', span: 1, label: 'synchro trénink' },
  { id: 3, cat: 'Vystoupení', year: '2025', span: 1, label: 'Den města' },
  { id: 4, cat: 'Zákulisí', year: '2024', span: 1, label: 'před nástupem' },
  { id: 5, cat: 'Soutěže', year: '2025', span: 1, label: 'European Cup' },
  { id: 6, cat: 'Vystoupení', year: '2024', span: 2, label: 'pom-pom show' },
  { id: 7, cat: 'Tréninky', year: '2024', span: 1, label: 'letní soustředění' },
  { id: 8, cat: 'Zákulisí', year: '2025', span: 1, label: 'rozcvička' },
  { id: 9, cat: 'Soutěže', year: '2024', span: 1, label: 'MČR 2024 — zlato' },
]
