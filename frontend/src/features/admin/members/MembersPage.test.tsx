import { render, screen, fireEvent } from '@testing-library/react'
import { MembersPage } from './MembersPage'

test('renders the members table and opens the inert create-account modal', () => {
  render(<MembersPage />)
  expect(screen.getByText('Členka')).toBeInTheDocument()
  expect(screen.getByText('Nina Balážová')).toBeInTheDocument()

  // modal is closed until the button is clicked
  expect(screen.queryByRole('heading', { name: 'Vytvořit účet členky' })).not.toBeInTheDocument()
  fireEvent.click(screen.getByRole('button', { name: '+ Vytvořit účet' }))
  expect(screen.getByRole('heading', { name: 'Vytvořit účet členky' })).toBeInTheDocument()
  expect(screen.getByRole('button', { name: 'Vytvořit účet' })).toBeDisabled()
})
