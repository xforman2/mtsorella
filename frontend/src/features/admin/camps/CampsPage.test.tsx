import { render, screen } from '@testing-library/react'
import { CampsPage } from './CampsPage'

test('renders the camps editor and past-camps list with an inert save button', () => {
  render(<CampsPage />)
  expect(screen.getByRole('heading', { name: 'Nadcházející tábor' })).toBeInTheDocument()
  expect(screen.getByRole('heading', { name: 'Minulé tábory' })).toBeInTheDocument()
  expect(screen.getByRole('button', { name: 'Uložit tábor' })).toBeDisabled()
})
