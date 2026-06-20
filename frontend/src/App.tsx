import { BrowserRouter, Routes, Route } from 'react-router-dom'
import { PublicLayout } from './components/Layout/PublicLayout'
import { MemberLayout } from './components/Layout/MemberLayout'
import { AdminLayout } from './components/Layout/AdminLayout'
import { HomePage } from './features/public/HomePage'
import { LoginPage } from './features/public/login/LoginPage'
import { ApplyPage } from './features/public/apply/ApplyPage'
import { DashboardPage } from './features/member/DashboardPage'
import { AdminHomePage } from './features/admin/AdminHomePage'
import { NotFound } from './components/NotFound'

/**
 * App shell + routing for the three access zones (public / member / admin).
 * Role-based guards for the member and admin zones are a follow-up (FE-3).
 */
export function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route element={<PublicLayout />}>
          <Route index element={<HomePage />} />
          <Route path="login" element={<LoginPage />} />
          <Route path="apply" element={<ApplyPage />} />
        </Route>
        <Route path="app" element={<MemberLayout />}>
          <Route index element={<DashboardPage />} />
        </Route>
        <Route path="admin" element={<AdminLayout />}>
          <Route index element={<AdminHomePage />} />
        </Route>
        <Route path="*" element={<NotFound />} />
      </Routes>
    </BrowserRouter>
  )
}
