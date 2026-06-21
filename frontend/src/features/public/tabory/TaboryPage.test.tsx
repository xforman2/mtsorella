import { render, screen, fireEvent } from '@testing-library/react'
import { TaboryPage } from './TaboryPage'

test('renders the camps page with the locked application by default', () => {
  render(<TaboryPage override="locked" />)
  expect(screen.getByRole('heading', { name: 'Letní tábory MT Sorella' })).toBeInTheDocument()
  expect(screen.getByRole('heading', { name: 'Minulé ročníky' })).toBeInTheDocument()
  expect(screen.getByRole('button', { name: /Přihláška uzamčena/ })).toBeDisabled()
})

test('open application submits to a confirmation state', () => {
  render(<TaboryPage override="open" />)
  expect(screen.getByRole('heading', { name: 'Přihlásit na tábor' })).toBeInTheDocument()

  fireEvent.click(screen.getByRole('button', { name: 'Odeslat přihlášku' }))
  expect(screen.getByRole('heading', { name: 'Přihláška odeslána!' })).toBeInTheDocument()
})
