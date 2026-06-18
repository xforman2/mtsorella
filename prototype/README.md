# Prototype

`MT Sorella.html` is the self-contained **high-fidelity prototype** of the MT Sorella
website/app (the source the functional requirements in [`../requirements.md`](../requirements.md)
were derived from).

- It's a single bundled HTML file — fonts, the React/Babel runtime, and the component logic
  are embedded, so it runs by simply opening it in a browser (no build step, no network).
- Treat it as a **design reference only**, not production code. The real app is built under
  `/frontend` (React + Vite + TS, issue #4) and `/backend` (.NET 10, issue #3).
- Covers all three access layers from the spec: public area, member zone, and admin panel.
