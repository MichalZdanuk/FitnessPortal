import { Alert } from 'react-bootstrap'

const NotFoundMessage = ({ name }) => {
  return <Alert variant="danger">{name} could not be found</Alert>
}

export default NotFoundMessage
