export const formatPayload = (payload) => {
  const payloadParts = payload.toString().split('.')
  const integerPart = payloadParts[0]
  const decimalPart = payloadParts[1] || '0'

  const formattedIntegerPart = integerPart.replace(/\B(?=(\d{3})+(?!\d))/g, ' ')

  return `${formattedIntegerPart}${
    decimalPart !== '' ? '.' + decimalPart : ''
  } kg`
}
