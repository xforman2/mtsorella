import { render, screen } from '@testing-library/react'
import { PartnershipPage } from './PartnershipPage'

test('renders the partnership page with an inert inquiry form', () => {
  render(<PartnershipPage />)
  expect(screen.getByRole('heading', { name: 'Staňte se partnerem' })).toBeInTheDocument()
  expect(screen.getByRole('heading', { name: 'Mám zájem o spolupráci' })).toBeInTheDocument()
  expect(screen.getByRole('button', { name: 'Odeslat dotaz' })).toBeDisabled()
})
