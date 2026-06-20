import { render, screen, fireEvent } from '@testing-library/react'
import { TrainingsPage } from './trainings/TrainingsPage'
import { ChallengesPage } from './challenges/ChallengesPage'
import { LeaderboardPage } from './leaderboard/LeaderboardPage'

test('trainings renders calendar + inert RSVP', () => {
  render(<TrainingsPage />)
  expect(screen.getByRole('heading', { name: 'Tréninky' })).toBeInTheDocument()
  expect(screen.getAllByRole('button', { name: 'Přijdu' })[0]).toBeDisabled()
})

test('challenges opens the detail modal with inert upload', () => {
  render(<ChallengesPage />)
  expect(screen.getByRole('heading', { name: 'Tréninkové výzvy' })).toBeInTheDocument()
  fireEvent.click(screen.getAllByRole('button', { name: 'Nahrát video' })[0])
  expect(screen.getByRole('button', { name: 'Vybrat video' })).toBeDisabled()
})

test('leaderboard renders the hall of fame + my row', () => {
  render(<LeaderboardPage />)
  expect(screen.getByRole('heading', { name: 'Síň slávy' })).toBeInTheDocument()
  expect(screen.getByText('TY')).toBeInTheDocument()
})
