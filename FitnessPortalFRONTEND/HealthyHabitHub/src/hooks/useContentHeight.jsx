import { useEffect, useState } from 'react'

const useContentHeight = () => {
  const [navbarHeight, setNavbarHeight] = useState(0)
  const [footerHeight, setFooterHeight] = useState(0)
  const [contentHeight, setContentHeight] = useState(0)

  useEffect(() => {
    const navbar = document.querySelector('.navbar')
    if (navbar) setNavbarHeight(navbar.offsetHeight)

    const footer = document.querySelector('.footer')
    if (footer) setFooterHeight(footer.offsetHeight)

    setContentHeight(window.innerHeight - navbarHeight - footerHeight)
  }, [navbarHeight, footerHeight])

  return { navbarHeight, footerHeight, contentHeight }
}

export default useContentHeight
