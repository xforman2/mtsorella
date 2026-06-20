import { render, screen, fireEvent } from '@testing-library/react'
import { TeamPage } from './team/TeamPage'
import { BoardPage } from './board/BoardPage'
import { GoalsPage } from './goals/GoalsPage'

test('team grid opens a member detail modal', () => {
  render(<TeamPage />)
  expect(screen.getByRole('heading', { name: 'Naše členky' })).toBeInTheDocument()
  fireEvent.click(screen.getByRole('button', { name: /Adéla Marková/ }))
  // bio appears in the modal
  expect(screen.getByText(/Kapitánka týmu/)).toBeInTheDocument()
})

test('board renders announcements with inert reactions', () => {
  render(<BoardPage />)
  expect(screen.getByRole('heading', { name: 'Nástěnka' })).toBeInTheDocument()
  expect(screen.getByText('Připnuté')).toBeInTheDocument()
  expect(screen.getAllByRole('button', { name: /👍/ })[0]).toBeDisabled()
})

test('goals renders the current goal + completed history', () => {
  render(<GoalsPage />)
  expect(screen.getByRole('heading', { name: 'Týmové cíle' })).toBeInTheDocument()
  expect(screen.getByRole('heading', { name: 'Splněné cíle' })).toBeInTheDocument()
})
