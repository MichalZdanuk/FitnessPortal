import { useEffect, useState } from 'react'

const useContentHeightDesktop = () => {
  const [navbarHeight, setNavbarHeight] = useState(0)
  const [footerHeight, setFooterHeight] = useState(0)
  const [contentHeight, setContentHeight] = useState('calc(100dvh - 0px)') // Initial value

  useEffect(() => {
    const recalculateContentHeight = () => {
      const navbar = document.querySelector('.navbar')
      //   const footer = document.querySelector('.footer')
      const footer = document.getElementById('footer')

      if (navbar) setNavbarHeight(navbar.offsetHeight)
      if (footer) setFooterHeight(footer.offsetHeight)

      setContentHeight(`calc(100dvh - ${navbarHeight + footerHeight}px)`)
    }

    window.addEventListener('resize', recalculateContentHeight)
    recalculateContentHeight()

    return () => {
      window.removeEventListener('resize', recalculateContentHeight)
    }
  }, [navbarHeight, footerHeight])

  return { navbarHeight, footerHeight, contentHeight }
}

export default useContentHeightDesktop
