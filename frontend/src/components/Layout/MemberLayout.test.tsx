import { render, screen } from '@testing-library/react'
import { MemoryRouter, Routes, Route } from 'react-router-dom'
import { MemberLayout } from './MemberLayout'
import { MemberDashboardPage } from '../../features/member/dashboard/MemberDashboardPage'

test('member shell renders the member nav + points badge and the dashboard', () => {
  render(
    <MemoryRouter initialEntries={['/app']}>
      <Routes>
        <Route path="/app" element={<MemberLayout />}>
          <Route index element={<MemberDashboardPage />} />
        </Route>
      </Routes>
    </MemoryRouter>,
  )
  for (const label of ['Přehled', 'Tím', 'Tréninky', 'Výzvy', 'Rebříček', 'Cíle', 'Profil']) {
    expect(screen.getAllByText(label).length).toBeGreaterThan(0)
  }
  expect(screen.getByRole('link', { name: 'Odhlásit' })).toBeInTheDocument()
  // dashboard greeting + inert RSVP
  expect(screen.getByRole('heading', { name: /Ahoj, Sofi/ })).toBeInTheDocument()
  expect(screen.getByRole('button', { name: 'Přijdu' })).toBeDisabled()
})
