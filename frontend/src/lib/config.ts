/** Runtime configuration derived from Vite env vars (see .env.example). */
export const config = {
  /** Base URL for the backend API. Set `VITE_API_URL`; falls back to "/api". */
  apiBaseUrl: import.meta.env.VITE_API_URL ?? '/api',
} as const
