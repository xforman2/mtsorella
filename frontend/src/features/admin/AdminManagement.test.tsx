import { render, screen } from '@testing-library/react'
import { VideosPage } from './videos/VideosPage'
import { ChallengesPage } from './challenges/ChallengesPage'
import { TrainingsPage } from './trainings/TrainingsPage'
import { BoardPage } from './board/BoardPage'

test('video grading renders with inert grade button', () => {
  render(<VideosPage />)
  expect(screen.getByRole('heading', { name: 'Ohodnoceno dnes' })).toBeInTheDocument()
  expect(screen.getAllByRole('button', { name: /Přidělit body/ })[0]).toBeDisabled()
})

test('challenges renders with inert create button', () => {
  render(<ChallengesPage />)
  expect(screen.getByRole('heading', { name: 'Nová výzva' })).toBeInTheDocument()
  expect(screen.getByRole('button', { name: 'Vytvořit výzvu' })).toBeDisabled()
})

test('trainings renders with inert add button', () => {
  render(<TrainingsPage />)
  expect(screen.getByRole('heading', { name: 'Přidat trénink' })).toBeInTheDocument()
  expect(screen.getByRole('button', { name: 'Přidat do rozpisu' })).toBeDisabled()
})

test('board renders with inert publish button and a pinned announcement', () => {
  render(<BoardPage />)
  expect(screen.getByRole('heading', { name: 'Nový oznam' })).toBeInTheDocument()
  expect(screen.getByRole('button', { name: 'Publikovat oznam' })).toBeDisabled()
  expect(screen.getByText('Připnuté')).toBeInTheDocument()
})
