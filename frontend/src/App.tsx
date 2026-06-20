import { BrowserRouter, Routes, Route } from 'react-router-dom'
import { PublicLayout } from './components/Layout/PublicLayout'
import { MemberLayout } from './components/Layout/MemberLayout'
import { AdminLayout } from './components/Layout/AdminLayout'
import { HomePage } from './features/public/HomePage'
import { LoginPage } from './features/public/login/LoginPage'
import { ApplyPage } from './features/public/apply/ApplyPage'
import { GalleryPage } from './features/public/gallery/GalleryPage'
import { AchievementsPage } from './features/public/achievements/AchievementsPage'
import { CoachesPage } from './features/public/coaches/CoachesPage'
import { EventsPage } from './features/public/events/EventsPage'
import { SponsorsPage } from './features/public/sponsors/SponsorsPage'
import { ContactPage } from './features/public/contact/ContactPage'
import { PartnershipPage } from './features/public/partnership/PartnershipPage'
import { DashboardPage } from './features/member/DashboardPage'
import { AdminDashboardPage } from './features/admin/dashboard/AdminDashboardPage'
import { MembersPage } from './features/admin/members/MembersPage'
import { VideosPage } from './features/admin/videos/VideosPage'
import { ChallengesPage } from './features/admin/challenges/ChallengesPage'
import { TrainingsPage } from './features/admin/trainings/TrainingsPage'
import { BoardPage } from './features/admin/board/BoardPage'
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
          <Route path="gallery" element={<GalleryPage />} />
          <Route path="achievements" element={<AchievementsPage />} />
          <Route path="coaches" element={<CoachesPage />} />
          <Route path="events" element={<EventsPage />} />
          <Route path="sponsors" element={<SponsorsPage />} />
          <Route path="contact" element={<ContactPage />} />
          <Route path="partnership" element={<PartnershipPage />} />
        </Route>
        <Route path="app" element={<MemberLayout />}>
          <Route index element={<DashboardPage />} />
        </Route>
        <Route path="admin" element={<AdminLayout />}>
          <Route index element={<AdminDashboardPage />} />
          <Route path="members" element={<MembersPage />} />
          <Route path="videos" element={<VideosPage />} />
          <Route path="challenges" element={<ChallengesPage />} />
          <Route path="trainings" element={<TrainingsPage />} />
          <Route path="board" element={<BoardPage />} />
        </Route>
        <Route path="*" element={<NotFound />} />
      </Routes>
    </BrowserRouter>
  )
}
