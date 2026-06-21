import { render, screen } from '@testing-library/react'
import { AdminMotmPage } from './motm/AdminMotmPage'
import { AdminSponsorsPage } from './sponsors/AdminSponsorsPage'
import { AdminStatsPage } from './stats/AdminStatsPage'
import { AdminTrainersPage } from './trainers/AdminTrainersPage'
import { AdminEventsPage } from './events/AdminEventsPage'
import { AdminAchievementsPage } from './achievements/AdminAchievementsPage'

test('motm renders both panels with inert buttons', () => {
  render(<AdminMotmPage />)
  expect(screen.getByRole('heading', { name: 'Mažoretka měsíce' })).toBeInTheDocument()
  expect(screen.getByRole('heading', { name: 'Týmový cíl' })).toBeInTheDocument()
  expect(screen.getByRole('button', { name: 'Zveřejnit výběr' })).toBeDisabled()
  expect(screen.getByRole('button', { name: 'Nastavit cíl' })).toBeDisabled()
})

test('sponsors renders the grid', () => {
  render(<AdminSponsorsPage />)
  expect(screen.getByRole('button', { name: '+ Přidat sponzora' })).toBeDisabled()
})

test('stats renders KPIs and the category chart', () => {
  render(<AdminStatsPage />)
  expect(screen.getByRole('heading', { name: 'Členky podle kategorie' })).toBeInTheDocument()
})

test('trainers renders add form + list', () => {
  render(<AdminTrainersPage />)
  expect(screen.getByRole('heading', { name: 'Přidat trenéra / vedení' })).toBeInTheDocument()
  expect(screen.getByRole('button', { name: 'Přidat trenéra' })).toBeDisabled()
})

test('events renders add form', () => {
  render(<AdminEventsPage />)
  expect(screen.getByRole('button', { name: 'Přidat do kalendáře' })).toBeDisabled()
})

test('achievements renders add form', () => {
  render(<AdminAchievementsPage />)
  expect(screen.getByRole('button', { name: 'Přidat úspěch' })).toBeDisabled()
})
