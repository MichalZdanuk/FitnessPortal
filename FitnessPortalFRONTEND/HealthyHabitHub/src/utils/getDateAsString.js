export const getCurrentDate = () => {
  const today = new Date()
  const day = String(today.getDate()).padStart(2, '0')
  const month = String(today.getMonth() + 1).padStart(2, '0') // Months are zero-based, so we add 1
  const year = today.getFullYear()

  return `${day}.${month}.${year}`
}
