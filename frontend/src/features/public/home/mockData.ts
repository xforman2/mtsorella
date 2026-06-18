import type { HomeData } from './types'

/**
 * Dummy home-page data (Czech) — stands in until the backend (#3) exists.
 * Swap the consumer (`useHomeData`) to `apiFetch` later; this file goes away.
 */
export const homeData: HomeData = {
  heroStats: [
    { value: '32', label: 'členek' },
    { value: '24', label: 'medailí' },
    { value: '38', label: 'soutěží' },
    { value: '12', label: 'let' },
  ],
  about: {
    heading: 'Tým, který tančí srdcem',
    paragraphs: [
      'MT Sorella je soutěžní mažoretkový tým, který už více než deset let spojuje dívky milující pohyb, hudbu a disciplínu. Naše členky reprezentují na domácích i mezinárodních soutěžích.',
      '„Sorella" znamená sestra — a přesně tak se k sobě chováme. Za každou medailí stojí poctivý trénink, vzájemná podpora a radost z tance.',
    ],
  },
  achievements: [
    {
      year: '2025',
      competition: 'Mistrovství ČR v mažoretkovém sportu',
      placement: '1. místo — zlato',
      medal: 'gold',
      description: 'Zlato v kategorii pom-pom seniorky s rekordním bodovým hodnocením.',
    },
    {
      year: '2025',
      competition: 'European Cup Praha',
      placement: '2. místo — stříbro',
      medal: 'silver',
      description: 'Stříbrná medaile v silné mezinárodní konkurenci.',
    },
    {
      year: '2024',
      competition: 'Mistrovství ČR',
      placement: '1. místo — zlato',
      medal: 'gold',
      description: 'Obhajoba titulu mistryň republiky.',
    },
  ],
  majorette: {
    name: 'Nina Balážová',
    nickname: 'Ninka',
    category: 'Kadetky',
    reason: 'Za největší pokrok v sezóně a obětavou pomoc mladším členkám během každého tréninku.',
  },
  goal: {
    title: 'Společný výlet do aquaparku',
    current: 7240,
    target: 10000,
  },
  gallery: [
    { id: 1, label: 'soutěž · MČR 2025', category: 'Soutěž', year: '2025', span: 2 },
    { id: 2, label: 'trénink · synchro', category: 'Trénink', year: '2025', span: 1 },
    { id: 3, label: 'vystoupení · město', category: 'Vystoupení', year: '2025', span: 1 },
    { id: 4, label: 'zákulisí', category: 'Zákulisí', year: '2024', span: 1 },
    { id: 5, label: 'soutěž · Praha', category: 'Soutěž', year: '2025', span: 1 },
    { id: 6, label: 'vystoupení · pom-pom', category: 'Vystoupení', year: '2024', span: 2 },
  ],
  events: [
    {
      id: 1,
      day: '21',
      month: 'Čvn',
      title: 'Den města — slavnostní průvod',
      location: 'Hlavní náměstí, 14:00',
      kind: 'show',
    },
    {
      id: 2,
      day: '05',
      month: 'Čvc',
      title: 'Letní soustředění týmu',
      location: 'Penzion Limba, Beskydy',
      kind: 'camp',
    },
    {
      id: 3,
      day: '14',
      month: 'Zář',
      title: 'Pohár ČR — podzimní kolo',
      location: 'Sportovní hala, 9:00',
      kind: 'competition',
    },
  ],
  sponsors: [
    { name: 'MĚSTO BRNO', description: 'Hlavní partner týmu.' },
    { name: 'STUDIO RYTMUS', description: 'Taneční prostory a choreografická podpora.' },
    { name: 'LÉKÁRNA HARMONIE', description: 'Partner pro zdraví našich členek.' },
    { name: 'PEKÁRNA U JANKA', description: 'Občerstvení na soutěžích a soustředěních.' },
    { name: 'OPTIKA VIZE', description: 'Podporovatel mládežnického sportu.' },
    { name: 'AUTOŠKOLA START', description: 'Doprava na soutěže.' },
  ],
}
