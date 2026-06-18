import { Link } from 'react-router-dom'

export function NotFound() {
  return (
    <div style={{ minHeight: '60vh', display: 'grid', placeItems: 'center', textAlign: 'center' }}>
      <div>
        <h1 style={{ fontSize: 48 }}>404</h1>
        <p style={{ color: 'var(--color-ink-soft)', marginTop: 8 }}>Stránka nenájdená.</p>
        <Link to="/" style={{ display: 'inline-block', marginTop: 16, fontWeight: 600 }}>
          ← Domov
        </Link>
      </div>
    </div>
  )
}
