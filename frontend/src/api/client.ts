import { config } from '../lib/config'

/** Error thrown when the API responds with a non-2xx status. */
export class ApiError extends Error {
  readonly status: number

  constructor(status: number, message: string) {
    super(message)
    this.name = 'ApiError'
    this.status = status
  }
}

/**
 * Thin typed `fetch` wrapper around the backend API.
 *
 * Request/response types are hand-written in `./types` and agreed directly
 * with the backend developer (issue #3) — there is no OpenAPI codegen.
 *
 * @example
 *   const health = await apiFetch<HealthStatus>('/health')
 */
export async function apiFetch<T>(path: string, init?: RequestInit): Promise<T> {
  const res = await fetch(`${config.apiBaseUrl}${path}`, {
    ...init,
    headers: { 'Content-Type': 'application/json', ...init?.headers },
  })

  if (!res.ok) {
    throw new ApiError(res.status, `Request to ${path} failed with ${res.status}`)
  }

  return (await res.json()) as T
}
