import { render, screen } from '@testing-library/react'
import { EventsPage } from './EventsPage'

test('renders the events page with the inert export button', () => {
  render(<EventsPage />)
  expect(screen.getByRole('heading', { name: 'Vystoupení a akce' })).toBeInTheDocument()
  expect(screen.getByRole('button', { name: /Export všech do kalendáře/ })).toBeDisabled()
})
