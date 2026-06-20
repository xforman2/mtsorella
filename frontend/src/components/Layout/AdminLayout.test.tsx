import { render, screen } from '@testing-library/react'
import { MemoryRouter, Routes, Route } from 'react-router-dom'
import { AdminLayout } from './AdminLayout'
import { adminNav } from './adminNav'
import { AdminDashboardPage } from '../../features/admin/dashboard/AdminDashboardPage'

test('admin shell renders all nav items and the active view', () => {
  render(
    <MemoryRouter initialEntries={['/admin']}>
      <Routes>
        <Route path="/admin" element={<AdminLayout />}>
          <Route index element={<AdminDashboardPage />} />
        </Route>
      </Routes>
    </MemoryRouter>,
  )
  // every admin nav label renders (sidebar + mobile chips)
  for (const item of adminNav) {
    expect(screen.getAllByText(item.label).length).toBeGreaterThan(0)
  }
  // dashboard (index) is shown
  expect(screen.getByRole('heading', { name: 'Nové přihlášky' })).toBeInTheDocument()
})
