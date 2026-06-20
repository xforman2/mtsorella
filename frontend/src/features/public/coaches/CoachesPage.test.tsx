import { render, screen } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom'
import { CoachesPage } from './CoachesPage'

test('renders the coaches page with the join CTA', () => {
  render(
    <MemoryRouter>
      <CoachesPage />
    </MemoryRouter>,
  )
  expect(screen.getByRole('heading', { name: 'Trenéři a vedení' })).toBeInTheDocument()
  expect(screen.getByRole('link', { name: 'Online přihláška' })).toBeInTheDocument()
})
