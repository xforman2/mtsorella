import { render, screen } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom'
import { HomePage } from './HomePage'

test('renders all the main home-page sections', () => {
  render(
    <MemoryRouter>
      <HomePage />
    </MemoryRouter>,
  )

  expect(screen.getByRole('heading', { name: /Elegance/i })).toBeInTheDocument()
  expect(screen.getByRole('heading', { name: /Nejnovější úspěchy/i })).toBeInTheDocument()
  expect(screen.getByRole('heading', { name: /Nadcházející vystoupení/i })).toBeInTheDocument()
  expect(screen.getByRole('heading', { name: /Naši partneři a sponzoři/i })).toBeInTheDocument()
  expect(screen.getByText(/Mažoretka měsíce/i)).toBeInTheDocument()
  expect(screen.getByText(/Týmový cíl/i)).toBeInTheDocument()
})
