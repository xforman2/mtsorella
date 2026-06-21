# MT Sorella — Functional Requirements

This document describes the functional requirements of the website and application for the competitive majorette team **MT Sorella**. It is derived from the high-fidelity prototype (`MT Sorella.dc.html`).

The system has **three access layers**:
1. **Public area** — accessible to anyone without logging in
2. **Member zone** — for logged-in team members (gamification)
3. **Admin panel** — for coaches / team management

---

## 1. Common / System Requirements

| ID | Requirement |
|----|-----------|
| FR-S1 | The site is fully responsive — works on both desktop and mobile. |
| FR-S2 | Desktop has a top navigation menu; mobile has a hamburger menu with an overlay panel. |
| FR-S3 | Logged-in members have a bottom navigation bar on mobile (Overview, Trainings, Challenges, Leaderboard, Profile). |
| FR-S4 | The system distinguishes three roles: guest (not logged in), member, admin (coach). |
| FR-S5 | Navigation and available sections change based on the user's role. |
| FR-S6 | Consistent visual style: green color palette, Bricolage Grotesque + Inter fonts, minimal emoji. |
| FR-S8 | All interface copy is in **Czech**. Club is based in Brno (CZ); championships, contacts (.cz, +420) and venues are localized accordingly. |
| FR-S7 | Photos are added via drag-and-drop slots (hero, "About us", majorette of the month) that persist locally. |

---

## 2. Public Area

### 2.1 Home Page
| ID | Requirement |
|----|-----------|
| FR-P1 | Hero section with team name, slogan, and photo (photo slot) + CTA buttons ("Meet our team", "View gallery"). |
| FR-P2 | Hero displays key team statistics (number of members, medals, competitions, years active). |
| FR-P3 | "About us" section with team description and photo. |
| FR-P4 | "Latest achievements" section — preview of 3 awards with a link to the full timeline. |
| FR-P5 | "Majorette of the month" section — highlighted member with photo and reasoning. |
| FR-P6 | "Team goal" section — visualization of the shared points goal with progress. |
| FR-P7 | Gallery preview (masonry grid) with a link to the full gallery. |
| FR-P8 | Preview of upcoming performances with a link to the full calendar. |
| FR-P9 | Sponsors and partners section + CTA for partnership. |

### 2.2 Gallery
| ID | Requirement |
|----|-----------|
| FR-P10 | Photo grid (masonry layout) with various tile sizes. |
| FR-P11 | Filter photos by category (Competition, Training, Performance, Behind the scenes). |
| FR-P12 | Clicking a photo opens an enlarged view (lightbox). |
| FR-P13 | Each photo displays its category and year on hover. |

### 2.3 Achievements
| ID | Requirement |
|----|-----------|
| FR-P14 | Timeline of awards sorted from newest. |
| FR-P15 | Filter achievements by year. |
| FR-P16 | Each achievement displays year, competition type (national/international/regional), name, placement, medal, and description. |
| FR-P17 | Summary statistics (total medals, gold medals, competitions, years of history). |

### 2.4 Coaches (Public)
| ID | Requirement |
|----|-----------|
| FR-P18 | Only the coaching staff is shown publicly (not internal members — privacy protection). |
| FR-P19 | Each coach profile contains name, role, years in the team, and a short bio. |
| FR-P20 | CTA to submit an online application. |

### 2.5 Performances
| ID | Requirement |
|----|-----------|
| FR-P21 | List of upcoming performances and events with date, location, and type. |
| FR-P22 | Each performance has an "+ Add to calendar" button — export to `.ics`. |
| FR-P23 | "Export all to calendar" button — export the entire list to `.ics`. |

### 2.5a Camps
| ID | Requirement |
|----|-----------|
| FR-P34 | Dedicated **"Tábory"** (summer day-camp) section, reachable from the main navigation and the footer. |
| FR-P35 | A single highlighted **upcoming camp** card: photo slot, name, dates, location, age range, price, and description. Only one upcoming camp exists at a time. |
| FR-P36 | The camp application (**"přihláška"**) is an *unlockable* element. While registration is not yet open it is **locked**: it shows a padlock, an explanatory message, a countdown of days remaining, and a disabled button. |
| FR-P37 | When the registration-open date is reached (or an admin opens it manually) the application **unlocks** into a working form — child's name, date of birth, parent's name, e-mail, phone, note, and consent — with a submitted/confirmation state. |
| FR-P38 | **Past camps** are shown as a grid of cards with photo slots, year badge, dates, location, number of children, and a short description. |

