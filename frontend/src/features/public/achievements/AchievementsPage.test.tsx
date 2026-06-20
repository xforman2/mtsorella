import { render, screen } from '@testing-library/react'
import { AchievementsPage } from './AchievementsPage'

test('renders the achievements timeline page', () => {
  render(<AchievementsPage />)
  expect(screen.getByRole('heading', { name: 'Časová osa úspěchů' })).toBeInTheDocument()
  expect(screen.getByRole('button', { name: 'Mezinárodní' })).toBeInTheDocument()
})
