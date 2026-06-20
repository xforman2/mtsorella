import { render, screen, fireEvent } from '@testing-library/react'
import { ProfilePage } from './ProfilePage'

test('profile renders and opens the inert edit modal', () => {
  render(<ProfilePage />)
  expect(screen.getByRole('heading', { name: 'Získané odznaky' })).toBeInTheDocument()

  expect(screen.queryByRole('heading', { name: 'Upravit profil' })).not.toBeInTheDocument()
  fireEvent.click(screen.getByRole('button', { name: 'Upravit profil' }))
  expect(screen.getByRole('heading', { name: 'Upravit profil' })).toBeInTheDocument()
  expect(screen.getByRole('button', { name: 'Uložit změny' })).toBeDisabled()
})
