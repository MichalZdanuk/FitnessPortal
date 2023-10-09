import classes from './TileList.module.css'
import { useNavigate } from 'react-router-dom'
import { accountTiles } from '../../../../mocks/mockedData'

const TileList = () => {
  return accountTiles.map((tile) => (
    <Tile key={tile.id} name={tile.name} link={tile.link} icon={tile.icon} />
  ))
}

const Tile = ({ name, link, icon }) => {
  const navigate = useNavigate()
  const handleClick = () => navigate(link)

  return (
    <div className={classes['tile-div']} onClick={handleClick}>
      <img src={icon} className={classes['tile-img']} />
      <h1>{name}</h1>
    </div>
  )
}

export default TileList
