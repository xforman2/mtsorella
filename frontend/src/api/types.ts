/**
 * Hand-written API types, agreed directly with the backend developer (issue #3).
 * Add request/response shapes here as endpoints are defined together — no codegen.
 */

/** GET /health */
export interface HealthStatus {
  status: string
}
