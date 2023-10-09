import { InfinitySpin } from 'react-loader-spinner'
import useContentHeightDesktop from '../../hooks/useContentHeightDesktop'

export const MySpinnerWithContainer = () => {
  const { contentHeight } = useContentHeightDesktop()

  const spinnerContainerStyle = {
    display: 'flex',
    justifyContent: 'center',
    alignItems: 'center',
    height: contentHeight, // Use innerHeight here
  }

  return (
    <div style={spinnerContainerStyle}>
      <InfinitySpin width="200" color="#00a896" />
    </div>
  )
}

const MySpinner = () => {
  return <InfinitySpin width="200" color="#00a896" />
}

export default MySpinner