### 2.6 Sponsors
| ID | Requirement |
|----|-----------|
| FR-P24 | List of partners and sponsors with descriptions. |
| FR-P25 | CTA to the partnership form. |

### 2.7 Contact
| ID | Requirement |
|----|-----------|
| FR-P26 | Contact form (name, email, message). |
| FR-P27 | Contact details (address, email, phone, training times) + map. |

### 2.8 Online Application (Form)
| ID | Requirement |
|----|-----------|
| FR-P28 | Child application form: name, date of birth, category of interest. |
| FR-P29 | Parent details: name, email, phone. |
| FR-P30 | Optional field about previous experience + consent to data processing. |
| FR-P31 | A confirmation message is shown after submission. |

### 2.9 Partnership / Cooperation (Form)
| ID | Requirement |
|----|-----------|
| FR-P32 | Form for companies/partners: company name, contact person, email, phone, type of cooperation, message. |
| FR-P33 | A confirmation message is shown after submission. |

---

## 3. Member Zone (After Login)

### 3.1 Login
| ID | Requirement |
|----|-----------|
| FR-M1 | Login via parent's email and password. |
| FR-M2 | Registration is not public — accounts are created exclusively by the admin. |
| FR-M3 | Separate entry for coaches into the admin panel. |

### 3.2 Overview (Dashboard)
| ID | Requirement |
|----|-----------|
| FR-M4 | Personalized greeting for the member. |
| FR-M5 | Slim stats strip: points, streak, leaderboard rank, attendance. |
| FR-M6 | Next training card with attendance confirmation (Attending / Not attending). |
| FR-M7 | Preview of active challenges with a link to the full section. |
| FR-M8 | Preview of the team goal with progress. |
| FR-M9 | Preview of the latest announcements from the board. |
| FR-M10 | Quick link to one's own profile. |

### 3.3 Team (Members)
| ID | Requirement |
|----|-----------|
| FR-M11 | Complete list of members (visible only in the logged-in zone). |
| FR-M12 | Filter by category (Juniors, Cadets, Seniors). |
| FR-M13 | Clicking a member opens a detail view (name, nickname, category, role, level, years, bio). |

### 3.4 Trainings
| ID | Requirement |
|----|-----------|
| FR-M14 | Monthly calendar marking days with training and performances; the current day is highlighted. |
| FR-M15 | List of upcoming trainings with time, location, category, and what to bring. |
| FR-M16 | Attendance confirmation for each training (Attending / Not attending). |
| FR-M17 | A member earns points for confirmed attendance. |
| FR-M18 | Export an individual training ("+ Calendar") and the entire schedule to `.ics`. |

### 3.5 Announcement Board
| ID | Requirement |
|----|-----------|
| FR-M19 | List of announcements from coaches. |
| FR-M20 | Pinned/important announcements are highlighted. |
| FR-M21 | Filtering (all / pinned). |
| FR-M22 | Reactions to announcements (like / heart). |

### 3.6 Challenges (Gamification)
| ID | Requirement |
|----|-----------|
| FR-M23 | List of active challenges with an instructional video, description, deadline, and point value. |
| FR-M24 | A member can upload their own training video for a challenge. |
| FR-M25 | Challenge detail in a modal with the option to upload a video. |
| FR-M26 | Challenge history with the achieved rating/score. |
| FR-M27 | Points system: +10 for completion, +5 bonus for completing before the deadline. |

### 3.7 Leaderboard
| ID | Requirement |
|----|-----------|
| FR-M28 | TOP 3 members podium (visually distinguished 1st/2nd/3rd place). |
| FR-M29 | Toggle between seasonal / lifetime leaderboard. |
| FR-M30 | Filter by category. |
| FR-M31 | Ranking list with points, level, and category; the user's own row is highlighted. |

### 3.8 Team Goals
| ID | Requirement |
|----|-----------|
| FR-M32 | Display of the current shared goal with progress and remaining points. |
| FR-M33 | History of completed goals. |

### 3.9 Profile
| ID | Requirement |
|----|-----------|
| FR-M34 | Profile header: name, nickname, category, level, streak, total points. |
| FR-M35 | Current level progress + attendance. |
| FR-M36 | Earned badges. |
| FR-M37 | Points history (what, how much, when). |
| FR-M38 | List of one's own uploaded videos with ratings. |
| FR-M39 | Profile editing via a modal (name, nickname, email, photo). |

