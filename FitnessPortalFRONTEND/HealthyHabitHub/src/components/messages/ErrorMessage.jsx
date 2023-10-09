import { Alert } from 'react-bootstrap'

const ErrorMessage = ({ message, widthPercentage }) => {
  return (
    <div style={{ display: 'flex', justifyContent: 'center' }}>
      <Alert
        style={{ width: `${widthPercentage}%`, textAlign: 'center' }}
        variant={'danger'}
      >
        {message}
      </Alert>
    </div>
  )
}

export default ErrorMessage
