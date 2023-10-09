import {
  Bar,
  BarChart,
  CartesianGrid,
  Legend,
  ResponsiveContainer,
  Tooltip,
  XAxis,
  YAxis,
} from 'recharts'
import { useViewport } from '../../contexts/viewportContext'

const TrainingProgressChart = ({ data }) => {
  const { isMobile, width } = useViewport()
  let mobileWidth = width - 30
  return !isMobile ? (
    <ResponsiveContainer width="80%" height="70%">
      <BarChart
        data={data}
        margin={{ top: 30, right: 30, bottom: 30, left: 30 }}
      >
        <CartesianGrid strokeDasharray="3 3" />
        <XAxis dataKey="date" />
        <YAxis />
        <Tooltip />
        <Legend iconType="rect" />
        <Bar dataKey="payload" fill="#00a896" />
      </BarChart>
    </ResponsiveContainer>
  ) : (
    <BarChart width={mobileWidth} height={500} data={data}>
      <CartesianGrid strokeDasharray="3 3" />
      <XAxis dataKey="date" />
      <YAxis />
      <Tooltip />
      <Legend />
      <Bar dataKey="payload" fill="#00a896" />
    </BarChart>
  )
}

export default TrainingProgressChart
