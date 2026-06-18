import { render, screen } from '@testing-library/react'
import { App } from './App'

test('renders the MT Sorella home hero on the index route', () => {
  render(<App />)
  expect(screen.getByRole('heading', { name: /Elegance v každém pohybu/i })).toBeInTheDocument()
})
