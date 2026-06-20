import { render, screen } from '@testing-library/react'
import { ContactPage } from './ContactPage'

test('renders the contact page with an inert form', () => {
  render(<ContactPage />)
  expect(screen.getByRole('heading', { name: 'Kontakt' })).toBeInTheDocument()
  expect(screen.getByRole('button', { name: 'Odeslat zprávu' })).toBeDisabled()
})
