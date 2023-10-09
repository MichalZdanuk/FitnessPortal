import { Table } from 'react-bootstrap'
import { useViewport } from '../../contexts/viewportContext'
import { formatBMIScore } from '../../utils/formatBMIScore'
import { getBMICategoryIcon } from '../../utils/getBMICategoryIcon'

const BMITable = ({ bmiHistory, startNumber }) => {
  const { isMobile } = useViewport()

  return (
    <Table striped bordered hover size={isMobile ? 'sm' : undefined}>
      <thead>
        <tr>
          {!isMobile && <th>No.</th>}
          <th>Score</th>
          <th>Category</th>
          <th>Date</th>
          <th>Condition</th>
        </tr>
      </thead>
      <tbody>
        {bmiHistory.map((bmi, idx) => (
          <tr key={bmi.id}>
            {!isMobile && (
              <td style={{ textAlign: 'center' }}>{startNumber + idx}</td>
            )}
            <td>{formatBMIScore(bmi.bmiScore)}</td>
            <td>{bmi.bmiCategory}</td>
            {!isMobile && <td>{bmi.date.substring(0, 10)}</td>}
            {isMobile && <td>{bmi.date.substring(0, 7)}</td>}
            <td style={{ textAlign: 'center' }}>
              {getBMICategoryIcon(bmi.bmiCategory)}
            </td>
          </tr>
        ))}
      </tbody>
    </Table>
  )
}

export default BMITable
