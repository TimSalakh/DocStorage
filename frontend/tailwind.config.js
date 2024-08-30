const flowbite = require('flowbite-react/tailwind')

/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ['./src/**/*.{html,js,jsx,ts,tsx}', flowbite.content()],
  theme: {
    extend: {}
  },
  plugins: [
    function ({ addUtilities }) {
      const newUtilities = {
        '.no-scrollbar::-webkit-scrollbar': {
          display: 'none'
        },
        '.no-scrollbar': {
          '-ms-overflow-style': 'none',
          'scrollbar-width': 'none'
        }
      }

      addUtilities(newUtilities)
    },
    flowbite.plugin()
  ]
}