### 3.10 Levels and Points
| ID | Requirement |
|----|-----------|
| FR-M40 | The points system determines a member's level (Beginner → Sorella). |
| FR-M41 | Each level has a name, point range, and visual indicator. |
| FR-M42 | Streak (consecutive days) with milestones. |

---

## 4. Admin Panel (Coaches / Management)

| ID | Requirement |
|----|-----------|
| FR-A1 | Separate interface with a side menu (desktop) / horizontal tabs (mobile). |
| FR-A2 | Overview: summary statistics, new applications, videos awaiting review. |
| FR-A3 | Members: table of all members with points, category, and level. |
| FR-A4 | Creating member accounts (name, nickname, category, parent's email) — password is generated automatically. |
| FR-A5 | Coaches: managing who is displayed on the public site (add, edit, hide). |
| FR-A6 | Video review: assessing execution quality (0–20 points) + automatically +10 for completion. |
| FR-A7 | Challenges: creating new challenges with name, description, deadline, points, and instructional video. |
| FR-A8 | Trainings: adding trainings to the schedule (date, time, location, category, recurrence). |
| FR-A9 | Performances: creating performances/events for the public calendar. |
| FR-A10 | Announcement board: publishing announcements (with the option to pin as important). |
| FR-A11 | Achievements: adding/editing/deleting awards displayed on the public timeline. |
| FR-A12 | Majorette & goals: selecting the majorette of the month + setting the team goal. |
| FR-A13 | Sponsors: managing partners displayed on the site (add, edit, remove). |
| FR-A14 | Statistics: key metrics (team points, attendance, completed challenges, medals) + member breakdown by category. |
| FR-A15 | Camps (Tábory): edit the upcoming camp (name, dates, location, age, price, capacity, description, **registration-open date**, manual "open now" toggle) with a live status banner (locked / open); manage past camps (add, edit, remove). |

---

## 5. Frontend Requirements

The client-side part (what the user sees and interacts with in the browser).

| ID | Requirement | Related to |
|----|-----------|----------|
| FE-1 | Responsive layout (desktop + mobile) with breakpoints and mobile navigation. | FR-S1–S3 |
| FE-2 | Routing between screens without reload (public area, member zone, admin). | FR-S4–S5 |
| FE-3 | Conditional rendering of navigation and sections based on role (guest / member / admin). | FR-S4–S5 |
| FE-4 | Consistent design system: colors, typography (Bricolage Grotesque + Inter), components. | FR-S6 |
| FE-5 | Drag-and-drop photo slots with local storage (localStorage) — hero, about, majorette, and camp photos. | FR-S7 |
| FE-16 | Date-driven unlock of the camp application: compare the current date with the registration-open date to render the locked countdown vs. the open form; overridable via the `registrationOverride` tweak (`auto` / `open` / `locked`). | FR-P36, FR-P37 |
| FE-6 | Lightbox / modals (gallery, member detail, challenge detail, profile editing, account creation). | FR-P12, FR-M13, FR-M25, FR-M39, FR-A4 |
| FE-7 | Client-side filtering and sorting (gallery, achievements, members, leaderboard, board). | FR-P11, FR-P15, FR-M12, FR-M30, FR-M21 |
| FE-8 | Client-side form validation (required fields, email format, consent). | FR-P28–P33, FR-M1 |
| FE-9 | Progress visualization (goal progress bars, levels, leaderboard podium). | FR-P6, FR-M8, FR-M28, FR-M32 |
| FE-10 | Interactive monthly calendar marking trainings/performances. | FR-M14 |
| FE-11 | Generating and downloading the `.ics` file on the client. | FR-P22–P23, FR-M18 |
| FE-12 | States for attendance confirmation, reactions, and submitted forms (instant UI feedback). | FR-M6, FR-M16, FR-M22, FR-P31, FR-P33 |
| FE-13 | Upload UI for challenge videos (file select/drag, preview, progress). | FR-M24 |
| FE-14 | Displaying points, levels, streak, and badges from user data. | FR-M40–M42, FR-M36 |
| FE-15 | Accessibility and readability (contrast, minimum 44px touch targets on mobile). | FR-S1 |

---

## 6. Backend Requirements

The server-side part (data, logic, authentication, storage). Not implemented in the prototype — this is the specification for production deployment.

### 6.1 Authentication and Users
| ID | Requirement | Related to |
|----|-----------|----------|
| BE-1 | Login via email and password with secure password hashing. | FR-M1 |
| BE-2 | Role and permission management (guest / member / admin) at the API level. | FR-S4 |
| BE-3 | Member accounts created exclusively by admin + generating and sending a temporary password. | FR-M2, FR-A4 |
| BE-4 | Password recovery and session management (session / token). | FR-M1 |
| BE-5 | Separate admin access for coaches. | FR-M3 |

### 6.2 Data and CRUD Operations
| ID | Requirement | Related to |
|----|-----------|----------|
| BE-6 | CRUD for members (including category, points, attendance). | FR-A3, FR-M11 |
| BE-7 | CRUD for coaches + "show on website" flag. | FR-A5, FR-P18 |
| BE-8 | CRUD for achievements / awards. | FR-A11, FR-P14 |
| BE-9 | CRUD for performances and trainings (including recurring trainings). | FR-A8, FR-A9 |
| BE-10 | CRUD for board announcements (including pinning). | FR-A10, FR-M19 |
| BE-11 | CRUD for challenges + instructional videos. | FR-A7, FR-M23 |
| BE-12 | CRUD for sponsors. | FR-A13 |
| BE-13 | Gallery management (categories, years, photo metadata). | FR-P10 |
| BE-14 | Selecting the majorette of the month and managing team goals. | FR-A12, FR-M32 |
| BE-14a | CRUD for camps (upcoming + past) incl. registration-open date; the application gate opens automatically on that date (or by manual admin override). | FR-A15, FR-P36, FR-P37 |

### 6.3 Gamification and Points System
| ID | Requirement | Related to |
|----|-----------|----------|
| BE-15 | Calculating and storing points, levels, and streaks for each member. | FR-M40–M42 |
| BE-16 | Awarding points for training attendance (confirmed attendance). | FR-M17 |
| BE-17 | Awarding points for challenges (+10 completion, +5 before deadline, +0–20 for quality). | FR-M27, FR-A6 |
| BE-18 | Calculating the leaderboard (both seasonal and lifetime) and rankings. | FR-M28–M31 |
| BE-19 | Aggregating team goals from the sum of members' points. | FR-M32 |
| BE-20 | Managing and assigning badges. | FR-M36 |

### 6.4 Media and Files
| ID | Requirement | Related to |
|----|-----------|----------|
| BE-21 | Storing and serving photos (gallery, profiles, hero). | FR-P10, FR-S7 |
| BE-22 | Uploading, storing, and playing videos (member challenge videos + instructional videos). | FR-M24, FR-A6 |
| BE-23 | Media optimization / thumbnail generation. | FR-P10 |

### 6.5 Communication and Integrations
| ID | Requirement | Related to |
|----|-----------|----------|
| BE-24 | Processing submitted forms (application, partnership, contact) + notifications. | FR-P28–P33, FR-P26 |
| BE-25 | Sending emails (login credentials, confirmations, announcements). | FR-A4, FR-A10 |
| BE-26 | Generating `.ics` calendar data on the server (alternative to client-side export). | FR-M18, FR-P22 |
| BE-27 | Video review by admin and notifying the member about awarded points. | FR-A6 |

### 6.6 Non-functional / Operational
| ID | Requirement |
|----|-----------|
| BE-28 | GDPR — consent to processing personal data (especially minors), right to erasure. |
| BE-29 | API security (HTTPS, CSRF/XSS protection, rate limiting). |
| BE-30 | Database and media backups. |
| BE-31 | Logging and change audit (who changed data and when). |

---

## 7. Prototype Notes

- This is a **high-fidelity prototype** — forms and actions show confirmation states but do not persist data to a server.
- Real data (photos, logo, member names, contacts) is replaced with realistic placeholder content.
- Calendar export (`.ics`) is fully functional and opens in Google / Apple / Outlook calendars.
- Photo slots store images locally in the browser (localStorage).
- The whole prototype is in **Czech**.
- There is a **single upcoming camp**; its application unlocks automatically on the registration-open date. The locked/open state can be previewed with the `registrationOverride` tweak (`auto` / `open` / `locked`).
- Production deployment requires a backend (authentication, database, video and photo storage, email sending).
