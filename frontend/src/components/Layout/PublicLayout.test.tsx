import { render, screen } from '@testing-library/react'
import { MemoryRouter, Routes, Route } from 'react-router-dom'
import { PublicLayout } from './PublicLayout'

function renderLayout() {
  render(
    <MemoryRouter>
      <Routes>
        <Route element={<PublicLayout />}>
          <Route index element={<p>obsah</p>} />
        </Route>
      </Routes>
    </MemoryRouter>,
  )
}

test('public header renders the nav links and auth CTAs', () => {
  renderLayout()
  for (const label of [
    'Galerie',
    'Úspěchy',
    'Trenéři',
    'Vystoupení',
    'Sponzoři',
    'Kontakt',
    'Spolupráce',
  ]) {
    expect(screen.getByRole('link', { name: label })).toBeInTheDocument()
  }
  expect(screen.getByRole('link', { name: 'Přihlášení' })).toBeInTheDocument()
  expect(screen.getByRole('link', { name: 'Přihláška' })).toBeInTheDocument()
})
