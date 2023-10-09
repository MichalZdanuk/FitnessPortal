import { createContext, useContext, useEffect, useState } from 'react'

const viewportContext = createContext({})

export const ViewportProvider = ({ children }) => {
  const [width, setWidth] = useState(window.innerWidth)
  const [height, setHeight] = useState(window.innerHeight)

  const handleWindowResize = () => {
    setWidth(window.innerWidth)
    setHeight(window.innerHeight)
  }

  useEffect(() => {
    window.addEventListener('resize', handleWindowResize)
    return () => window.removeEventListener('resize', handleWindowResize)
  }, [])

  return (
    <viewportContext.Provider value={{ width, height }}>
      {children}
    </viewportContext.Provider>
  )
}

// eslint-disable-next-line react-refresh/only-export-components
export const useViewport = () => {
  const { width, height } = useContext(viewportContext)
  const breakpoint = 768
  const isMobile = width < breakpoint ? true : false

  return { isMobile, width, height, breakpoint }
}
