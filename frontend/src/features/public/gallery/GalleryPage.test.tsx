import { render, screen, fireEvent } from '@testing-library/react'
import { GalleryPage } from './GalleryPage'

test('renders the gallery and filters by category', () => {
  render(<GalleryPage />)
  expect(screen.getByRole('heading', { name: 'Naše momentky' })).toBeInTheDocument()

  // a training photo is visible by default…
  expect(screen.getByText('[ synchro trénink ]')).toBeInTheDocument()
  // …and hidden once we filter to competitions
  fireEvent.click(screen.getByRole('button', { name: 'Soutěže' }))
  expect(screen.queryByText('[ synchro trénink ]')).not.toBeInTheDocument()
})
