import { render, screen } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom'
import { SponsorsPage } from './SponsorsPage'

test('renders the sponsors page with the partnership CTA', () => {
  render(
    <MemoryRouter>
      <SponsorsPage />
    </MemoryRouter>,
  )
  expect(screen.getByRole('heading', { name: 'Partneři a sponzoři' })).toBeInTheDocument()
  expect(screen.getByRole('link', { name: 'Staňte se partnerem' })).toBeInTheDocument()
})
