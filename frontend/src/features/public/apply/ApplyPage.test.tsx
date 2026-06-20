import { render, screen } from '@testing-library/react'
import { ApplyPage } from './ApplyPage'

test('renders the registration page heading and form', () => {
  render(<ApplyPage />)
  expect(screen.getByRole('heading', { name: 'Online přihláška' })).toBeInTheDocument()
  expect(screen.getByRole('heading', { name: 'Přihláška dítěte' })).toBeInTheDocument()
  expect(screen.getByRole('button', { name: 'Odeslat přihlášku' })).toBeInTheDocument()
})
