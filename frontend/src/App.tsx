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
import { TaboryPage } from './features/public/tabory/TaboryPage'
import { EventsPage } from './features/public/events/EventsPage'
import { SponsorsPage } from './features/public/sponsors/SponsorsPage'
import { ContactPage } from './features/public/contact/ContactPage'
import { PartnershipPage } from './features/public/partnership/PartnershipPage'
import { MemberDashboardPage } from './features/member/dashboard/MemberDashboardPage'
import { ProfilePage } from './features/member/profile/ProfilePage'
import { TrainingsPage as MemberTrainingsPage } from './features/member/trainings/TrainingsPage'
import { ChallengesPage as MemberChallengesPage } from './features/member/challenges/ChallengesPage'
import { LeaderboardPage } from './features/member/leaderboard/LeaderboardPage'
import { TeamPage } from './features/member/team/TeamPage'
import { BoardPage as MemberBoardPage } from './features/member/board/BoardPage'
import { GoalsPage } from './features/member/goals/GoalsPage'
import { AdminDashboardPage } from './features/admin/dashboard/AdminDashboardPage'
import { MembersPage } from './features/admin/members/MembersPage'
import { CampsPage } from './features/admin/camps/CampsPage'
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
          <Route path="tabory" element={<TaboryPage />} />
          <Route path="events" element={<EventsPage />} />
          <Route path="sponsors" element={<SponsorsPage />} />
          <Route path="contact" element={<ContactPage />} />
          <Route path="partnership" element={<PartnershipPage />} />
        </Route>
        <Route path="app" element={<MemberLayout />}>
          <Route index element={<MemberDashboardPage />} />
          <Route path="profile" element={<ProfilePage />} />
          <Route path="trainings" element={<MemberTrainingsPage />} />
          <Route path="challenges" element={<MemberChallengesPage />} />
          <Route path="leaderboard" element={<LeaderboardPage />} />
          <Route path="team" element={<TeamPage />} />
          <Route path="board" element={<MemberBoardPage />} />
          <Route path="goals" element={<GoalsPage />} />
        </Route>
        <Route path="admin" element={<AdminLayout />}>
          <Route index element={<AdminDashboardPage />} />
          <Route path="members" element={<MembersPage />} />
          <Route path="camps" element={<CampsPage />} />
        </Route>
        <Route path="*" element={<NotFound />} />
      </Routes>
    </BrowserRouter>
  )
}
