import { render, screen } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom'
import { LoginPage } from './LoginPage'

test('renders the login page with an inert submit button', () => {
  render(
    <MemoryRouter>
      <LoginPage />
    </MemoryRouter>,
  )
  expect(screen.getByRole('heading', { name: 'Přihlášení' })).toBeInTheDocument()
  expect(screen.getByRole('button', { name: 'Přihlásit se' })).toBeDisabled()
})
